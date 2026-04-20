using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataV4();
            SaveLoadManager.Data.Name = "TEST1234";
            SaveLoadManager.Data.Gold = 5678;

            List<int> temp = new List<int>();
            List<string> items = DataTableManager.ItemTable.ItemKeys;

            for (int i = 0; i < items.Count; i++)
            {
                int rand = Random.Range(0, items.Count);

                if (temp.Contains(rand))
                    continue;

                temp.Add(rand);

                var newItem = new SaveItemData();
                newItem.ItemData = DataTableManager.ItemTable.Get(items[rand]);
                SaveLoadManager.Data.ItemId.Add(newItem);
            }

            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                if (SaveLoadManager.Data.ItemId.Count > 0)
                {
                    foreach (var item in SaveLoadManager.Data.ItemId)
                    {
                        Debug.Log(item.InstanceId);
                        Debug.Log(item.ItemData);
                        Debug.Log(item.CreationTime);
                    }
                }
                else
                {
                    Debug.Log("아이템이 없습니다");
                }
            }
            else
            {
                Debug.LogError("세이브파일 없음");
            }
        }
    }
}
