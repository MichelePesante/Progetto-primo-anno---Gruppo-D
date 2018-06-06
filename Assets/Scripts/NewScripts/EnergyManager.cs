using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {

    public static EnergyManager Instance;

    private int maxEnergyValue = 5;
    private int Curve_Energy = 2;
    private int Quad_Energy = 2;

    [Header("Energy images")]
    public Sprite Curve_Energy_0;
    public Sprite Curve_Energy_1;
    public Sprite Curve_Energy_2;
    public Sprite Curve_Energy_3;
    public Sprite Curve_Energy_4;
    public Sprite Curve_Energy_5;
    public Sprite Quad_Energy_0;
    public Sprite Quad_Energy_1;
    public Sprite Quad_Energy_2;
    public Sprite Quad_Energy_3;
    public Sprite Quad_Energy_4;
    public Sprite Quad_Energy_5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start () {
        Curve_Energy = 2;
        Quad_Energy = 2;
        RefreshEnergy();
	}
	
	void Update () {
		
	}

    public void AddCurveEnergy(int energyToAdd)
    {
        if (Curve_Energy + energyToAdd >= maxEnergyValue)
            Curve_Energy = maxEnergyValue;
        else
            Curve_Energy += energyToAdd;
    }

    public void AddQuadEnergy(int energyToAdd)
    {
        if (Quad_Energy + energyToAdd >= maxEnergyValue)
            Quad_Energy = maxEnergyValue;
        else
            Quad_Energy += energyToAdd;
    }

    public void SubCurveEnergy(int energyToSub)
    {
        if (Curve_Energy > energyToSub && Curve_Energy - energyToSub <= 0)
            Curve_Energy = 0;
        else if (energyToSub > Curve_Energy)
            Curve_Energy = 0;
        else
            Curve_Energy -= energyToSub;
    }

    public void SubQuadEnergy(int energyToSub)
    {
        if (Quad_Energy > energyToSub && Quad_Energy - energyToSub <= 0)
            Quad_Energy = 0;
        else if (energyToSub > Quad_Energy)
            Quad_Energy = 0;
        else
            Quad_Energy -= energyToSub;
    }

    public void RefreshEnergy()
    {
        switch (Curve_Energy) {
            case 0:
                NewUIManager.Instance.Curve_Energy.sprite = Curve_Energy_0;
                break;
            case 1:
                NewUIManager.Instance.Curve_Energy.sprite = Curve_Energy_1;
                break;
            case 2:
                NewUIManager.Instance.Curve_Energy.sprite = Curve_Energy_2;
                break;
            case 3:
                NewUIManager.Instance.Curve_Energy.sprite = Curve_Energy_3;
                break;
            case 4:
                NewUIManager.Instance.Curve_Energy.sprite = Curve_Energy_4;
                break;
            case 5:
                NewUIManager.Instance.Curve_Energy.sprite = Curve_Energy_5;
                break;
            default:
                break;
        }
        switch (Quad_Energy)
        {
            case 0:
                NewUIManager.Instance.Quad_Energy.sprite = Quad_Energy_0;
                break;
            case 1:
                NewUIManager.Instance.Quad_Energy.sprite = Quad_Energy_1;
                break;
            case 2:
                NewUIManager.Instance.Quad_Energy.sprite = Quad_Energy_2;
                break;
            case 3:
                NewUIManager.Instance.Quad_Energy.sprite = Quad_Energy_3;
                break;
            case 4:
                NewUIManager.Instance.Quad_Energy.sprite = Quad_Energy_4;
                break;
            case 5:
                NewUIManager.Instance.Quad_Energy.sprite = Quad_Energy_5;
                break;
            default:
                break;
        }
    }
}