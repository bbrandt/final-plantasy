using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Domain.Abstractions.Planning;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Unit.Domain.Planning;

[UnitTest]
internal class PlanEntryDomainServiceTests
{
    private IServiceProvider? _serviceProvider;

    [SetUp]
    public void Setup()
    {
        var collection = TestServiceCollectionCreator.Create();

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Test]
    public async Task CreatePlanEntry_WithValues_Creates()
    {
        // Arrange
        var service = _serviceProvider!.GetRequiredService<IPlanEntryDomainService>();

        // Act
        var entry = service.CreatePlanEntry(
            PlanTypeId.Credit,
            new DateOnly(1995, 5, 5),
            155.55,
            PlanRepeatOnId.BiWeekly,
            "This is a new plan",
            null);

        // Assert
        var assertions = new
        {
            entry
        };

        await Verify(assertions)
            .DontScrubDateTimes();
    }

    [Test]
    public async Task UpdatePlanEntry_WithValues_Updates()
    {
        // Arrange
        var service = _serviceProvider!.GetRequiredService<IPlanEntryDomainService>();

        // Act
        var entry = PlanEntry.NewEntry(
            PlanTypeId.Credit,
            new DateOnly(1995, 5, 5),
            155.55,
            PlanRepeatOnId.BiWeekly,
            "This is a new plan",
            new DateOnly(1996, 1, 1));

        service.UpdatePlanEntry(
            PlanTypeId.Debit,
            new DateOnly(2005, 5, 5),
            765,
            PlanRepeatOnId.Yearly,
            "This is an updated plan",
            null,
            entry);

        // Assert
        var assertions = new
        {
            entry
        };

        await Verify(assertions)
            .DontScrubDateTimes();
    }
}
