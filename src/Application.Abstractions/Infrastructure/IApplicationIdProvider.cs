namespace TRS.FinalPlantasy.Application.Abstractions.Infrastructure;

public interface IApplicationIdProvider
{
    /// <summary>
    /// Get the id representing this application instance
    /// </summary>
    /// <returns></returns>
    string GetId();
}
