using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

    public static ArrowManager Instance;

    [Header("Contentitori Frecce")]
    public GameObject Frecce;
    public GameObject Frecce_Curve;
    public GameObject Frecce_Quad;

    [Header("Frecce Curvi")]
    public GameObject Freccia_Nord_Curve;
    public GameObject Freccia_Nord_Est_Curve;
    public GameObject Freccia_Est_Curve;
    public GameObject Freccia_Sud_Est_Curve;
    public GameObject Freccia_Sud_Curve;
    public GameObject Freccia_Sud_Ovest_Curve;
    public GameObject Freccia_Ovest_Curve;
    public GameObject Freccia_Nord_Ovest_Curve;

    [Header("Frecce Quadrati")]
    public GameObject Freccia_Nord_Quad;
    public GameObject Freccia_Nord_Est_Quad;
    public GameObject Freccia_Est_Quad;
    public GameObject Freccia_Sud_Est_Quad;
    public GameObject Freccia_Sud_Quad;
    public GameObject Freccia_Sud_Ovest_Quad;
    public GameObject Freccia_Ovest_Quade;
    public GameObject Freccia_Nord_Ovest_Quad;

    private JoystickManager jm;

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
        jm = JoystickManager.Instance;
	}
	
	void Update () {
		
	}
}
