using BestHackerStories.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace BestHackerStories.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class BestHackersStoriesController(HackersStoriesService hackersStoriesServiceService) : ControllerBase
    {
        private readonly HackersStoriesService _hackersStoriesService = hackersStoriesServiceService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBestHackersStoriesById(int id)
        {
            var post = await _hackersStoriesService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpGet("BestHackersStoriesDetails")]
        public async Task<IActionResult> GetBestHackersStoriesDetails()
        {
            var storyIds = await _hackersStoriesService.GetAsync();
            if (storyIds == null || !storyIds.Any())
            {
                return NotFound();
            }
            var storyDetailsList = new List<Object>();

            await Parallel.ForEachAsync(storyIds, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (id, _) =>
            {
                var post = await _hackersStoriesService.GetByIdAsync(id);
                if (post != null)
                {
                    var formattedPost = new
                    {
                        title = post.title,
                        uri = post.url,
                        postedBy = post.by,
                        time = post.time,
                        score = post.score,
                        commentCount = post.descendants
                    };
                    lock (storyDetailsList)
                    {
                        storyDetailsList.Add(formattedPost);
                    }
                }
            });
            return Ok(storyDetailsList);
        }


        [HttpGet("BestHackersStoriesDetailsChannels")]
        public async Task<IActionResult> GetBestHackersStoriesDetailsChannels()
        {
            var storyIds = await _hackersStoriesService.GetAsync();
            if (storyIds == null || !storyIds.Any())
            {
                return NotFound();
            }
            var channel = Channel.CreateUnbounded<object>(); // Canal sin límite de capacidad

            var tasks = storyIds.Select(async id =>
            {
                var post = await _hackersStoriesService.GetByIdAsync(id);
                if (post != null)
                {
                    var formattedPost = new
                    {
                        title = post.title,
                        uri = post.url,
                        postedBy = post.by,
                        time = post.time,
                        score = post.score,
                        commentCount = post.descendants
                    };
                    await channel.Writer.WriteAsync(formattedPost);
                }
            }).ToList();

            _ = Task.WhenAll(tasks).ContinueWith(_ => channel.Writer.Complete());
            var storyDetailsList = new List<object>();

            await foreach (var story in channel.Reader.ReadAllAsync())
            {
                storyDetailsList.Add(story);
            }
            return Ok(storyDetailsList);
        }

    }
}
