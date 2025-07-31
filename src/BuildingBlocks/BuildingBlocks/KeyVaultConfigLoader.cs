using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace BuildingBlocks;
public static class KeyVaultConfigLoader
{
    public static string LoadSecret(
        ConfigurationManager configuration,
        string secretName,
        string keyVaultUriKey)
    {
        var keyVaultUri = configuration[keyVaultUriKey];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
        }

        return configuration[secretName]
            ?? throw new InvalidOperationException($"Secret '{secretName}' not found in Key Vault.");
    }
}