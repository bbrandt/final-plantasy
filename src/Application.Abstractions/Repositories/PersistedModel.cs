namespace TRS.FinalPlantasy.Application.Abstractions.Repositories;

public abstract class PersistedModel
{
    public PersistentState PersistentState { get; set; }

    public bool IsPersisted()
    {
        return PersistentState == PersistentState.Updated;
    }
}
