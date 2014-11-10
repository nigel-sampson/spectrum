using System.Threading.Tasks;

namespace Spectrum.Demo.Services
{
    public interface ITileService
    {
        void Initialise();
        Task EnqueueSchemeTileAsync(Scheme scheme);
    }
}