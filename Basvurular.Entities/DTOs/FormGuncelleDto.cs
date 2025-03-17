using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basvurular.Entities.DTOs
{
    public class FormGuncelleDto
    {
        public string isim { get; set; }
        public string soyisim { get; set; }
        public string telno { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly dtarihi { get; set; }

        public string tc { get; set; }
        public string ilce { get; set; }
        public string mahalle { get; set; }
        public bool Aktif { get; set; }
    }

    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "dd-MM-yyyy";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString(), Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
