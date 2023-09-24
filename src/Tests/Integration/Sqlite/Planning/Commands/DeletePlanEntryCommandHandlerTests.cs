using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration.Sqlite.Planning.Commands;

[IntegrationTest]
internal class DeletePlanEntryCommandHandlerTests : SqliteDatabaseIntegrationTest
{
    private IServiceScope? _testServiceScope;

    protected override void Setup(IEnumerable<TestConfiguration> configuration)
    {
        var collection = TestServiceCollectionCreator.CreateWithSqlite(configuration);

        _testServiceScope = collection.BuildServiceProvider().CreateScope();
    }

    protected override void TearDown()
    {
        _testServiceScope?.Dispose();
    }

    [Test]
    public async Task DeletePlanEntryCommand_WithPlanEntry_DeletesNewPlanEntry()
    {
        // Arrange
        var entryId = await CreateNewEntry();

        var mediator = _testServiceScope!.ServiceProvider.GetRequiredService<IMediator>();

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
        var mediator = _testServiceScope!.ServiceProvider.GetRequiredService<IMediator>();

        var model = new PlanEntryModel
        {
            PlanType = PlanTypeId.Credit,
            Amount = 1337.7,
            EventDate = new DateOnly(2023, 12, 12),
            RepeatOn = PlanRepeatOnId.Yearly,
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
