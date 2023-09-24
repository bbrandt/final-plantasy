using Polly;

namespace TRS.FinalPlantasy.Tests.Support;

internal static class TestFileDeleter
{
    public static void Delete(string file)
    {
        var policy = Policy.Handle<IOException>()
            .WaitAndRetry(5, (index) => TimeSpan.FromSeconds(1));

        policy.Execute(() => DeleteFile(file));

        if (File.Exists(file))
        {
            throw new Exception($"Failed to remove file '{file}'");
        }            
    }

    private static void DeleteFile(string file)
    {
        if (!File.Exists(file))
        {
            return;
        }

        File.Delete(file);
    }
}
