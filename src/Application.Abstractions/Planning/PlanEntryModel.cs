using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Application.Abstractions.Planning;

public class PlanEntryModel
{
    public PlanType? PlanType { get; set; }

    public DateOnly? EventDate { get; set; }

    public double? Amount { get; set; }

    public PlanRepeatOn? RepeatOn { get; set; }
}
