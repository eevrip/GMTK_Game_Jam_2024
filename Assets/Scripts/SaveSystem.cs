using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveManager(manager Manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levels.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(Manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }   

    public static LevelData LoadManager()
    {
        string path = Application.persistentDataPath + "/levels.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else 
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
