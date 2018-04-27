using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour {

	public static SpriteManager Instance;

	[Header ("Carte nello slot")]
	public Sprite Curve_1;
	public Sprite Curve_2;
	public Sprite Curve_3;
	public Sprite Curve_4;
	public Sprite Quad_1;
	public Sprite Quad_2;
	public Sprite Quad_3;
	public Sprite Quad_4;

	[Header ("Carte evidenziate")]
	public Sprite Curve_1_1_Highlighted;
	public Sprite Curve_1_2_Highlighted;
	public Sprite Curve_1_3_Highlighted;
	public Sprite Curve_1_4_Highlighted;
	public Sprite Curve_2_1_Highlighted;
	public Sprite Curve_2_2_Highlighted;
	public Sprite Curve_2_3_Highlighted;
	public Sprite Curve_2_4_Highlighted;
	public Sprite Curve_3_1_Highlighted;
	public Sprite Curve_3_2_Highlighted;
	public Sprite Curve_3_3_Highlighted;
	public Sprite Curve_3_4_Highlighted;
	public Sprite Curve_4_1_Highlighted;
	public Sprite Curve_4_2_Highlighted;
	public Sprite Curve_4_3_Highlighted;
	public Sprite Curve_4_4_Highlighted;
	public Sprite Quad_1_1_Highlighted;
	public Sprite Quad_1_2_Highlighted;
	public Sprite Quad_1_3_Highlighted;
	public Sprite Quad_1_4_Highlighted;
	public Sprite Quad_2_1_Highlighted;
	public Sprite Quad_2_2_Highlighted;
	public Sprite Quad_2_3_Highlighted;
	public Sprite Quad_2_4_Highlighted;
	public Sprite Quad_3_1_Highlighted;
	public Sprite Quad_3_2_Highlighted;
	public Sprite Quad_3_3_Highlighted;
	public Sprite Quad_3_4_Highlighted;
	public Sprite Quad_4_1_Highlighted;
	public Sprite Quad_4_2_Highlighted;
	public Sprite Quad_4_3_Highlighted;
	public Sprite Quad_4_4_Highlighted;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (this.gameObject);
		}
	}
}
