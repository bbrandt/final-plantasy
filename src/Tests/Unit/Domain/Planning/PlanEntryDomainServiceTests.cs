using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Domain.Planning;
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
        var service = _serviceProvider!.GetRequiredService<PlanEntryDomainService>();

        // Act
        var entry = service.CreatePlanEntry(
            PlanType.Credit,
            new DateOnly(1995, 5, 5),
            155.55,
            PlanRepeatOn.BiWeekly,
            "This is a new plan");

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
        var service = _serviceProvider!.GetRequiredService<PlanEntryDomainService>();

        // Act
        var entry = PlanEntry.NewEntry(
            PlanType.Credit,
            new DateOnly(1995, 5, 5),
            155.55,
            PlanRepeatOn.BiWeekly,
            "This is a new plan");

        service.UpdatePlanEntry(
            PlanType.Debit,
            new DateOnly(2005, 5, 5),
            765,
            PlanRepeatOn.Yearly,
            "This is an updated plan",
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
