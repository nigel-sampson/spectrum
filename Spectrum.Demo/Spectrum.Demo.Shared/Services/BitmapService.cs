using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Spectrum.Universal;
using WinRTXamlToolkit.Imaging;

namespace Spectrum.Demo.Services
{
    public class BitmapService : IBitmapService
    {
        public async Task<string> CreateSchemeBitmapAsync(Scheme scheme, TileSize size)
        {
            var width = size == TileSize.Square ? 150 : 310;

            var bitmap = new WriteableBitmap(width, 150);

            var columnWidth = width / scheme.Colours.Count;

            var x = 0;

            for (int i = 0; i < scheme.Colours.Count; i++)
            {
                var colour = scheme.Colours[i];

                if (i == scheme.Colours.Count - 1) // last column
                    columnWidth = width - ((scheme.Colours.Count - 1) * columnWidth);

                bitmap.FillRectangle(x, 0, x + columnWidth, 150, colour.ToSystemColor(255));

                x += columnWidth;
            }

            var file = await CreateImageFileAsync(scheme, size);

            await bitmap.SaveToFile(file, BitmapEncoder.PngEncoderId);

            return String.Format("ms-appdata:///local/{0}", file.Name);
        }

        private Task<StorageFile> CreateImageFileAsync(Scheme scheme, TileSize size)
        {
            var filename = String.Format("{0:N}-{1}.png", scheme.Id, size);

            return ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting).AsTask();
        }
    }
}
