using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTest : MonoBehaviour {

    public int RedEnergy;
    public int BlueEnergy;
    public int MaxEnergy = 5;
    public GameObject RedBar;
    public GameObject BlueBar;
    public Texture[] RedBarsUI;
    public Texture[] BlueBarsUI;
    public int currentTextureRed;
    public int currentTextureBlue;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTextureRed++;
            currentTextureRed %= RedBarsUI.Length;
            RedBar.GetComponent<Renderer>().material.mainTexture = RedBarsUI[currentTextureRed];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTextureBlue++;
            currentTextureBlue %= BlueBarsUI.Length;
            BlueBar.GetComponent<Renderer>().material.mainTexture = BlueBarsUI[currentTextureBlue];
        }
	}
}
