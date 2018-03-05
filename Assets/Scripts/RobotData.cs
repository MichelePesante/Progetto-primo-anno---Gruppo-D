using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Robot", menuName = "GameData/Robots", order = 1)]
public class RobotData : ScriptableObject {

	public int UniqueID = -1;
	public int X;
	public int Y;
	public int Strength;
	public int Upgrade;
	public int Abilità_1;
	public int Abilità_2;
	public int Abilità_3;
	public int Abilità_4;

	private static int maxUniqueID = 0;

	void Awake() {
		maxUniqueID++;
		UniqueID = maxUniqueID;
	}
}