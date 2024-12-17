using EventSearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace EventSearch.Tests
{
    public class SearchServiceTests
    {
        private const string TestFilePath = "test_events.csv";

        public SearchServiceTests()
        {
            GenerateTestEventsFile();
        }

        [Fact]
        public void SearchEvents_ShouldReturnEventsByGenre()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder()
                .SetGenre("Rock")
                .Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, evt => Assert.Equal("Rock", evt.Genre));
        }

        [Fact]
        public void SearchEvents_ShouldReturnEventsByLocation()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder()
                .SetLocation("London")
                .Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, evt => Assert.Equal("London", evt.Location));
        }

        [Fact]
        public void SearchEvents_ShouldReturnEventsByArtist()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder()
                .SetArtist("Foo Fighters")
                .Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, evt => Assert.Equal("Foo Fighters", evt.MainArtist));
        }

        [Fact]
        public void SearchEvents_ShouldReturnEventsByType()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder()
                .SetType("Concert")
                .Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, evt => Assert.Equal("Concert", evt.Type));
        }

        [Fact]
        public void SearchEvents_ShouldReturnEventsByMultipleCriteria()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder()
                .SetGenre("Pop")
                .SetLocation("Paris")
                .SetType("Festival")
                .Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, evt =>
            {
                Assert.Equal("Pop", evt.Genre);
                Assert.Equal("Paris", evt.Location);
            });
        }

        [Fact]
        public void SearchEvents_WithNoMatchingCriteria_ShouldReturnEmptyList()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder()
                .SetGenre("UnknownGenre")
                .Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SearchEvents_WithEmptyCriteria_ShouldReturnAllEvents()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder().Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.Equal(10, result.Count);
        }

        [Fact]
        public void SearchEvents_WhenFileDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var invalidPath = "invalid_file.csv";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => new SearchService(invalidPath));
        }

        [Fact]
        public void SearchEvents_ShouldReturnEventsByMultipleDates()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder()
                .SetDate(new DateTime(2024, 09, 25))
                .SetDate(new DateTime(2024, 08, 12))
                .Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, evt => evt.Date == new DateTime(2024, 08, 12));
            Assert.Contains(result, evt => evt.Date == new DateTime(2024, 09, 25));
        }

        [Fact]
        public void SearchEvents_ShouldReturnEventsByArtistAndLocation()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder()
                .SetArtist("Coldplay")
                .SetLocation("New York")
                .Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, evt => 
            {
                Assert.Equal("Coldplay", evt.MainArtist);
                Assert.Equal("New York", evt.Location);
            });
        }

        [Fact]
        public void SearchEvents_ShouldReturnEventsByArtistDateAndLocation()
        {
            // Arrange
            var searchService = new SearchService(TestFilePath);
            var criteria = new EventSearchCriteria.Builder()
                .SetArtist("Coldplay")
                .SetLocation("New York")
                .SetDate(new DateTime(2024, 12, 01))
                .Build();

            // Act
            var result = searchService.SearchEvents(criteria);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, evt => 
            {
                Assert.Equal("Coldplay", evt.MainArtist);
                Assert.Equal("New York", evt.Location);
                Assert.Equal(new DateTime(2024, 12, 01), evt.Date);
            });
        }

        private void GenerateTestEventsFile()
        {
            var events = new List<string>
            {
                "Name,Date,Location,Genre,Type,MainArtist,Theme,Description,Image",
                "Rock Concert,2024-08-12,London,Rock,Concert,Foo Fighters,Rock Theme,Awesome rock concert,event1.jpg",
                "Jazz Night,2024-09-10,New Orleans,Jazz,Concert,Louis Armstrong,Jazz Theme,Jazz music night,event2.jpg",
                "Pop Festival,2024-07-15,Paris,Pop,Festival,Beyonce,Pop Theme,Pop festival in Paris,event3.jpg",
                "Electronic Party,2024-06-20,Berlin,Electronic,Party,David Guetta,Electronic Theme,Electronic party,event4.jpg",
                "Theatre Play,2024-11-05,London,Drama,Theatre,Shakespeare Company,Drama Theme,Classic play,event5.jpg",
                "Rock Night,2024-12-01,New York,Rock,Concert,Coldplay,Rock Theme,Rock night in NY,event6.jpg",
                "Art Exhibition,2024-09-25,Paris,Art,Exhibition,Van Gogh Exhibition,Art Theme,Art exhibition,event7.jpg",
                "Comedy Show,2024-08-15,Los Angeles,Comedy,Theatre,Standup Artist,Comedy Theme,Comedy show,event8.jpg",
                "Hip Hop Battle,2024-10-05,Rio de Janeiro,Hip Hop,Party,Eminem,Hip Hop Theme,Hip hop battle,event9.jpg",
                "Classical Night,2024-09-15,Sydney,Classical,Concert,Mozart Orchestra,Classical Theme,Classical concert,event10.jpg"
            };

            File.WriteAllLines(TestFilePath, events);
        }
    }
}
