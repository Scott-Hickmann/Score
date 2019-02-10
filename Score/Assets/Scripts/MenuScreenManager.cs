using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenManager : MonoBehaviour {

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

	public Text scoreText;
	public Text bestScoreText;

	public Color whiteTextColor;
	public Color blackTextColor;

	void Start () {
		UpdateMode ();
		UpdateMusic ();

		modeButton.rectTransform.anchoredPosition = new Vector2 (modeButton.rectTransform.anchoredPosition.x, -statusBar.rectTransform.rect.height*0.7f);
		musicButton.rectTransform.anchoredPosition = new Vector2 (musicButton.rectTransform.anchoredPosition.x, -statusBar.rectTransform.rect.height*0.7f);
	}

	public void ChangeMode () {
		ScreenManager.mode = !ScreenManager.mode;
		UpdateMode ();
	}

	public void ChangeMusic () {
		Floor.music = !Floor.music;
		UpdateMusic ();
	}

	void UpdateMode () {
		if (ScreenManager.mode) {
			background.GetComponent<Renderer> ().material.color = whiteColor;

			statusBar.sprite = whiteStatusBar;

			scoreText.color = blackTextColor;
			bestScoreText.color = whiteTextColor;
		} else {
			background.GetComponent<Renderer> ().material.color = blackColor;

			statusBar.sprite = blackStatusBar;

			scoreText.color = whiteTextColor;
			bestScoreText.color = blackTextColor;
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
