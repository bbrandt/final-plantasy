﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.ObjectModel;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Commands;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;
using TRS.FinalPlantasy.Domain.Model.Planning;
using TRS.FinalPlantasy.Tests.Support;

namespace TRS.FinalPlantasy.Tests.Integration.SqlServer.Planning.Queries;

[IntegrationTest]
internal class PlanTimelineQueryHandlerTests : SqlServerDatabaseIntegrationTest
{
    private IServiceScope? _testServiceScope;

    protected override void Setup(IEnumerable<TestConfiguration> configuration)
    {
        var collection = TestServiceCollectionCreator.CreateWithSqlServer(configuration);

        _testServiceScope = collection.BuildServiceProvider().CreateScope();
    }

    protected override void TearDown()
    {
        _testServiceScope?.Dispose();
    }

    [Test]
    public async Task GetTimeline_WithPlanEntries_GetsTimeline()
    {
        // Arrange
        await CreateEntries();

        var mediator = _testServiceScope!.ServiceProvider.GetRequiredService<IMediator>();

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
        var mediator = _testServiceScope!.ServiceProvider.GetRequiredService<IMediator>();

        var models = new Collection<PlanEntryModel>
        {
            new PlanEntryModel
            {
                PlanType = PlanTypeId.Credit,
                Amount = 10000,
                EventDate = new DateOnly(2023, 12, 12),
                RepeatOn = PlanRepeatOnId.None,
                Description = "Starting balance"
            },
            new PlanEntryModel
            {
                PlanType = PlanTypeId.Debit,
                Amount = 500,
                EventDate = new DateOnly(2023, 12, 12),
                RepeatOn = PlanRepeatOnId.Monthly,
                Description = "Monthly Cost"
            },
            new PlanEntryModel
            {
                PlanType = PlanTypeId.Credit,
                Amount = 150,
                EventDate = new DateOnly(2023, 12, 12),
                RepeatOn = PlanRepeatOnId.Monthly,
                Description = "Monthly Gain",
                EndDate = new DateOnly(2024, 6, 1)
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
