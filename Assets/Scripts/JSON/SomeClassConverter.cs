using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;
using System.Collections.Generic;


public class SomeClassConverter : JsonConverter<SomeClass>
{
    public override SomeClass ReadJson(JsonReader reader, Type objectType, SomeClass existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        SomeClass c = new SomeClass();
        JObject jObj = JObject.Load(reader);

        

        return c;
    }

    public override void WriteJson(JsonWriter writer, SomeClass value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("P");
        writer.WriteValue(value.pos);
        writer.WritePropertyName("R");
        writer.WriteValue(value.rot);
        writer.WritePropertyName("S");
        writer.WriteValue(value.scale);
        writer.WritePropertyName("C");
        writer.WriteValue(value.color);
        writer.WriteEndObject();
    }
}

public class SomeClassListConverter : JsonConverter<List<SomeClass>>
{
    public override List<SomeClass> ReadJson(JsonReader reader, Type objectType, List<SomeClass> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        List<SomeClass> c = new List<SomeClass>();
        JObject jObj = JObject.Load(reader);

        SomeClass d = new SomeClass();
        
        while (true)
        {

        }

        return c;
    }

    public override void WriteJson(JsonWriter writer, List<SomeClass> value, JsonSerializer serializer)
    {
        writer.WriteStartArray();

        foreach (var s in value)
        {
            writer.WriteStartObject();
            writer.WriteValue(s);
            writer.WriteEndObject();
        }
        
        writer.WriteEndArray();
    }
}
