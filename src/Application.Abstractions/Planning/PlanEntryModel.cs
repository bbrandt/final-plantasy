using TRS.FinalPlantasy.Application.Abstractions.Repositories;
using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning;

public class PlanEntryModel : PersistedModel
{
    public int? Id { get; set; }

    public PlanType? PlanType { get; set; }

    public DateOnly? EventDate { get; set; }

    public double? Amount { get; set; }

    public PlanRepeatOn? RepeatOn { get; set; }

    public string? Description { get; set; }
}
