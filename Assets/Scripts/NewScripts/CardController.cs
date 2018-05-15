using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

	public RobotData Data;

	private Image Card_Image;

	private Sprite Normal_Sprite;
	private Sprite Highlighted_Sprite;

	private bool isHighlighted;

	public bool hasBeenPlaced;

	void Start () {
		Card_Image = GetComponent<Image> ();
		isHighlighted = false;
		hasBeenPlaced = false;
	}

	void Update () {
		if (hasBeenPlaced) {
			Card_Image.color = new Color (1f, 1f, 1f, 0f);
		}
		else {
			Card_Image.color = new Color (1f, 1f, 1f, 1f);
			if (isHighlighted) {
				Card_Image.sprite = Highlighted_Sprite;
			} 
			else {
				Card_Image.sprite = Normal_Sprite;
			}
		}
	}

	public void SetData (RobotData _newData) {
		Data = _newData;
	}

	public void RefreshCards () {
		if (Data == null) {
			return;
		}
		Normal_Sprite = Data.Normal_Card;
		Highlighted_Sprite = Data.Highlighted_Card;
	}
}