using System.IO;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

public class Encryption : MonoBehaviour
{
    string path;
    string secretPath;
    string encryptPath;
    string decryptPath;

    byte key = 0xAB;
    bool isEncrypted = false;

    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "Encryption");

        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        secretPath = Path.Combine(path, "secret.txt");
        encryptPath = Path.Combine(path, "encrypt.txt");
        decryptPath = Path.Combine(path, "decrypt.txt");

        File.WriteAllText(secretPath, "Hello World!!!");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isEncrypted)
        {
            Debug.Log("암호화");

            using (FileStream fs = File.OpenRead(secretPath))
            using (FileStream stream = new FileStream(encryptPath, FileMode.Create))
            {
                while (true)
                {
                    int b = fs.ReadByte();
                    if (b == -1) { break; }

                    b = b ^ key;

                    stream.WriteByte((byte)b);
                    isEncrypted = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D) && isEncrypted)
        {
            Debug.Log("복호화");

            using (FileStream fs = File.OpenRead(encryptPath))
            using (FileStream stream = new FileStream(decryptPath, FileMode.Create))
            {
                while (true)
                {
                    int b = fs.ReadByte();
                    if (b == -1) { break; }

                    b = b ^ key;

                    stream.WriteByte((byte)b);
                    isEncrypted = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("정상 출력");

            using (FileStream fs = File.OpenRead(decryptPath))
            {
                string text = string.Empty;
                int b = 0;

                while (true)
                {
                    b = fs.ReadByte();

                    if (b == -1)
                    {
                        Debug.Log(text);
                        break;
                    }

                    text = text + (char)b;
                }

                if (File.ReadAllText(decryptPath).Equals(File.ReadAllText(secretPath)))
                {
                    Debug.Log("원본과 일치");
                }
                else { Debug.Log("원본과 불일치"); }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("비정상 출력");

            using (FileStream fs = File.OpenRead(encryptPath))
            {
                string text = string.Empty;
                int b = 0;

                while (true)
                {
                    b = fs.ReadByte();

                    if (b == -1)
                    {
                        Debug.Log(text);
                        break;
                    }

                    text = text + (char)b;
                }
            }
        }
    }
}
