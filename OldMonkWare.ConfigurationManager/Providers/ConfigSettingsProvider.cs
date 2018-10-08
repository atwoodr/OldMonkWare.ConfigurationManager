using OldMonkWare.Configuration;
using System.Security;

namespace OldMonkWare.Providers
{
    public interface IConfigSettingsProvider
    {
        string this[string name] { get; }

        SecureString GetConnectionString(string connectionStringName);
        SecureString GetProtectedAppSetting(string protectedAppSettingKey);
        byte[] GetProtectedAppSettingAsByteArray(string protectedAppSettingKey);

        SecureString GetConfigFileSectionSetting(string configFileSettingKey, string configFileSectionName);
        byte[] GetConfigFileSectionSettingAsByteArray(string configFileSettingKey, string configFileSectionName);
    }

    public class ConfigSettingsProvider : IConfigSettingsProvider
    {
        public string this[string name]
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings[name];
            }
        }

        public SecureString GetConnectionString(string connectionStringName)
        {
            return SecureConfigurationManager.ReadConnectionString(connectionStringName);
        }

        public SecureString GetProtectedAppSetting(string protectedAppSettingKey)
        {
            return SecureConfigurationManager.ReadProtectedAppSetting(protectedAppSettingKey);
        }

        public byte[] GetProtectedAppSettingAsByteArray(string protectedAppSettingKey)
        {
            return SecureConfigurationManager.ReadProtectedAppSettingAsByteArray(protectedAppSettingKey);
        }

        public SecureString GetConfigFileSectionSetting(string configFileSettingKey, string configFileSectionName)
        {
            return SecureConfigurationManager.ReadConfigFileAttribute(configFileSettingKey, configFileSectionName);
        }

        public byte[] GetConfigFileSectionSettingAsByteArray(string configFileSettingKey, string configFileSectionName)
        {
            return SecureConfigurationManager.ReadConfigFileAttributeAsByteArray(configFileSettingKey, configFileSectionName);
        }
    }
}