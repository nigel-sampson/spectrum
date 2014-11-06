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
        private IList<Scheme> cachedSchemes;

        public async Task<IReadOnlyCollection<Scheme>> GetSchemesAsync()
        {
            if (cachedSchemes != null)
                return new ReadOnlyCollection<Scheme>(cachedSchemes);

            var roamingFiles = await ApplicationData.Current.RoamingFolder.GetFilesAsync();

            cachedSchemes = (await roamingFiles
                .Where(f => f.FileType == ".scheme")
                .SelectAsync(DeserializeFileAsync))
                .ToList();

            return new ReadOnlyCollection<Scheme>(cachedSchemes);
        }

        public async Task SaveSchemeAsync(Scheme scheme)
        {
            cachedSchemes.Add(scheme);

            var file = await ApplicationData.Current.RoamingFolder.CreateFileAsync(String.Format("{0:N}.scheme", scheme.Id), CreationCollisionOption.ReplaceExisting);

            var json = JsonConvert.SerializeObject(scheme);

            await FileIO.WriteTextAsync(file, json);
        }

        private async Task<Scheme> DeserializeFileAsync(StorageFile file)
        {
            var json = await FileIO.ReadTextAsync(file);

            return JsonConvert.DeserializeObject<Scheme>(json);
        }
    }
}
