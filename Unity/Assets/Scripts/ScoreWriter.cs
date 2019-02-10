using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWriter : MonoBehaviour {

	public Text scoreText;
	public Text bestScoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "" + Floor.score;
		bestScoreText.text = "" + Floor.bestScore;
	}
}
