using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour {

	public GameObject background;
	public Color blackColor;
	public Color whiteColor;

	public RectTransform canvas;

	public Image statusBar;
	public Sprite blackStatusBar;
	public Sprite whiteStatusBar;

	public Image modeButton;

	public Image musicButton;
	public Sprite musicOnButton;
	public Sprite musicOffButton;

	public Image inputBar;
	public Sprite blackInputBar;
	public Sprite whiteInputBar;

	public Image keypad;
	public Sprite blackKeypad;
	public Sprite whiteKeypad;

	public Text scoreText;
	public Text bestScoreText;
	public Text bigScoreText;
	public Text sumText;
	public Text equalText;

	public Text[] plusSigns;

	public Color whiteTextColor;
	public Color blackTextColor;

	public static bool mode;

	void Start () {
		UpdateMode ();
		UpdateMusic ();

		float bigScoreTextHeight = canvas.rect.height / 2 - statusBar.rectTransform.rect.height;
		float inputBarHeight = canvas.rect.height / 2 - keypad.rectTransform.rect.height;
		int fontSize = Mathf.RoundToInt (inputBarHeight / 2) - 4;

		bigScoreText.rectTransform.sizeDelta = new Vector2 (0, bigScoreTextHeight);
		inputBar.rectTransform.sizeDelta = new Vector2 (0, inputBarHeight);
		equalText.rectTransform.sizeDelta = new Vector2 (equalText.rectTransform.rect.width, inputBarHeight);
		sumText.rectTransform.sizeDelta = new Vector2 (sumText.rectTransform.rect.width, inputBarHeight);

		modeButton.rectTransform.anchoredPosition = new Vector2 (modeButton.rectTransform.anchoredPosition.x, -statusBar.rectTransform.rect.height*0.7f);
		musicButton.rectTransform.anchoredPosition = new Vector2 (musicButton.rectTransform.anchoredPosition.x, -statusBar.rectTransform.rect.height*0.7f);
		bigScoreText.rectTransform.anchoredPosition = new Vector2 (bigScoreText.rectTransform.anchoredPosition.x, -statusBar.rectTransform.rect.height - bigScoreTextHeight/2);
		for (int n = 0; n < plusSigns.Length; n++) {
			plusSigns[n].rectTransform.anchoredPosition = new Vector2 (canvas.rect.width / 4 * (n + 1), 4 * canvas.rect.height / 75);
		}
		inputBar.rectTransform.anchoredPosition = new Vector2 (inputBar.rectTransform.anchoredPosition.x, -inputBarHeight / 2);
		equalText.rectTransform.anchoredPosition = new Vector2 (equalText.rectTransform.anchoredPosition.x, -inputBarHeight / 2);
		sumText.rectTransform.anchoredPosition = new Vector2 (sumText.rectTransform.anchoredPosition.x, -inputBarHeight / 2);

		equalText.fontSize = fontSize;
		sumText.fontSize = fontSize;
	}
	
	public void ChangeMode () {
		mode = !mode;
		UpdateMode ();
	}

	public void ChangeMusic () {
		Floor.music = !Floor.music;
		UpdateMusic ();
	}

	void UpdateMode () {
		if (mode) {
			background.GetComponent<Renderer> ().material.color = whiteColor;

			statusBar.sprite = whiteStatusBar;
			inputBar.sprite = whiteInputBar;
			keypad.sprite = whiteKeypad;

			scoreText.color = blackTextColor;
			bestScoreText.color = whiteTextColor;
			bigScoreText.color = blackTextColor;
			sumText.color = blackTextColor;
			equalText.color = blackTextColor;

			foreach (Text plusSign in plusSigns) {
				plusSign.color = blackTextColor;
			}

			Floor.sumTextColor = blackTextColor;
		} else {
			background.GetComponent<Renderer> ().material.color = blackColor;

			statusBar.sprite = blackStatusBar;
			inputBar.sprite = blackInputBar;
			keypad.sprite = blackKeypad;

			scoreText.color = whiteTextColor;
			bestScoreText.color = blackTextColor;
			bigScoreText.color = whiteTextColor;
			sumText.color = whiteTextColor;
			equalText.color = whiteTextColor;

			foreach (Text plusSign in plusSigns) {
				plusSign.color = whiteTextColor;
			}

			Floor.sumTextColor = whiteTextColor;
		}
	}

	void UpdateMusic () {
		if (Floor.music) {
			musicButton.sprite = musicOnButton;
		} else {
			musicButton.sprite = musicOffButton;
		}
	}
}
