using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "Robot", menuName = "GameData/Robot", order = 1)]
public class RobotData : ScriptableObject {

	public int Unique_ID;
	public GameObject Robot_Prefab;
	public Sprite Start_Normal_Card;
    public Sprite Upgrade_Normal_Card;
	public Sprite Start_Highlighted_Card;
	public Sprite Upgrade_Highlighted_Card;
	public int Strength;
	public int Upgrade;
	public int[] Ability_Array = new int[9];
}
