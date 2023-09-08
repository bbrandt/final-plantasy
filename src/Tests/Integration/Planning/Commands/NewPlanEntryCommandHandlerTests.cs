﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration.Planning.Commands;

[IntegrationTest]
internal class NewPlanEntryCommandHandlerTests : DatabaseIntegrationTest
{
    private IServiceProvider? _serviceProvider;

    protected override void Setup(IEnumerable<TestConfiguration> configuration)
    {
        var collection = TestServiceCollectionCreator.Create(configuration);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Test]
    public async Task NewPlanEntryCommand_WithPlanEntry_CreatesNewPlanEntry()
    {
        // Arrange
        var mediator = _serviceProvider!.GetRequiredService<IMediator>();

        // Act
        var model = new PlanEntryModel
        {
            PlanType = PlanType.Credit,
            Amount = 1337.7,
            EventDate = new DateOnly(2023, 12, 12),
            RepeatOn = PlanRepeatOn.Yearly
        };

        var command = new NewPlanEntryCommand 
        { 
            Model = model
        };

        var response = await mediator.Send(command);

        // Assert
        var context = _serviceProvider!.GetRequiredService<IPlanningQueryContext>();

        var entity = context.PlanEntries.Single(x => x.Id == response.Value);

        var assertions = new 
        { 
            response,
            entity
        };

        await Verify(assertions)
            .DontScrubDateTimes();
    }
}