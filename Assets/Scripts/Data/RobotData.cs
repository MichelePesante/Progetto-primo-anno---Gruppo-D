using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Robot", menuName = "GameData/Robot", order = 1)]
public class RobotData : ScriptableObject {

	public int UniqueID = -1;
	public int Strength;
	public int Upgrade;
	public int Abilità_1;
	public int Abilità_2;
	public int Abilità_3;
	public int Abilità_4;
	public int Abilità_5;
	public int Abilità_6;
	public int Abilità_7;
	public int Abilità_8;

	private static int maxUniqueID = 0;

	void Awake() {
		maxUniqueID++;
		UniqueID = maxUniqueID;
	}
}