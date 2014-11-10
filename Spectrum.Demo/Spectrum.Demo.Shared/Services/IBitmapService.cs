using System.Threading.Tasks;

namespace Spectrum.Demo.Services
{
    public interface IBitmapService
    {
        Task<string> CreateSchemeBitmapAsync(Scheme scheme, TileSize size);
    }
}