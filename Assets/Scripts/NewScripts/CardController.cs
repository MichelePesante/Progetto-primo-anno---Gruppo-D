using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

	public RobotData Data;

	private Vector3 StartingPosition;
	private Vector3 StartingScale;

	private Image Card_Image;

	private Sprite Normal_Sprite;
	private Sprite Highlighted_Sprite;

	public bool isHighlighted;

	public bool hasBeenPlaced;

	void Start () {
		Card_Image = GetComponent<Image> ();
		StartingPosition = transform.position;
		StartingScale = transform.localScale;
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
				if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
					transform.position = StartingPosition + CardManager.Instance.IncPosition;
				else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
					transform.position = StartingPosition - CardManager.Instance.IncPosition;
				transform.localScale = StartingScale + CardManager.Instance.IncScale;
			} 
			else {
				Card_Image.sprite = Normal_Sprite;
				transform.position = StartingPosition;
				transform.localScale = StartingScale;
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