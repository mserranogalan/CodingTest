# CodingTest
Santander - Coding Test

📞 The project has three HTTP GET calls 📞:

 📍 Building three methods aims to demonstrate and differentiate the efficiency of asynchronous, multithreading, parallel, and Channels programming. 📌
 📍 In this coding challenge, it has been shown that using Channels instead of locks is more efficient. 📌

## A) By ID with simple asynchronous calls ##    "GetBestHackersStoriesById"

📞📞 "/api/posts/{id}"  📞📞  For this call you must indicate the ID of a Story

## B) Obtain the details of all stories using asynchronous programming, multithreading, and Parallel Programming ##    2GetBestHackersStoriesDetails"

📞📞 "/api/posts/BestHackersStoriesDetails"  📞📞  Only this call is sent

1. Obtain the IDs with _hackersStoriesService.GetAsync()
2. Use Parallel.ForEachAsync to query the details of each story in parallel:
  • A limit of 10 concurrent tasks is set (MaxDegreeOfParallelism = 10).
  • Each request obtains the details with GetBestHackersStoriesDetails.
3. Create formatted objects with the necessary information.
4. Synchronize the storyDetailsList with a lock to avoid concurrency issues.

📍 This provides the following benefits: 
  📌 High concurrency without overloading the server. 
  📌 Avoids unnecessary blocking by being completely asynchronous. 
  📌 Use Parallel.ForEachAsync for better CPU management.

## C) Get details of all stories using Channels instead of locks ##    "GetBestHackersStoriesDetailsChannels"

📞📞 "/api/posts/BestHackersStoriesDetailsChannels"  📞📞  Only this call is sent

1. Create a Channel<T> to store the formatted stories.
2. Launch all tasks in parallel to get details of each story.
3. Each task is written to the Channel<T> asynchronously.
4. We automatically close the channel when the tasks are complete (ContinueWith).
5. Read data in a foreach await loop, without blocking other processes.

📍 Why is this version faster?
  📌 Eliminates locks and uses Channel<T>, avoiding unnecessary blocking.
  📌 Produces and consumes data concurrently, without impacting performance.
  📌 Minimizes wait time by processing elements as soon as they become available.
  📌 Use Task.WhenAll to wait for all tasks without affecting the channel.
📍 This reduces overhead and improves efficiency 🚀
