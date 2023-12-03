using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using System.Linq;

public static class SaveSystem 
{
	public static void SavePlayer(Player player,int checkpoint, int lastLevel) 
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + $"/player_{lastLevel}.fun"; //zapis informacji o poszczególnych poziomach
		FileStream stream = new FileStream(path, FileMode.Create);
		PlayerData data = new PlayerData(player, checkpoint);
		formatter.Serialize(stream, data);
		stream.Close();
	}
	public static void DeleteProgress(int lastLevel) 
	{
        string path = Application.persistentDataPath + $"/player_{lastLevel}.fun";
		File.Delete(path);
	}
	public static PlayerData LoadPlayer(int level)
	{
		string path = Application.persistentDataPath + $"/player_{level}.fun";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();
			return data;
		}
		else
		{
			Debug.Log("Save file not found in " + path);
			return null;
		}
	}
	//public static int LoadLastLevel() 
	//{
	//	string path = Application.persistentDataPath + "/lastlevel.fun";
	//	if (File.Exists(path))
	//	{
	//		BinaryFormatter formatter = new BinaryFormatter();
	//		FileStream stream = new FileStream(path, FileMode.Open);
	//		int lastlevel = (int)formatter.Deserialize(stream);
	//		stream.Close();
	//		return lastlevel;
	//	}
	//	else
	//	{
	//		Debug.LogError("Save file not found in " + path);
	//		return 0;
	//	}
	//}
	public static bool IsLevelCompleted(int level)
	{
        string fullPath = Path.Combine(Application.dataPath, "Scripts", "CompletedLevels.json");
		string text= File.ReadAllText(fullPath);
		JObject levelInfo = JObject.Parse(text);
		JArray array = (JArray)levelInfo["data"];
		JToken levelData = array.Where(x => (string)x["name"] == level.ToString()).FirstOrDefault();
		var completed = (bool)levelData["isCompleted"];

        Debug.Log(completed.ToString());
		if (levelData != null)
			return (bool)levelData["isCompleted"];
		else
			return false;
    }
	public static void LevelCompleted(int level)
	{
        string fullPath = Path.Combine(Application.dataPath, "Scripts", "CompletedLevels.json");
        string text = File.ReadAllText(fullPath);
        JObject levelInfo = JObject.Parse(text);
        JArray array = (JArray)levelInfo["data"];
        JToken levelData = array.Where(x => (string)x["name"] == level.ToString()).FirstOrDefault();
		levelData["isCompleted"] = true;
		File.WriteAllText(fullPath, levelInfo.ToString());
    }
}
