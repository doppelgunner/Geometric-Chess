using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {

	public bool destroyOnLoad;
	protected static bool _destroyOnLoad;

	private static T instance;

	public static T Instance {
		get {
				T foundObject = FindObjectOfType<T>();

				if (instance == null) {
					instance = foundObject;
				} else if (instance != foundObject) {
					Destroy(foundObject);
				}

				if (!_destroyOnLoad) DontDestroyOnLoad(foundObject);
				return instance;
			}
	}
}
