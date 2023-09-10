using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Domain.Planning;

public class PlanningDomainService
{
    /// <summary>
    /// Create a new plant entry entity
    /// </summary>
    /// <param name="planType"></param>
    /// <param name="eventDate"></param>
    /// <param name="amount"></param>
    /// <param name="repeatOn"></param>
    /// <returns></returns>
    /*
     * This domain is a little anemic but so far it does not have much behavior.
     */
    public PlanEntry CreatePlanEntry(
        PlanType planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOn repeatOn,
        string description)
    {
        return PlanEntry.NewEntry(
            planType,
            eventDate,
            amount,
            repeatOn,
            description);
    }
}
