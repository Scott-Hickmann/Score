using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floor : MonoBehaviour {

	public Transform numbers;

	public static List<GameObject> bottomRow = new List<GameObject>();
	static public int rowAmount;
	private int addNumbersAmount;
	public static int numbersAmount;
	public int[] addRowBigScores;
	public int[] addNumbersBigScores;

	private int sum;
	private string sumInput = "??";
	public static int score;
	public static int bestScore;

	public Text sumText;
	public static Color sumTextColor;
	public Text bigScoreText;
	public Text bestScoreText;

	static public bool music = true;
	public AudioClip correctSound;
	public AudioClip errorSound;
	public AudioClip levelUpSound;
	private AudioSource source;
	private float volume = 0.5f;
	public float errorBlinkDelay;

	private int nextBigScore;
	public static int previousBigScore;

	public GameObject[] plusSigns;



	public float[] addRowDelays;

	public float defaultRoundDelay;
	private float roundDelay;

	public float defaultGenerationDelay;
	private float generationDelay;

	private int n = 0;

	private int newRowAmount;

	public GameObject number;
	public GameObject numbersParent;
	private Transform clone;

	static public float width;
	static public float height;

	void Start () {
		bigScoreText.CrossFadeAlpha(0.0f, 0.0f, false);
		source = GetComponent<AudioSource>();
		rowAmount = 2;
		addNumbersAmount = 1;
		numbersAmount = 10;
		plusSigns [rowAmount - 2].SetActive (true);
		/*for (int n = 0; n < addRowBigScores.Length; n++) {
			if (previousBigScore >= addRowBigScores [n]) {
				rowAmount += 1;
				plusSigns [rowAmount - 1].SetActive (true);
			}
		}*/
		score = 0;//previousBigScore;
		sum = 0;

		height = Camera.main.orthographicSize * 2;
		width = height * Screen.width/Screen.height;
		roundDelay = defaultRoundDelay;
		generationDelay = defaultGenerationDelay;
		//StartCoroutine (ManageDelay ());
		StartCoroutine (Generate ());
        StartCoroutine(KeyboardManager ());
	}

	public void inputDigit (int digit) {
		if (sumInput.Length < 3) {
			if (sumInput == "??") {
				sumInput = "";
			}
			sumInput += "" + digit;
			sumText.color = sumTextColor;
		}
    }

    IEnumerator KeyboardManager()
    {
        while (true) {
            for (int digit = 0; digit < 10; digit++)
            {
                if (Input.GetKeyDown("" + digit))
                {
                    inputDigit(digit);
                }
            }
            if (Input.GetKeyDown("backspace"))
            {
                delete();
            }
            if (Input.GetKeyDown("return"))
            {
                validate();
            }
            yield return null;
        }
    }

    public void delete () {
		if (sumInput != "??") {
			sumInput = sumInput.Substring (0, sumInput.Length - 1);
			if (sumInput == "") {
				sumInput = "??";
			}
		}
	}

	public void validate () {
		if (bottomRow.Count == rowAmount) {
			if (int.Parse (sumInput) == sum) {
				nextBigScore = Mathf.RoundToInt(Mathf.Ceil ((score + 1) / 10f) * 10);
				if (score + 1 >= nextBigScore) {
					previousBigScore = nextBigScore;
					if (music) {
						source.PlayOneShot (levelUpSound, volume);
					}
					StartCoroutine (ShowBigScore ());
                    Debug.Log(previousBigScore - addNumbersBigScores[addNumbersAmount - 1]);
                    Debug.Log(addRowBigScores[rowAmount]);
                    Debug.Log(previousBigScore - addNumbersBigScores[addNumbersAmount - 1] == addRowBigScores[rowAmount]);

                    if (previousBigScore - addNumbersBigScores[addNumbersAmount - 1] == addRowBigScores[rowAmount]) {
						rowAmount += 1;
						plusSigns [rowAmount - 2].SetActive (true);
						Reset ();
						foreach (Transform number in numbers) {
							//Destroy (number.gameObject);
							number.gameObject.GetComponent<CircleCollider2D> ().isTrigger = true;
						}
					}
					if (previousBigScore == addNumbersBigScores[addNumbersAmount]) {
						addNumbersAmount += 1;
						numbersAmount += 10;
						Reset ();
						rowAmount = 2;
						foreach (GameObject plusSign in plusSigns) {
							plusSign.SetActive (false);
						}
						plusSigns [rowAmount - 2].SetActive (true);
						foreach (Transform number in numbers) {
							//Destroy (number.gameObject);
							number.gameObject.GetComponent<CircleCollider2D> ().isTrigger = true;
						}
					}
				} else {
					if (music) {
						source.PlayOneShot (correctSound, volume);
					}
				}
				foreach (GameObject bottomNumber in bottomRow) {
					bottomNumber.GetComponent<CircleCollider2D> ().isTrigger = true;
				}
				score += 1;//sum;
				if (roundDelay > defaultGenerationDelay) {
					roundDelay -= 0.1f;
				} else {
					generationDelay -= 0.02f;
					roundDelay = generationDelay;
				}
				//Debug.Log ("Round Delay: " + roundDelay + " Generation Delay: " + generationDelay);
				bottomRow.Clear ();
				sum = 0;
				sumInput = "??";
			} else {
				StartCoroutine (Error ());
			}
		}
	}

	IEnumerator ShowBigScore () {
		bigScoreText.text = "" + nextBigScore;
		bigScoreText.CrossFadeAlpha(1, 0.2f, false);
		yield return new WaitForSeconds(1);
		bigScoreText.CrossFadeAlpha(0, 0.4f, false);
	}

	IEnumerator Error() {
		if (music) {
			source.PlayOneShot (errorSound, volume);
		}
		for (int n = 0; n < 2; n++) {
			sumText.color = Color.red;
			yield return new WaitForSeconds (errorBlinkDelay);
			sumText.color = sumTextColor;
			yield return new WaitForSeconds (errorBlinkDelay / 2);
		}
		sumInput = "??";
	}

	void OnCollisionEnter2D (Collision2D other) {
		sum += other.gameObject.GetComponent<Number> ().number;
		bottomRow.Add (other.gameObject);
		// AI
		/*if (bottomRow.Count == rowAmount) {
			sumInput = "" + sum;
			validate ();
		}*/
	}

	void Update () {
		if (score > bestScore) {
			bestScore = score;
		}
		if (sumInput == "??") {
			sumText.color = bestScoreText.color;
		}
		sumText.text = sumInput;
	}



	void Reset () {
		generationDelay = defaultGenerationDelay;
		roundDelay = defaultRoundDelay;
		n = 0;
	}

	/*IEnumerator ManageDelay() {
		generationDelay = defaultGenerationDelay;
		roundDelay = defaultRoundDelay;
		while (roundDelay > defaultGenerationDelay) {
			yield return new WaitForSeconds (1);
			roundDelay -= 0.03f;
			Debug.Log ("Round Delay: " + roundDelay + "Generation Delay: " + generationDelay);
		}
		roundDelay = defaultGenerationDelay;
		while (roundDelay <= defaultGenerationDelay) {
			yield return new WaitForSeconds (1);
			generationDelay -= 0.03f;
			roundDelay -= 0.03f;
			Debug.Log ("Round Delay: " + roundDelay + "Generation Delay: " + generationDelay);
		}
	}*/

	IEnumerator Generate() {
		while (true) {
			n = 0;
			while (n < Floor.rowAmount) {
				clone = Instantiate (number.transform, numbersParent.transform);
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
