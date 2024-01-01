using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

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
	public static bool IsLevelCompleted(int level)
	{
        string fullPath = Application.persistentDataPath + "/CompletedLevels.json";
		if (File.Exists(fullPath))
		{
			string text = File.ReadAllText(fullPath);
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
		else
			return false;
    }
	public static void LevelCompleted(int level)
	{

		string fullPath = Application.persistentDataPath + "/CompletedLevels.json";
		if (!File.Exists(fullPath))
		{
			CreateCompletedLevelsFile(fullPath);

		}
		string text = File.ReadAllText(fullPath);
		JObject levelInfo = JObject.Parse(text);
		JArray array = (JArray)levelInfo["data"];
		JToken levelData = array.Where(x => (string)x["name"] == level.ToString()).FirstOrDefault();
		levelData["isCompleted"] = true;
		File.WriteAllText(fullPath, levelInfo.ToString());
	}
	public static void CreateCompletedLevelsFile(string filePath) 
	{
        List<object> taskDataList = new List<object>();
        for (int i = 1; i <= 10; i++)
        {
            taskDataList.Add(new
            {
                name = i.ToString(),
                isCompleted = (i == 7)
            });
        }
        var jsonData = new
        {
            data = taskDataList
        };
        File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonData, Formatting.Indented));
    }

}
