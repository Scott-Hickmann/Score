using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

	public void loadScene (string scene) {
		Floor.bottomRow = new List<GameObject>();
		Floor.score = 0;
		SceneManager.LoadScene (scene);
	}
}
