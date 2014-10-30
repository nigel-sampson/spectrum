using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Spectrum.Demo.Services
{
    public class Scheme
    {
        public Scheme()
        {
            Id = Guid.NewGuid();
            Colours = new List<Color.RGB>();
        }

        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public DateTimeOffset CreatedOn
        {
            get; set;
        }

        [JsonConverter(typeof(ColorConverter))]
        public IList<Color.RGB> Colours
        {
            get; set;
        }
    }
}
