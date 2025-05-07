using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GildedRose
{
    public class FileItemsRepository : ItemsRepository
    {
        private readonly string filePath;

        public FileItemsRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public Item[] GetInventory()
        {
            if (!File.Exists(filePath))
                return new Item[0];

            var options = GetJsonOptions();
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Item[]>(json, options);
        }

        public void SaveInventory(Item[] items)
        {
            var options = GetJsonOptions();
            string json = JsonSerializer.Serialize(items, options);
            File.WriteAllText(filePath, json);
        }

        private JsonSerializerOptions GetJsonOptions()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new PolymorphicItemConverter());
            return options;
        }

        private class PolymorphicItemConverter : JsonConverter<Item>
        {
            public override Item Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using var doc = JsonDocument.ParseValue(ref reader);
                var root = doc.RootElement;

                if (!root.TryGetProperty("Type", out var typeProp))
                    throw new JsonException("Champ 'Type' manquant");

                string type = typeProp.GetString();

                return type switch
                {
                    "GenericItem" => JsonSerializer.Deserialize<GenericItem>(root.GetRawText(), options),
                    "AgedItem" => JsonSerializer.Deserialize<AgedItem>(root.GetRawText(), options),
                    "LegendaryItem" => JsonSerializer.Deserialize<LegendaryItem>(root.GetRawText(), options),
                    "EventItem" => JsonSerializer.Deserialize<EventItem>(root.GetRawText(), options),
                    "ConjuredItem" => JsonSerializer.Deserialize<ConjuredItem>(root.GetRawText(), options),
                    _ => throw new NotSupportedException($"Type inconnu : {type}")
                };
            }

            public override void Write(Utf8JsonWriter writer, Item value, JsonSerializerOptions options)
            {
                string type = value.GetType().Name;
                string json = JsonSerializer.Serialize(value, value.GetType(), options);
                using var doc = JsonDocument.Parse(json);

                writer.WriteStartObject();
                writer.WriteString("Type", type);

                foreach (var prop in doc.RootElement.EnumerateObject())
                {
                    prop.WriteTo(writer);
                }

                writer.WriteEndObject();
            }
        }
    }
}
