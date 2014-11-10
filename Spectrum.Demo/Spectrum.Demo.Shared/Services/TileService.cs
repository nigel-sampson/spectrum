using System;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using NotificationsExtensions.TileContent;

namespace Spectrum.Demo.Services
{
    public class TileService : ITileService
    {
        private readonly IBitmapService bitmapService;

        public TileService(IBitmapService bitmapService)
        {
            this.bitmapService = bitmapService;
        }

        public void Initialise()
        {
            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();

            tileUpdater.EnableNotificationQueue(true);
        }

        private void EnsureDefaultTile()
        {
            var wideTile = TileContentFactory.CreateTileWide310x150Image();
            var squareTile = TileContentFactory.CreateTileSquare150x150Image();

            wideTile.Square150x150Content = squareTile;

            squareTile.Branding = TileBranding.Name;
            squareTile.Image.Src = "ms-appx:///resources/images/square150.png";

            wideTile.Branding = TileBranding.Name;
            wideTile.Image.Src = "ms-appx:///resources/images/wide.png";

            var notification = wideTile.CreateNotification();

            notification.Tag = "default";

            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();

            tileUpdater.Update(notification);
        }

        public async Task EnqueueSchemeTileAsync(Scheme scheme)
        {
            var wideImage = await bitmapService.CreateSchemeBitmapAsync(scheme, TileSize.Wide);
            var squareImage = await bitmapService.CreateSchemeBitmapAsync(scheme, TileSize.Square);

            var wideTile = TileContentFactory.CreateTileWide310x150ImageAndText01();
            var squareTile = TileContentFactory.CreateTileSquare150x150PeekImageAndText01();

            wideTile.Square150x150Content = squareTile;

            squareTile.Branding = TileBranding.None;
            squareTile.Image.Src = squareImage;
            squareTile.TextHeading.Text = scheme.Name;

            wideTile.Branding = TileBranding.None;
            wideTile.Image.Src = wideImage;
            wideTile.TextCaptionWrap.Text = scheme.Name;

            var notification = wideTile.CreateNotification();

            notification.Tag = scheme.Id.GetHashCode().ToString();

            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();

            tileUpdater.Update(notification);

            EnsureDefaultTile();
        }
    }
}
