using System;
using Caliburn.Micro;
using Spectrum.Demo.Services;

namespace Spectrum.Demo.ViewModels
{
    public class SchemeViewModel : PropertyChangedBase
    {
        private readonly Scheme scheme;

        public SchemeViewModel(Scheme scheme)
        {
            this.scheme = scheme;
        }

        public Scheme Scheme
        {
            get { return scheme; }
        }
    }
}
