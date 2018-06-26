﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewUIManager : MonoBehaviour {

	public static NewUIManager Instance;
    public bool TutorialActive;
    public TextMeshProUGUI Punteggio_P1;
	public TextMeshProUGUI Punteggio_P2;
	public TextMeshProUGUI TutorialText;
	public Animator TutorialBoxAnimator;
    public Image Curve_Energy;
    public Image Quad_Energy;
	public GameObject TutorialBox;
	public GameObject Rotation_Buttons;
	public GameObject Energies;
	public GameObject Slots;
    public GameObject Segnapunti;

	// Use this for initialization
	void Awake () {
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	void Start () {
		TutorialBoxAnimator = TutorialBox.GetComponent<Animator> ();
    }

	// Update is called once per frame
	void Update () {
		Punteggio_P1.text = FindObjectOfType<TurnManager> ().ScoreCurve.ToString();
		Punteggio_P2.text = FindObjectOfType<TurnManager> ().ScoreQuad.ToString();
	}

	public void ChangeText (string _textToInsert) {
		TutorialText.text = _textToInsert;
	}

	public void TutorialBoxSummon () {
        GameManager.isTutorialOn = true;
		TutorialBoxAnimator.SetBool ("IsTutorialActive", true);
		if (TutorialBoxAnimator.GetBool("IsTutorialActive") == true)
			TutorialBoxAnimator.Play ("TutorialBox");
		TutorialBoxAnimator.SetBool ("IsTutorialActive", false);
	}

    public void TutorialHelp() {
        if (TutorialActive)
        {
            if (GameManager.isTutorialOn)
            {
                TutorialBox.transform.position = Vector3.zero;
                GameManager.isTutorialOn = false;
            }
            else
            {
                TutorialBox.transform.position = Vector3.one;
                GameManager.isTutorialOn = true;
            }
        }
    }
}
