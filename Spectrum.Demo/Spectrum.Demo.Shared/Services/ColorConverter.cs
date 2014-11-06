using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Spectrum.Demo.Services
{
    public class ColorConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                return;

            var colours = (IList<Color.RGB>) value;

            writer.WriteStartArray();

            foreach (var colour in colours)
            {
                writer.WriteValue(colour.ToHexString());
            }

            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var colours = new List<Color.RGB>();

            if (reader.TokenType != JsonToken.StartArray)
                return colours;

            reader.Read();

            while (reader.TokenType != JsonToken.EndArray)
            {
                colours.Add(new Color.RGB(reader.Value.ToString()));

                reader.Read();
            }

            return colours;
        }

        public override bool CanConvert(Type objectType)
        {
            var colorTypeInfo = typeof (IList<Color.RGB>).GetTypeInfo();
            var objectTypeInfo = objectType.GetTypeInfo();

            return colorTypeInfo.IsAssignableFrom(objectTypeInfo);
        }
    }
}
