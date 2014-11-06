using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectrum.Demo.Services
{
    public interface IWindowManager
    {
        Task<bool> ShowDialogAsync(object rootModel, object context = null, IDictionary<string, object> settings = null);
    }
}