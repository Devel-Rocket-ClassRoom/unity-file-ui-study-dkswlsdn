using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class FileIO : MonoBehaviour
{
    string path; 

    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using (FileStream fs = new FileStream(Path.Combine(path, "text1.txt"), FileMode.Create))
        using (BinaryWriter bw = new BinaryWriter(fs))
        {
            bw.Write("Hello");
        }

        using (FileStream fs = new FileStream(Path.Combine(path, "text2.txt"), FileMode.Create))
        using (BinaryWriter bw = new BinaryWriter(fs))
        {
            bw.Write("World");
        }

        using (FileStream fs = new FileStream(Path.Combine(path, "text3.txt"), FileMode.Create))
        using (BinaryWriter bw = new BinaryWriter(fs))
        {
            bw.Write("!!!");
        }


        string [] files = Directory.GetFiles(path);

        foreach (string file in files)
        {
            Debug.Log(Path.GetFileName(file));
        }

        if (!File.Exists(Path.Combine(path, "text1_backup.txt")))
        {
            File.Copy(Path.Combine(path, "text1.txt"), Path.Combine(path, "text1_backup.txt"));
            Debug.Log("복사 완료");
        }

        File.Delete(Path.Combine(path, "text3.txt"));
        Debug.Log("삭제 완료");


        files = Directory.GetFiles(path);

        foreach (string file in files)
        {
            Debug.Log(Path.GetFileName(file));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            using (FileStream fs = File.OpenRead(Path.Combine(path, "text1.txt")))
            using (BinaryReader sr = new BinaryReader(fs))
            {
                string text = sr.ReadString();
                Debug.Log(text);
            }
        }
    }
}
