namespace ACME.DataAccess.Repositories.Tools
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    public class GuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (Guid.TryParse(reader.GetString(), out var guid))
                {
                    return guid;
                }
            }
            return Guid.Empty;
        }
        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
