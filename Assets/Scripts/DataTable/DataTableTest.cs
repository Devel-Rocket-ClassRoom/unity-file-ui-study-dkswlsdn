using UnityEngine;

public class DataTableTest : MonoBehaviour
{
    public void OnClickStringTableKR()
    {
        Variables.Languages = Languages.Korean;
        Debug.Log(DataTableManager.StringTable.Get("YouDie"));
    }

    public void OnClickStringTableEN()
    {
        Variables.Languages = Languages.English;
        Debug.Log(DataTableManager.StringTable.Get("YouDie"));
    }

    public void OnClickStringTableJP()
    {
        Variables.Languages = Languages.Japanese;
        Debug.Log(DataTableManager.StringTable.Get("YouDie"));
    }
}
