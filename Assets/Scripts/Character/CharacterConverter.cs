using Newtonsoft.Json;
using UnityEngine;
using System;

public class CharacterConverter : JsonConverter<CharacterData>
{
    public override CharacterData ReadJson(JsonReader reader, Type objectType, CharacterData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string id = reader.Value as string;
        return DataTableManager.CharacterTable.Get(id);
    }

    public override void WriteJson(JsonWriter writer, CharacterData value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Id);
    }
}

