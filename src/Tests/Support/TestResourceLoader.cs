using System.Reflection;

namespace TRS.FinalPlantasy.Tests.Support;

internal static class TestResourceLoader
{
    private const string AssemblyPath = "TRS.FinalPlantasy.Tests";

    public static Stream? Load(string resource)
    {
        return Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyPath}.{resource}");
    }

    public static async Task<string?> LoadAsync(string resource)
    {
        using var stream = Load(resource);
        
        if (stream == null)
        {
            return null;
        }

        using var reader = new StreamReader(stream);

        return await reader.ReadToEndAsync();
    }
}