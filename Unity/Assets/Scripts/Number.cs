using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour {

	public int number;
	public Text numberText;
	public Sprite[] numbersImages;

	void Start () {
		number = Random.Range (Floor.numbersAmount-9, Floor.numbersAmount+1);
		numberText.text = "" + number;
		GetComponent<SpriteRenderer> ().sprite = numbersImages [number % 10];
		//Debug.Log ("Number: " + number + " Text: " + numberText.text);
	}

	void Update () {
		//transform.Translate(Vector3.down * Time.deltaTime, Space.World);
	}
}
