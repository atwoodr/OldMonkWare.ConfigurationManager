using System;
using System.Security;

namespace OldMonkWare.Configuration
{
    public static class SecureConfigurationManager
    {
        public static SecureString ReadProtectedAppSetting(string protectedAppSettingKey)
        {
            SecureString s = new SecureString();
            s.ReadConfigFileValueAsSecureString(protectedAppSettingKey, "protectedAppSettings");
            return s;
        }

        public static SecureString ReadConnectionString(string connectionStringName)
        {
            SecureString s = new SecureString();
            s.ReadConfigFileValueAsSecureString(connectionStringName, "connectionStrings");
            return s;
        }

        public static SecureString ReadConfigFileAttribute(string key, string configSectionName)
        {
            SecureString s = new SecureString();
            s.ReadConfigFileValueAsSecureString(key, configSectionName);
            return s;
        }

        private static void ReadConfigFileValueAsSecureString(this SecureString value, string key, string configSectionName)
        {
            dynamic protectedAppSettings = new Formo.Configuration(configSectionName);
            char[] appSettingValueCharacters = protectedAppSettings.Get(key)?.ToCharArray() ?? new char[] { };

            for (int i = 0; i < appSettingValueCharacters.Length; i++)
            {
                value.AppendChar(appSettingValueCharacters[i]);

                //erase the char values as we go so they are not in memory in the array, awaiting GC
                appSettingValueCharacters[i] = default(char);
            }

            value.MakeReadOnly();
        }

        public static byte[] ReadProtectedAppSettingAsByteArray(string protectedAppSettingKey)
        {
            dynamic protectedAppSettings = new Formo.Configuration("protectedAppSettings");
            return Convert.FromBase64String(protectedAppSettings.Get(protectedAppSettingKey));
        }

        public static byte[] ReadConfigFileAttributeAsByteArray(string key, string configSectionName)
        {
            dynamic protectedAppSettings = new Formo.Configuration(configSectionName);
            return Convert.FromBase64String(protectedAppSettings.Get(key));
        }

        /** 
         * Note: If you get the urge to add a method to this class that returns a string object, think twice:
         * https://blogs.msdn.microsoft.com/fpintos/2009/06/12/how-to-properly-convert-securestring-to-string/
         * 
         * We don't want to encourage getting these protected strings out of our encrypted config files only to put them
         * into a string which ends up visible to a hacker in the heap.
         */
    }
}