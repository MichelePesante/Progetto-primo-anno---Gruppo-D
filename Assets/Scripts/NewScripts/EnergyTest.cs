using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EnergyTest : MonoBehaviour {

    public bool TurnPlayer1 = true;
    public GameObject RedPlayerUI;
    public GameObject BluePlayerUI;
    public TextMeshProUGUI TurnText;
    public int P1EnergyValue;
    public int P2EnergyValue;
    private int MinEnergyValue = 0;
    private int MaxEnergyValue = 5;
    public GameObject P1Totem;
    public GameObject P2Totem;
    private Quaternion TotemRotation;
    private float smooth = 1f;

    // Use this for initialization
    void Start()
    {
        TurnPlayer1 = true;
        RedPlayerUI.SetActive(true);
        BluePlayerUI.SetActive(false);
        TurnText.text = "Turno Player 1";
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1) && TurnPlayer1 == false )
        {
            if (P1EnergyValue < MaxEnergyValue)
            {
                TurnPlayer1 = true;
                RedPlayerUI.SetActive(true);
                BluePlayerUI.SetActive(false);
                TurnText.text = "Turno Player 1";
                P1EnergyValue = P1EnergyValue + 1;
            }

            else if (P1EnergyValue == MaxEnergyValue)
            {
                TurnPlayer1 = true;
                RedPlayerUI.SetActive(true);
                BluePlayerUI.SetActive(false);
                TurnText.text = "Turno Player 1";
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1) && TurnPlayer1 == true)
        {
            if (P2EnergyValue < MaxEnergyValue)
            {
                TurnPlayer1 = false;
                RedPlayerUI.SetActive(false);
                BluePlayerUI.SetActive(true);
                TurnText.text = "Turno Player 2";
                P2EnergyValue = P2EnergyValue + 1;
            }

            if (P2EnergyValue == MaxEnergyValue)
            {
                TurnPlayer1 = false;
                RedPlayerUI.SetActive(false);
                BluePlayerUI.SetActive(true);
                TurnText.text = "Turno Player 2";
            }
        }
        
       


        
    }

    public void Energy()
    {
        if (TurnPlayer1 == true && Input.GetKeyDown(KeyCode.Space))
        {
            TotemRotation = P1Totem.transform.rotation;
            TotemRotation *= Quaternion.AngleAxis(45, Vector3.up);
        }
       
       else if (TurnPlayer1 == false && Input.GetKeyDown(KeyCode.Space))
       {
            TotemRotation = P2Totem.transform.rotation;
            TotemRotation *= Quaternion.AngleAxis(45, Vector3.up);
       }
        transform.rotation = Quaternion.Lerp(transform.rotation, TotemRotation, 4 * smooth * Time.deltaTime);
    }

   

}
