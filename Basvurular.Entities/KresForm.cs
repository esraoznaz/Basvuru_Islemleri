using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Basvurular.Entities
{
    public class KresForm
    {
        [Key]
        public int KresFormId { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
        public string telno { get; set; }

        [JsonConverter(typeof(JsonDateOnlyConverter))]
        public DateOnly dtarihi { get; set; }

        public string tc { get; set; }
        public string ilce { get; set; }
        public string mahalle { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public string? isturu { get; set; } = " Ramazan Başvuru Formu";
        public string? durumu { get; set; }
        public string? dagıtım { get; set; }
        public string? SonucAciklama { get; set; }
        public bool Aktif { get; set; } = true;

        public KresForm()
        {
            OlusturulmaTarihi = DateTime.Now;
        }
    }

}


// DateOnly için JSON Dönüştürücü
public class JsonDateOnlyConverter : JsonConverter<DateOnly>
{
    private const string Format = "dd-MM-yyyy";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString(), Format, null);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}

