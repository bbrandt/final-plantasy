using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using TRS.FinalPlantasy.Domain.Abstractions.Planning;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Unit.Domain.Planning;

[UnitTest]
internal class PlanTimelineDomainServiceTests
{
    private IServiceProvider? _serviceProvider;

    [SetUp]
    public void Setup()
    {
        var collection = TestServiceCollectionCreator.Create();

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Test]
    public async Task CalculateTimeline_WithEntries_Creates()
    {
        // Arrange
        var service = _serviceProvider!.GetRequiredService<IPlanTimelineDomainService>();
        var entries = new Collection<PlanEntry> 
        { 
            PlanEntry.NewEntry(
                PlanType.Credit,
                new DateOnly(2023, 9, 10),
                50000,
                PlanRepeatOn.None,
                "Starting balance",
                null),
            PlanEntry.NewEntry(
                PlanType.Credit,
                new DateOnly(2023, 10, 1),
                2000,
                PlanRepeatOn.BiWeekly,
                "Pay period",
                new DateOnly(2024, 2, 1)),
            PlanEntry.NewEntry(
                PlanType.Debit,
                new DateOnly(2023, 10, 1),
                3000,
                PlanRepeatOn.Monthly,
                "Monthly cost",
                null),
        };

        var end = new DateOnly(2024, 5, 1);

        // Act
        var timeline = service.Calculate(end, entries);

        // Assert
        var assertions = new
        {
            timeline
        };

        await Verify(assertions)
            .DontScrubDateTimes();
    }
}
