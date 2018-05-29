using UnityEngine;

public class TurnableGrid3x3<T> {

	/// <summary>
	/// numero degli elementi contenuti nella griglia sia sulle ascisse che sulle ordinate
	/// </summary>
	public const int GRID_DIM = 3;

	/// <summary>
	/// griglia di gioco
	/// </summary>
	private T[,] grid = new T[GRID_DIM, GRID_DIM];
	/// <summary>
	/// indica la rotazione della griglia in senso antiorario
	/// 0 --> 0 gradi
	/// 1 --> 90 gradi
	/// 2 --> 180 gradi
	/// 3 --> 270 gradi
	/// </summary>
	private int rotationStatus = 0;
	/// <summary>
	/// matrice contenente gli indici per gestire grid in base a rotationStatus
	/// </summary>
	private int[,,,] transformationMatrix = new int[4, GRID_DIM, GRID_DIM, 2] { // indici rotationStatus 0
																{ { {0,0}, {0,1}, {0,2} },
																  { {1,0}, {1,1}, {1,2} },
																  { {2,0}, {2,1}, {2,2} }, },
																  // indici rotationStatus 1
																{ { {0,2}, {1,2}, {2,2} },
																  { {0,1}, {1,1}, {2,1} },
																  { {0,0}, {1,0}, {2,0} }, },
																  // indici rotationStatus 2
																{ { {2,2}, {2,1}, {2,0} },
																  { {1,2}, {1,1}, {1,0} },
																  { {0,2}, {0,1}, {0,0} }, },
																  // indici rotationStatus 3
																{ { {2,0}, {1,0}, {0,0} },
																  { {2,1}, {1,1}, {0,1} },
																  { {2,2}, {1,2}, {0,2} },
																} };



	/// <summary>
	/// accede al valore contenuto nell elemento i,j
	/// </summary>
	/// <param name="y"></param>
	/// <param name="x"></param>
	/// <returns>valore contenuto nell elemento i,j</returns>
	public T this[int y, int x] {
		get {
			if (y >= 0 && y < GRID_DIM && x >= 0 && x < GRID_DIM) {
				return grid[transformationMatrix[rotationStatus, y, x, 0], transformationMatrix[rotationStatus, y, x, 1]];
			} else {
				return default(T);
			}
		}
		set {
			if (y >= 0 && y < GRID_DIM && x >= 0 && x < GRID_DIM) {
				grid[transformationMatrix[rotationStatus, y, x, 0], transformationMatrix[rotationStatus, y, x, 1]] = value;
			}
		}
	}
										
	/// <summary>
	/// ruota la griglia di 90 gradi in senso antiorario "step" volte; se "step" è negativo ruota in senso antiorario
	/// </summary>
	/// <param name="step">numero di rotazioni da effettuare</param>
	public void Rotate(int step) {
		rotationStatus = (int)Mathf.Repeat(rotationStatus + step, GRID_DIM);
	}

	public int GetLength (int _index) {
		return grid.GetLength (_index);
	}

    public void ResetRotationStatus() {
        rotationStatus = 1;
    }
}
