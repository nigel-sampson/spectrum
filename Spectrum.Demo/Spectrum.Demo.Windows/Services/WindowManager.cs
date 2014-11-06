using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectrum.Demo.Services
{
    public class WindowManager : IWindowManager
    {
        public Task<bool> ShowDialogAsync(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            throw new NotImplementedException();
        }
    }
}
