﻿using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Domain.Abstractions.Planning;

public interface IPlanEntryDomainService
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
     * This domain is anemic but so far it does not have much behavior.
     */
    PlanEntry CreatePlanEntry(
        PlanType planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOn repeatOn,
        string description);

    void UpdatePlanEntry(
        PlanType planType,
        DateOnly eventDate,
        double amount,
        PlanRepeatOn repeatOn,
        string description,
        PlanEntry entity);
}
