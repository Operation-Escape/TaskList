using Microsoft.Extensions.Configuration;

namespace TaskList.Shared.Common.Extensions;

public static class ConfigurationExtensions
{
    public static string TryGetEnvironmentSetting(this IConfiguration configuration, string key)
    {
        var environmentVariables = Environment.GetEnvironmentVariables();
        if (environmentVariables.Contains(key))
        {
            return environmentVariables[key]!.ToString();
        }

        return configuration.GetSection(key)?.Value;
    }
}