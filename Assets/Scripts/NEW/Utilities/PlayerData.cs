using System;

[Serializable]
public class PlayerData
{
    public string gameSaveName_1 = "Empty";
    public string gameSaveName_2 = "Empty";
    public string gameSaveName_3 = "Empty";

    public bool isGameSave_1 = false;
    public bool isGameSave_2 = false;
    public bool isGameSave_3 = false;

    // Game Save Data
    public int gameSave_1_Chapter = 0;
    public int gameSave_2_Chapter = 0;
    public int gameSave_3_Chapter = 0;

    public int gameSave_1_Level = 0;
    public int gameSave_2_Level = 0;
    public int gameSave_3_Level = 0;
}