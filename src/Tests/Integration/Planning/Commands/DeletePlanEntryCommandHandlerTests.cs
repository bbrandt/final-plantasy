using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration.Planning.Commands;

[IntegrationTest]
internal class DeletePlanEntryCommandHandlerTests : DatabaseIntegrationTest
{
    private IServiceProvider? _serviceProvider;

    protected override void Setup(IEnumerable<TestConfiguration> configuration)
    {
        var collection = TestServiceCollectionCreator.Create(configuration);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Test]
    public async Task DeletePlanEntryCommand_WithPlanEntry_DeletesNewPlanEntry()
    {
        // Arrange
        var entryId = await CreateNewEntry();

        var mediator = _serviceProvider!.GetRequiredService<IMediator>();

        // Act
        var command = new DeletePlanEntryCommand 
        { 
            Id = entryId
        };

        var response = await mediator.Send(command);

        // Assert
        var persistedModel = await mediator.Send(
            new PlanEntryByIdQuery 
            { 
                Id = entryId
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
            PlanType = PlanType.Credit,
            Amount = 1337.7,
            EventDate = new DateOnly(2023, 12, 12),
            RepeatOn = PlanRepeatOn.Yearly,
            Description = "Tuition"
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
