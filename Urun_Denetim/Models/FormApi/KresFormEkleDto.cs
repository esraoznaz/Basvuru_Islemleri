using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Urun_Denetim.Models.FormApi
{
    public class KresFormEkleDto
    {
        public string isim { get; set; }
        public string soyisim { get; set; }
        public string telno { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly dtarihi { get; set; }

        public string tc { get; set; }
        public string ilce { get; set; }
        public string mahalle { get; set; }
    }

 
}
//public class JsonDateOnlyConverter : JsonConverter<DateOnly>
//{
//    private const string Duzen = "dd-MM-yyyy";

//    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        return DateOnly.ParseExact(reader.GetString(), Duzen, null);
//    }

//    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
//    {
//        writer.WriteStringValue(value.ToString(Duzen));
//    }
//}
