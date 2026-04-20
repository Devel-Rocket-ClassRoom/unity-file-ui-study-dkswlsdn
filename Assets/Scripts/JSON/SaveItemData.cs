using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class SaveItemData
{
    public Guid InstanceId { get; set; }

    [JsonConverter(typeof(ItemConverter))]
    public ItemData ItemData { get; set; }
    public DateTime CreationTime { get; set; }
    
    public SaveItemData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }
}
