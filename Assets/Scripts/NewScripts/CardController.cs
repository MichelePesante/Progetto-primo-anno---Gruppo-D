using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

	public RobotData Data;

	private Vector3 StartingPosition;
	private Vector3 StartingScale;

	private Image Card_Image;

	private Sprite Start_Normal_Sprite;
	private Sprite Start_Highlighted_Sprite;
    private Sprite Upgrade_Normal_Sprite;
	private Sprite Upgrade_Highlighted_Sprite;

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
                if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.placing)
                {
                    Card_Image.sprite = Start_Highlighted_Sprite;
                }
                else if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade)
                {
                    Card_Image.sprite = Upgrade_Highlighted_Sprite;
                }
                    if (TurnManager.Instance.CurrentPlayerTurn == PlayerTurn.Curve_Turn)
					transform.position = StartingPosition + CardManager.Instance.IncPosition;
				else if (TurnManager.Instance.CurrentPlayerTurn == PlayerTurn.Quad_Turn)
					transform.position = StartingPosition - CardManager.Instance.IncPosition;
				transform.localScale = StartingScale + CardManager.Instance.IncScale;
			} 
			else {
                if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.placing)
                {
                    Card_Image.sprite = Start_Normal_Sprite;
                }
                else if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade)
                {
                    Card_Image.sprite = Upgrade_Normal_Sprite;
                }
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
		Start_Normal_Sprite = Data.Start_Normal_Card;
		Start_Highlighted_Sprite = Data.Start_Highlighted_Card;
        Upgrade_Normal_Sprite = Data.Upgrade_Normal_Card;
        Upgrade_Highlighted_Sprite = Data.Upgrade_Highlighted_Card;
    }
}