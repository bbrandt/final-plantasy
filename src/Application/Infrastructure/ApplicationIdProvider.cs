using TRS.FinalPlantasy.Application.Abstractions.Infrastructure;

namespace TRS.FinalPlantasy.Application.Infrastructure;

internal class ApplicationIdProvider : IApplicationIdProvider
{
    public string GetId()
    {
        var processId = Environment.ProcessId.ToString("000000");

        return $"FinalPlantasy-{processId}";
    }
}
