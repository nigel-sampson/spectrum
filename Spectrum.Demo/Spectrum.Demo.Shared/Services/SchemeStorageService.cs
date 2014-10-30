using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using Spectrum.Demo.Extensions;

namespace Spectrum.Demo.Services
{
    public class SchemeStorageService : ISchemeStorageService
    {
        private IReadOnlyCollection<Scheme> cachedSchemes;

        public async Task<IReadOnlyCollection<Scheme>> GetSchemesAsync()
        {
            if (cachedSchemes != null)
                return cachedSchemes;

            var roamingFiles = await ApplicationData.Current.RoamingFolder.GetFilesAsync();

            var schemes = await roamingFiles
                .Where(f => f.FileType == ".scheme")
                .SelectAsync(DeserializeFileAsync);

            cachedSchemes = new ReadOnlyCollection<Scheme>(schemes.ToList());

            return cachedSchemes;
        }

        private async Task<Scheme> DeserializeFileAsync(StorageFile file)
        {
            var json = await FileIO.ReadTextAsync(file);

            return JsonConvert.DeserializeObject<Scheme>(json);
        }
    }
}
