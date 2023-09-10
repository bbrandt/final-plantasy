using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Domain.Planning;
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
        var service = _serviceProvider!.GetRequiredService<PlanTimelineDomainService>();
        var entries = new Collection<PlanEntry>();

        // Act
        var timeline = service.Calculate(entries);

        // Assert
        var assertions = new
        {
            timeline
        };

        await Verify(assertions)
            .DontScrubDateTimes();
    }
}
