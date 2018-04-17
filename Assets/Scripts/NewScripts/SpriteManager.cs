using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour {

	public static SpriteManager Instance;

	public Sprite Curve_1;
	public Sprite Curve_2;
	public Sprite Curve_3;
	public Sprite Curve_4;
	public Sprite Quad_1;
	public Sprite Quad_2;
	public Sprite Quad_3;
	public Sprite Quad_4;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (this.gameObject);
		}
	}
}
