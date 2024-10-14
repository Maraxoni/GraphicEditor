using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GraphicEditor
{
    public class ShapeConverter : JsonConverter<Shape>
    {
        public override Shape Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                string shapeType = root.GetProperty("Type").GetString();

                switch (shapeType)
                {
                    case "RectangleShape":
                        return JsonSerializer.Deserialize<RectangleShape>(root.GetRawText(), options);
                    case "LineShape":
                        return JsonSerializer.Deserialize<LineShape>(root.GetRawText(), options);
                    case "CircleShape":
                        return JsonSerializer.Deserialize<CircleShape>(root.GetRawText(), options);
                    default:
                        throw new JsonException("Unknown shape type");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Shape value, JsonSerializerOptions options)
        {
            using (var doc = JsonDocument.Parse(JsonSerializer.Serialize(value, value.GetType(), options)))
            {
                writer.WriteStartObject();
                foreach (var property in doc.RootElement.EnumerateObject())
                {
                    property.WriteTo(writer);
                }
                writer.WriteEndObject();
            }
        }
    }
}