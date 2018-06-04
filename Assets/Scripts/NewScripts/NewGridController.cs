using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGridController : MonoBehaviour {

	[Header ("Dimensioni Griglia")]
	public int X;
	public int Y;
	public float Offset = 1.4f;

	[Header ("Prefabs")]
	public GameObject ColliderPrefab;
	public GameObject TilePrefab;

	[Header ("Containers")]
	public GameObject CollidersContainer;
	public GameObject CurveTilesContainer;
	public GameObject QuadTilesContainer;

	[Header ("Rotation Buttons")]
	public GameObject CurveClockwiseRotationButton;
	public GameObject CurveCounterclockwiseRotationButton;
	public GameObject QuadClockwiseRotationButton;
	public GameObject QuadCounterclockwiseRotationButton;
	public GameObject EndRotationButton;

	public GameObject ColliderObject;
    public ColliderController Collider;

	public List<CellData> Cells = new List<CellData>();
    public List<ColliderController> Colliders = new List<ColliderController>();
		
	private void CreateGraphic (int _x, int _y, float _offset, GameObject _parent) {
		for (int i = 0; i < _x; i++) {
			for (int c = 0; c < _y; c++) {
				CellData cellCheck = FindCell (i, c);
				if (cellCheck.IsValid) {
					Instantiate (TilePrefab, new Vector3 ((TilePrefab.transform.localScale.x + _offset) * i, transform.position.y, (TilePrefab.transform.localScale.x + _offset) * c), transform.rotation, _parent.transform);
				}
			}
		}
	}

	private void CreateColliders (int _x, int _y, float _offset) {
		for (int i = 0; i < _x; i++) {
			for (int c = 0; c < _y; c++) {
				CellData cellCheck = FindCell (i, c);
				if (cellCheck.IsValid) {
                    ColliderObject = Instantiate (ColliderPrefab, new Vector3 ((ColliderPrefab.transform.localScale.x + _offset) * i, transform.position.y, (ColliderPrefab.transform.localScale.x + _offset) * c), transform.rotation, CollidersContainer.transform);
                    ColliderObject.GetComponent<ColliderController>().SetPosition (i, c, new Vector3 ((ColliderPrefab.transform.localScale.x + _offset) * i, transform.position.y, (ColliderPrefab.transform.localScale.x + _offset) * c));
                    Collider = ColliderObject.GetComponent<ColliderController>();
                    Colliders.Add(Collider);
				}
			}
		}
	}

	private CellData FindCell (int _x, int _y) {
		return (Cells.Find (c => c.X == _x && c.Y == _y));
	}

	private void RemoveCell (int _x, int _y) {
		FindCell (_x, _y).SetValidity (false);
	}

	private void SetGridPosition (GameObject _gridToSet) {
		_gridToSet.transform.localPosition = new Vector3 (2.4f, 0f, 12f);
	}

	#region API

	public void CreateGrid (int _x, int _y, float _offset) {
		for (int i = 0; i < _x; i++) {
			for (int c = 0; c < _y; c++) {
				Cells.Add (new CellData (i, c, new Vector3 ((ColliderPrefab.transform.localScale.x + _offset) * i, transform.position.y, (ColliderPrefab.transform.localScale.x + _offset) * c)));
			}
		}

		RemoveCell (1, 1);
		RemoveCell (0, 3);
		RemoveCell (1, 3);
		RemoveCell (2, 3);
		RemoveCell (1, 5);

		CreateGraphic (3, 3, _offset, CurveTilesContainer);
		CreateGraphic (3, 3, _offset, QuadTilesContainer);
		CreateColliders (_x, _y, _offset);

		SetGridPosition (QuadTilesContainer);
	}

	public Vector3 GetWorldPosition (int _x, int _y) {
		foreach (CellData cell in Cells) {
			if (cell.X == _x && cell.Y == _y) {
				return cell.WorldPosition;
			}
		}
		return Vector3.zero;
	}

	#endregion
}
