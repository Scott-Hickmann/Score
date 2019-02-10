using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

	public float[] addRowDelays;

	public static float defaultRoundDelay;
	public static float roundDelay;

	public static float defaultGenerationDelay;
	public static float generationDelay;

	public static int n = 0;

	private int newRowAmount;

	public GameObject number;
	public GameObject numbers;
	private Transform clone;

	static public float width;
	static public float height;

	// Use this for initialization
	void Start () {
		height = Camera.main.orthographicSize * 2;
		width = height * Screen.width/Screen.height;
		defaultRoundDelay = 2;
		roundDelay = defaultRoundDelay;
		defaultGenerationDelay = 1;
		generationDelay = defaultGenerationDelay;
		//StartCoroutine (AddRows ());
		StartCoroutine (ManageDelay ());
		StartCoroutine (Generate ());
	}

	IEnumerator AddRows() {
		newRowAmount = 1;
		for (int n = 1; n < addRowDelays.Length; n++) {
			newRowAmount += 1;
			yield return new WaitForSeconds (addRowDelays[n]);
		}
	}

	IEnumerator ManageDelay() {
		while (roundDelay > 0) {
			yield return new WaitForSeconds (1);
			roundDelay -= 0.03f;
		}
		while (true) {
			yield return new WaitForSeconds (1);
			generationDelay -= 0.03f;
		}
	}

	IEnumerator Generate() {
		while (true) {
			n = 0;
			while (n < Floor.rowAmount) {
				clone = Instantiate (number.transform, numbers.transform);
				clone.transform.position = new Vector3 ((-width/2+(width/4*(n+0.5f))), clone.transform.position.y, clone.transform.position.z);
				clone.gameObject.SetActive (true);
				//delay = Random.Range (3f, 8f);
				/*if (n + 1 == newRowAmount) {
					Floor.rowAmount = newRowAmount;
				}*/
				n++;
				yield return new WaitForSeconds (generationDelay);
			}
			yield return new WaitForSeconds (roundDelay);
		}
	}
}
