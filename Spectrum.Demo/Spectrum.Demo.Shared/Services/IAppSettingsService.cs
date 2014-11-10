using System;

namespace Spectrum.Demo.Services
{
    public interface IAppSettingsService
    {
        bool StorageInitialised { get; set; }
    }
}