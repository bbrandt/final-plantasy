﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration.Planning.Commands;

[IntegrationTest]
internal class AddPlanEntryCommandHandlerTests : DatabaseIntegrationTest
{
    private IServiceProvider? _serviceProvider;

    protected override void Setup(IEnumerable<TestConfiguration> configuration)
    {
        var collection = TestServiceCollectionCreator.Create(configuration);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Test]
    public async Task AddPlanEntryCommand_WithPlanEntry_CreatesNewPlanEntry()
    {
        // Arrange
        var mediator = _serviceProvider!.GetRequiredService<IMediator>();

        // Act
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
}
