using System.Net;
using System.Net.Http.Json;
using BookingService.API;
using BookingService.Application.DTOs.Home;
using BookingService.Domain;
using BookingService.Infrastructure.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApi.IntegrationTests
{
    public class AvailableHomesIntegrationTests
        : IClassFixture<WebApplicationFactory<Program>>
    {
        WebApplicationFactory<Program> _factory;

        public AvailableHomesIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAvailableHomes_ShouldHandleLargeDataset_Fast()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(InMemoryHomeDataStore));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton<InMemoryHomeDataStore>(sp =>
                    {
                        var store = new InMemoryHomeDataStore();
                        store.Clear();

                        for (int i = 0; i < 100_000; i++)
                        {
                            store.AddHome(new Home
                            {
                                Id = i,
                                Name = $"Home {i}",
                                AvailableSlots = Enumerable.Range(1, 30)
                                    .Select(d => new DateTime(2025, 9, d)).ToList()
                            });
                        }

                        return store;
                    });


                });
            }).CreateClient();

            var startDate = "2025-09-10";
            var endDate = "2025-09-15";

            var stopwatch = new System.Diagnostics.Stopwatch();

            // Act
            stopwatch.Start();
            var response = await client.GetAsync($"/api/available-homes?startDate={startDate}&endDate={endDate}");
            stopwatch.Stop();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<List<AvailableHomesResponseDto>>();
            result.Should().NotBeNull();
            result!.Count.Should().Be(100_000);

            stopwatch.ElapsedMilliseconds.Should().BeLessThan(3000);
        }




        [Fact]
        public async Task GetAvailableHomes_ShouldReturnNotFound_WhenNoHomeMatches()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(InMemoryHomeDataStore));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton<InMemoryHomeDataStore>(sp =>
                    {
                        var store = new InMemoryHomeDataStore();
                        store.Clear();

                        store.AddHome(new Home
                        {
                            Id = 1,
                            Name = "Home 1",
                            AvailableSlots = Enumerable.Range(1, 30)
                                .Select(d => new DateTime(2025, 9, d)).ToList()
                        });

                        return store;
                    });
                });
            }).CreateClient();
            var startDate = "2030-01-01";
            var endDate = "2030-01-02";

            // Act
            var response = await client.GetAsync($"/api/available-homes?startDate={startDate}&endDate={endDate}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAvailableHomes_ShouldReturnValidationError_WhenStartDateIsOlderThanNow()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(InMemoryHomeDataStore));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton<InMemoryHomeDataStore>(sp =>
                    {
                        var store = new InMemoryHomeDataStore();
                        store.Clear();

                        store.AddHome(new Home
                        {
                            Id = 1,
                            Name = "Home 1",
                            AvailableSlots = Enumerable.Range(1, 30)
                                .Select(d => new DateTime(2025, 9, d)).ToList()
                        });

                        return store;
                    });
                });
            }).CreateClient();
            var startDate = DateTime.Today.AddDays(-1);
            var endDate = "2030-01-02";

            // Act
            var response = await client.GetAsync($"/api/available-homes?startDate={startDate}&endDate={endDate}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task GetAvailableHomes_ShouldReturnValidationError_WhenStartDateIsOlderEndDate()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(InMemoryHomeDataStore));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton<InMemoryHomeDataStore>(sp =>
                    {
                        var store = new InMemoryHomeDataStore();
                        store.Clear();

                        store.AddHome(new Home
                        {
                            Id = 1,
                            Name = "Home 1",
                            AvailableSlots = Enumerable.Range(1, 30)
                                .Select(d => new DateTime(2025, 9, d)).ToList()
                        });

                        return store;
                    });
                });
            }).CreateClient();

            var startDate = "2030-01-03";
            var endDate = "2030-01-02";

            // Act
            var response = await client.GetAsync($"/api/available-homes?startDate={startDate}&endDate={endDate}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task GetAvailableHomes_ShouldFilterHomesCorrectly()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(InMemoryHomeDataStore));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton<InMemoryHomeDataStore>(sp =>
                    {
                        var store = new InMemoryHomeDataStore();
                        store.Clear();

                        store.AddHome(new Home
                        {
                            Id = 1,
                            Name = "Home 1",
                            AvailableSlots = new List<DateTime>
                            {
                                new DateTime(2025, 9, 15),
                                new DateTime(2025, 9, 16),
                                new DateTime(2025, 9, 17),
                            }
                        });

                        store.AddHome(new Home
                        {
                            Id = 2,
                            Name = "Home 2",
                            AvailableSlots = new List<DateTime>
                            {
                                new DateTime(2025, 9, 18),
                                new DateTime(2025, 9, 19),
                            }
                        });

                        return store;
                    });
                });
            }).CreateClient();

            var startDate = "2025-9-18";
            var endDate = "2025-9-19";

            // Act
            var response = await client.GetAsync($"/api/available-homes?startDate={startDate}&endDate={endDate}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<List<AvailableHomesResponseDto>>();

            result.Should().NotBeNull();

            result!.Count.Should().Be(1);

            var home = result[0];

            home.HomeId.Should().Be(2);

            home.HomeName.Should().Be("Home 2");

            home.AvailableSlots.Should().Contain(new[] { new DateTime(2025, 9, 18), new DateTime(2025, 9, 19) });
        }




    }




}
