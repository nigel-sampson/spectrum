using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectrum.Demo.Services
{
    public interface ISchemeStorageService
    {
        Task<IReadOnlyCollection<Scheme>> GetSchemesAsync();
    }
}