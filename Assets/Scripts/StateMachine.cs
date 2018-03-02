using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateMachine {

	public static PlayerTurn CurrentPlayerTurn;

	public static MacroPhase CurrentMacroPhase;

	public enum MacroPhase {
		Start,
		Setup,
		Core,
		End
	}

	public enum PlayerTurn {
		TurnPlayer1,
		TurnPlayer2
	}
}
