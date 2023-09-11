using System.Collections.ObjectModel;
using TRS.FinalPlantasy.Domain.Model.Planning;

namespace TRS.FinalPlantasy.Domain.Planning.Timelines;

internal class ExpandedEventCalculator
{
    /// <summary>
    /// Given a list of entries apply the repeat rules to fill the list out with all the repeated dates
    /// </summary>
    /// <param name="endDate"></param>
    /// <param name="entries"></param>
    /// <returns></returns>
    public IEnumerable<ExpandedEvent> Calculate(
        DateOnly endDate,
        IEnumerable<PlanEntry> entries)
    {
        var workingEntries = entries
            .SelectMany(
                entry =>
                {
                    return ExpandEvent(endDate, entry);
                })
            .OrderBy(x => x.Date)
            .ToList();

        return workingEntries;
    }

    private static IEnumerable<ExpandedEvent> ExpandEvent(
        DateOnly endDate,
        PlanEntry entry)
    {
        const double BiWeeklyDays = 14;
        const double MonthlyDays = 30.4;
        const double YearlyDays = 365.25;

        switch (entry.RepeatOn)
        {
            case PlanRepeatOn.BiWeekly:
                return ExpandEventByInterval(entry, TimeSpan.FromDays(BiWeeklyDays), endDate);
            case PlanRepeatOn.Monthly:
                return ExpandEventByInterval(entry, TimeSpan.FromDays(MonthlyDays), endDate);
            case PlanRepeatOn.Yearly:
                return ExpandEventByInterval(entry, TimeSpan.FromDays(YearlyDays), endDate);
            case PlanRepeatOn.None:
            default:
                return new Collection<ExpandedEvent>
                {
                    new ExpandedEvent(entry.EventDate, entry)
                };
        }
    }

    private static IEnumerable<ExpandedEvent> ExpandEventByInterval(
        PlanEntry entry,
        TimeSpan interval,
        DateOnly totalEndDate)
    {
        var effectiveEndDate = CalculateEndDate(entry.EndDate, totalEndDate);
        var daysDifference = effectiveEndDate.DayNumber - entry.EventDate.DayNumber;
        var intervalCount = daysDifference / interval.TotalDays;

        var timeline = new Collection<ExpandedEvent>
        {
            new ExpandedEvent(entry.EventDate, entry)
        };

        var repeats = Enumerable
            .Range(1, Convert.ToInt32(intervalCount))
            .Select((index) =>
            {
                var additionalDays = Convert.ToInt32(interval.TotalDays) * index;

                var expandedDate = entry.EventDate.AddDays(additionalDays);

                return new ExpandedEvent(expandedDate, entry);
            })
            .ToList();

        var total = timeline.Concat(repeats);

        return total;
    }

    private static DateOnly CalculateEndDate(DateOnly? entryEndDate, DateOnly totalEndDate)
    {
        if (entryEndDate == null)
        {
            return totalEndDate;
        }

        return entryEndDate < totalEndDate ?
            entryEndDate.Value :
            totalEndDate;
    }
}

