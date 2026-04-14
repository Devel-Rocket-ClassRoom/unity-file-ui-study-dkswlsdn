using System.Globalization;
using System.IO;
using CsvHelper;
using UnityEngine;

public class Csv
{
    public string ID { get; set; }
    public string String { get; set; }
}

public class CsvTest01 : MonoBehaviour
{
    public TextAsset textAsset;

    void Start()
    {
        
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TextAsset textAsset = Resources.Load<TextAsset>("DataTable/StringTableKr");
            string csv = textAsset.text;

            using (StringReader reader = new StringReader(csv))
            using (CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<Csv>();

                foreach (var record in records)
                {
                    Debug.Log(record.ID);
                    Debug.Log(record.String);
                }
            }
        }
    }
}
