using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveData
{
	public static void SaveGame(PlayerData data)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath
					 + "/MySaveData.dat");
		bf.Serialize(file, data);
		file.Close();
		Debug.Log("Game data saved!");
	}

	public static PlayerData LoadGame()
	{
		if (File.Exists(Application.persistentDataPath
					   + "/MySaveData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file =
					   File.Open(Application.persistentDataPath
					   + "/MySaveData.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			Debug.Log("Game data loaded!");

			return data;
		}
		else
		{
			Debug.LogError("There is no save data!");
			return null;
		}
	}
}
