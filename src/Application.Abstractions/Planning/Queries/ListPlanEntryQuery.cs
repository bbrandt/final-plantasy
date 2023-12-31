﻿using MediatR;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning.Queries;

public class ListPlanEntryQuery : IRequest<IEnumerable<PlanEntryListModel>>
{
}
