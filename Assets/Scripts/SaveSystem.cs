using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
	public static void SavePlayer(Player player,int checkpoint, int lastLevel) 
	{
		BinaryFormatter formatter = new BinaryFormatter();

		string path = Application.persistentDataPath + $"/player_{lastLevel}.fun"; //zapis informacji o poszczególnych poziomach
		FileStream stream = new FileStream(path, FileMode.Create);
		PlayerData data = new PlayerData(player, checkpoint, lastLevel);
		formatter.Serialize(stream, data);
		stream.Close();

		string recentlyPlayed = Application.persistentDataPath + $"/lastlevel.fun"; // info o ostatnio rozgrywanym poziomie
		stream = new FileStream(recentlyPlayed, FileMode.Create);
		int data2 = lastLevel;
		formatter.Serialize(stream, data2);
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
	public static int LoadLastLevel() 
	{
		string path = Application.persistentDataPath + "/lastlevel.fun";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			int lastlevel = (int)formatter.Deserialize(stream);
			stream.Close();
			return lastlevel;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return 0;
		}
	}
}
