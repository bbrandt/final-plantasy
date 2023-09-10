namespace TRS.FinalPlantasy.Application.Abstractions.Planning;

public class PlanEventWithBalanceModel
{
    public PlanEventWithBalanceModel(
        DateOnly date,
        double balance,
        IEnumerable<string> descriptions)
    {
        Date = date;
        Balance = balance;
        Descriptions = descriptions.ToList();
    }

    public DateOnly Date { get; protected set; }

    public double Balance { get; protected set; }

    public IReadOnlyCollection<string> Descriptions { get; protected set; }
}