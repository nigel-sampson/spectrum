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
        private readonly ITileService tileService;
        private readonly IAppSettingsService appSettingsService;
        private IList<Scheme> cachedSchemes;

        public SchemeStorageService(ITileService tileService, IAppSettingsService appSettingsService)
        {
            this.tileService = tileService;
            this.appSettingsService = appSettingsService;
        }

        public async Task<IReadOnlyCollection<Scheme>> GetSchemesAsync()
        {
            if (cachedSchemes != null)
                return new ReadOnlyCollection<Scheme>(cachedSchemes);

            cachedSchemes = new List<Scheme>();

            if (!appSettingsService.StorageInitialised)
            {
                await CreateDemoDataAsync();

                appSettingsService.StorageInitialised = true;
            }

            var roamingFiles = await ApplicationData.Current.RoamingFolder.GetFilesAsync();

            cachedSchemes = (await roamingFiles
                .Where(f => f.FileType == ".scheme")
                .SelectAsync(DeserializeFileAsync))
                .OrderBy(s => s.Name)                
                .ToList();

            return new ReadOnlyCollection<Scheme>(cachedSchemes);
        }

        private async Task CreateDemoDataAsync()
        {
            await SaveSchemeAsync(new Scheme
            {
                Name = "Aspirin C",
                CreatedOn = DateTimeOffset.Now,
                Type = SchemeType.Custom,
                Colours = new List<Color.RGB>
                {
                    new Color.RGB("225378"),
                    new Color.RGB("1695A3"),
                    new Color.RGB("ACF0F2"),
                    new Color.RGB("F3FFE2"),
                    new Color.RGB("EB7F00")
                }
            }, enqueueTile: false);

            await SaveSchemeAsync(new Scheme
            {
                Name = "Cherry Cheesecake",
                CreatedOn = DateTimeOffset.Now,
                Type = SchemeType.Custom,
                Colours = new List<Color.RGB>
                {
                    new Color.RGB("B9121B"),
                    new Color.RGB("4C1B1B"),
                    new Color.RGB("F6E497"),
                    new Color.RGB("FCFAE1"),
                    new Color.RGB("BD8D46")
                }
            }, enqueueTile: false);

            await SaveSchemeAsync(new Scheme
            {
                Name = "Quiet Cry",
                CreatedOn = DateTimeOffset.Now,
                Type = SchemeType.Custom,
                Colours = new List<Color.RGB>
                {
                    new Color.RGB("1C1D21"),
                    new Color.RGB("31353D"),
                    new Color.RGB("445878"),
                    new Color.RGB("92CDCF"),
                    new Color.RGB("EEEFF7")
                }
            }, enqueueTile: false);

            await SaveSchemeAsync(new Scheme
            {
                Name = "Sandy Stone",
                CreatedOn = DateTimeOffset.Now,
                Type = SchemeType.Custom,
                Colours = new List<Color.RGB>
                {
                    new Color.RGB("E6E2AF"),
                    new Color.RGB("A7A37E"),
                    new Color.RGB("EFECCA"),
                    new Color.RGB("046380"),
                    new Color.RGB("002F2F")
                }
            }, enqueueTile: false);
        }

        public async Task SaveSchemeAsync(Scheme scheme, bool enqueueTile = true)
        {
            if (!cachedSchemes.Contains(scheme))
                cachedSchemes.Add(scheme);

            var file = await ApplicationData.Current.RoamingFolder.CreateFileAsync(String.Format("{0:N}.scheme", scheme.Id), CreationCollisionOption.ReplaceExisting);

            var json = JsonConvert.SerializeObject(scheme);

            await FileIO.WriteTextAsync(file, json);

            if (enqueueTile)
                await tileService.EnqueueSchemeTileAsync(scheme);
        }

        public async Task DeleteSchemeAsync(Scheme scheme)
        {
            if (cachedSchemes.Contains(scheme))
                cachedSchemes.Remove(scheme);

            var file = await ApplicationData.Current.RoamingFolder.CreateFileAsync(String.Format("{0:N}.scheme", scheme.Id), CreationCollisionOption.OpenIfExists);

            await file.DeleteAsync();
        }

        private async Task<Scheme> DeserializeFileAsync(StorageFile file)
        {
            var json = await FileIO.ReadTextAsync(file);

            return JsonConvert.DeserializeObject<Scheme>(json);
        }
    }
}
