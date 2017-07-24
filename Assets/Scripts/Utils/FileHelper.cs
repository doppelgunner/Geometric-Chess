using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHelper {

	public static void Serialize(string name, object o) {
		string json = JsonUtility.ToJson(o);
		PlayerPrefs.SetString(name, json);
	}

	public static T Deserialize<T>(string name) {
		string json = PlayerPrefs.GetString(name);
		return JsonUtility.FromJson<T>(json);; 
	}
}
