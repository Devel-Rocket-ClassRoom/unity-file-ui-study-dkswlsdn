using System.IO;
using UnityEngine;

public class Encryption : MonoBehaviour
{
    string path;
    string secretPath;
    string encryptPath;
    string decryptPath;

    byte key = 0xAB;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("암호화");

            using (FileStream fs = File.OpenRead(secretPath))
            {
                int b = fs.ReadByte();
                b = b ^ key;

                using (FileStream stream = new FileStream(encryptPath, FileMode.Create))
                {
                    stream.WriteByte((byte)b);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("복호화");

            using (FileStream fs = File.OpenRead(encryptPath))
            {
                int b = fs.ReadByte();
                b = b ^ key;

                using (FileStream stream = new FileStream(decryptPath, FileMode.Create))
                {
                    stream.WriteByte((byte)b);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("정상 출력");

            using (FileStream fs = File.OpenRead(encryptPath))
            {
                string text = string.Empty;
                int b = 0;

                while (b != -1)
                {
                    b = fs.ReadByte();
                    text += (char)b;
                }

                Debug.Log(text);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("비정상 출력");

            using (FileStream fs = File.OpenRead(decryptPath))
            {
                string text = string.Empty;
                int b = 0;

                while (b != -1)
                {
                    b = fs.ReadByte();
                    text += (char)b;
                }

                Debug.Log(text);
            }
        }
    }
}
