namespace ACME.DataAccess.Repositories.Tools
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class GuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string? stringValue = reader.GetString();
                if (!string.IsNullOrWhiteSpace(stringValue) && Guid.TryParse(stringValue, out var guid))
                {
                    return guid;
                }
            }
            return Guid.NewGuid();
        }
        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value != Guid.Empty ? value.ToString() : null);
        }
    }

}
