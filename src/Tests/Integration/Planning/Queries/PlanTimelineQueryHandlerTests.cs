using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.ObjectModel;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration.Planning.Queries;

[IntegrationTest]
internal class PlanTimelineQueryHandlerTests : DatabaseIntegrationTest
{
    private IServiceProvider? _serviceProvider;

    protected override void Setup(IEnumerable<TestConfiguration> configuration)
    {
        var collection = TestServiceCollectionCreator.Create(configuration);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Test]
    public async Task GetTimeline_WithPlanEntries_GetsTimeline()
    {
        // Arrange
        await CreateEntries();

        var mediator = _serviceProvider!.GetRequiredService<IMediator>();

        // Act
        var query = new PlanTimelineQuery
        {
            EndDate = new DateOnly(2024, 12, 12),
        };

        var response = await mediator.Send(query);

        // Assert
        var assertions = new
        {
            response
        };

        await Verify(assertions)
            .DontScrubDateTimes();
    }

    private async Task CreateEntries()
    {
        var mediator = _serviceProvider!.GetRequiredService<IMediator>();

        var models = new Collection<PlanEntryModel>
        {
            new PlanEntryModel
            {
                PlanType = PlanType.Credit,
                Amount = 10000,
                EventDate = new DateOnly(2023, 12, 12),
                RepeatOn = PlanRepeatOn.None,
                Description = "Starting balance"
            },
            new PlanEntryModel
            {
                PlanType = PlanType.Debit,
                Amount = 500,
                EventDate = new DateOnly(2023, 12, 12),
                RepeatOn = PlanRepeatOn.Monthly,
                Description = "Monthly Cost"
            },
            new PlanEntryModel
            {
                PlanType = PlanType.Credit,
                Amount = 150,
                EventDate = new DateOnly(2023, 12, 12),
                RepeatOn = PlanRepeatOn.Monthly,
                Description = "Monthly Gain"
            }
        };

        foreach (var model in models)
        {
            var command = new AddPlanEntryCommand
            {
                Model = model
            };

            var response = await mediator.Send(command);

            response.Messages.ShouldBeEmpty();
            response.Value.ShouldNotBe(null);
        }
    }
}
