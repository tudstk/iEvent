using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EventRecommendationService;
using EventSearch;

public class LoadTester
{
    public async Task RunLoadTest(int numberOfRequestsPerUser, int concurrentUsers)
    {
        var tasks = new List<Task>();
        var stopwatch = new Stopwatch();
        var service = new SearchService();

        var criteria = new EventSearchCriteria.Builder()
            .SetGenre("Rock")
            .SetLocation("New York")
            .Build();

        stopwatch.Start();

        for (int i = 0; i < concurrentUsers; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                for (int j = 0; j < numberOfRequestsPerUser; j++)
                {
                    await service.SearchEventsAsync(criteria);
                }
            }));
        }

        await Task.WhenAll(tasks);

        stopwatch.Stop();
        Console.WriteLine($"Completed {numberOfRequestsPerUser * concurrentUsers} requests in {stopwatch.ElapsedMilliseconds}ms with {concurrentUsers} concurrent users.");
    }
}
