using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Application.Abstractions.Repositories;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration.Planning.Commands;

[IntegrationTest]
internal class UpdatePlanEntryCommandHandlerTests : DatabaseIntegrationTest
{
    private IServiceProvider? _serviceProvider;

    protected override void Setup(IEnumerable<TestConfiguration> configuration)
    {
        var collection = TestServiceCollectionCreator.Create(configuration);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Test]
    public async Task UpdatePlanEntryCommand_WithPlanEntry_UpdatesNewPlanEntry()
    {
        // Arrange
        var entryId = await CreateNewEntry();

        var mediator = _serviceProvider!.GetRequiredService<IMediator>();

        // Act
        var model = new PlanEntryModel
        {
            Id = entryId,
            PlanType = PlanTypeId.Debit,
            Amount = 5555,
            EventDate = new DateOnly(1983, 12, 12),
            RepeatOn = PlanRepeatOnId.Monthly,
            Description = "Tuition Update",
            PersistentState = PersistentState.Updated,
            EndDate = new DateOnly(2030, 1, 1)
        };

        var command = new UpdatePlanEntryCommand 
        { 
            Model = model
        };

        var response = await mediator.Send(command);

        // Assert
        var persistedModel = await mediator.Send(
            new PlanEntryByIdQuery 
            { 
                Id = response.Value.GetValueOrDefault()
            });

        var assertions = new 
        { 
            response,
            persistedModel
        };

        await Verify(assertions)
            .DontScrubDateTimes();
    }

    private async Task<int> CreateNewEntry()
    {
        var mediator = _serviceProvider!.GetRequiredService<IMediator>();

        var model = new PlanEntryModel
        {
            PlanType = PlanTypeId.Credit,
            Amount = 1337.7,
            EventDate = new DateOnly(2023, 12, 12),
            RepeatOn = PlanRepeatOnId.Yearly,
            Description = "Tuition",
            EndDate = new DateOnly(2025, 1, 1)
        };

        var command = new AddPlanEntryCommand
        {
            Model = model
        };

        var response = await mediator.Send(command);

        response.Messages.ShouldBeEmpty();
        response.Value.ShouldNotBe(null);

        return response.Value.GetValueOrDefault();
    }
}
