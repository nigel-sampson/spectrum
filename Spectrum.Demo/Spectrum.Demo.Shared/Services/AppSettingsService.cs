using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace Spectrum.Demo.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly ApplicationDataContainer roamingSettings;
        private ApplicationDataContainer localSettings;

        public AppSettingsService()
        {
            roamingSettings = ApplicationData.Current.RoamingSettings;
            localSettings = ApplicationData.Current.LocalSettings;
        }

        public bool StorageInitialised
        {
            get
            {
                if (!roamingSettings.Values.ContainsKey("StorageInitialised"))
                    return false;

                return (bool) roamingSettings.Values["StorageInitialised"];
            }
            set { roamingSettings.Values["StorageInitialised"] = value; }
        }
    }
}
