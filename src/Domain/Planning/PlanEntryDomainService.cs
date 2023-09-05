using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Domain.Planning;

public class PlanEntryDomainService
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
    public PlanEntry Create(
        PlanType planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOn? repeatOn)
    {
        return new PlanEntry(
            planType,
            eventDate,
            amount,
            repeatOn);
    }
}
