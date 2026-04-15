using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonTest2 : MonoBehaviour
{
    public string fileName = "test.json";
    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);
    private JsonSerializerSettings jsonSetting;

    public Transform Cube;
    public List<GameObject> BasicShape;
    private Dictionary<GameObject, SomeClass> shapeList;

    private float RandomPos => UnityEngine.Random.Range(-20f, 20f);
    private float Random01 => UnityEngine.Random.Range(0f, 1f);

    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
        jsonSetting.Converters.Add(new QuaternionConverter());
        jsonSetting.Converters.Add(new ColorConverter());

        shapeList = new Dictionary<GameObject, SomeClass>();
        shapeList.Add(Cube.gameObject, SomeClass.ToShape(Cube, 0));
    }

    public void Save()
    {
        List<SomeClass> sList = new List<SomeClass>();

        foreach( var obj in shapeList)
        {
            SomeClass s = obj.Value;

            sList.Add(s);
        }

        string json = JsonConvert.SerializeObject(sList, Formatting.Indented, jsonSetting);
        
        File.WriteAllText(FileFullPath, json);
    }

    public void Load()
    {
        string json = File.ReadAllText(FileFullPath);
        List<SomeClass> obj = JsonConvert.DeserializeObject<List<SomeClass>>(json, jsonSetting);
        Clear();

        for (int i = 0; i < obj.Count; i++)
        {
            Vector3 p = obj[i].pos;
            Quaternion r = obj[i].rot;
            Vector3 s = obj[i].scale;
            Color c = obj[i].color;

            GameObject g = Instantiate(BasicShape[(int)obj[i].type]);
            g.transform.position = p;
            g.transform.rotation = r;
            g.transform.localScale = s;
            g.transform.GetComponent<Renderer>().material.color = c;

            shapeList.Add(g, SomeClass.ToShape(g.transform, obj[i].type));
        }
    }

    public void Create()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 p = new Vector3(RandomPos, RandomPos, RandomPos);
            Quaternion r = new Quaternion(Random01, Random01, Random01, Random01);
            Vector3 s = new Vector3(Random01 * 5, Random01 * 5, Random01 * 5);
            Color c = new Color(Random01, Random01, Random01, Random01);

            int rand = UnityEngine.Random.Range(0, BasicShape.Count);
            GameObject shape = Instantiate(BasicShape[rand]);
            shape.transform.position = p;
            shape.transform.rotation = r;
            shape.transform.localScale = s;
            shape.GetComponent<Renderer>().material.color = c;

            shapeList.Add(shape, SomeClass.ToShape(shape.transform, (ShapeType)rand));
        }
    }

    public void Clear()
    {
        foreach (var g in shapeList)
        {
            Destroy(g.Key.gameObject);
        }

        shapeList.Clear();
    }
}

[Serializable]
public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
    public ShapeType type;

    public override string ToString()
    {
        return $"{pos} / {rot} / {scale} / {color}";
    }

    public static SomeClass ToShape(Transform t, ShapeType st)
    {
        SomeClass s = new SomeClass
        {
            pos = t.position,
            rot = t.rotation,
            scale = t.localScale,
            color = t.GetComponent<Renderer>().material.color,
            type = st
        };

        return s;
    }
}

public enum ShapeType
{
    Cube, Sphere, Capsule, Cylinder,
}