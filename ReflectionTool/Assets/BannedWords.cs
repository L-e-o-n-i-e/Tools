using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//Due to a scandal involving a racial slur as a variable name.
//    Given a set of banned words, we need to build a tool 
//    that searches all classes, fields, properties and methods 
//    to see if they contain a banned word.Build a reflection tool to accomplish this.

    [Serializable]
public class BannedWords 
{
    [SerializeField]HashSet<string> bannedWordsList ;    

    [JsonConverter(typeof(CustomHashSetConverter))]
    public HashSet<string> BannedWordsList { get => bannedWordsList; set => bannedWordsList = value; }

    
}

public class CustomHashSetConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(HashSet<string>);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        return new HashSet<string>(jo.Properties().Select(p => p.Name));
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        HashSet<string> hashSet = (HashSet<string>)value;
        JObject jo = new JObject(hashSet.Select(s => new JProperty(s, s)));
        jo.WriteTo(writer);
    }
}