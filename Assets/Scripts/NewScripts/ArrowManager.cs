using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{

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
    public GameObject Freccia_Ovest_Quad;
    public GameObject Freccia_Nord_Ovest_Quad;

    public Vector3 Frecce_Curve_Starting_Position;
    public Vector3 Frecce_Curve_Starting_Scale;
    public Vector3 Frecce_Quad_Starting_Position;
    public Vector3 Frecce_Quad_Starting_Scale;

    public Material HighlightMaterial;

    private Material StartingMaterial;
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

    void Start()
    {
        jm = JoystickManager.Instance;

        Frecce_Curve_Starting_Position = Frecce_Curve.transform.position;
        Frecce_Curve_Starting_Scale = Frecce_Curve.transform.localScale;
        Frecce_Quad_Starting_Position = Frecce_Quad.transform.position;
        Frecce_Quad_Starting_Scale = Frecce_Quad.transform.localScale;

        StartingMaterial = Freccia_Nord_Curve.GetComponent<Renderer>().material;
    }

    public void ResetAllCurveMaterials()
    {
        Freccia_Nord_Curve.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Nord_Est_Curve.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Est_Curve.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Sud_Est_Curve.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Sud_Curve.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Sud_Ovest_Curve.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Ovest_Curve.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Nord_Ovest_Curve.GetComponent<Renderer>().material = StartingMaterial;
    }

    public void ResetAllQuadMaterials()
    {
        Freccia_Nord_Quad.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Nord_Est_Quad.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Est_Quad.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Sud_Est_Quad.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Sud_Quad.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Sud_Ovest_Quad.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Ovest_Quad.GetComponent<Renderer>().material = StartingMaterial;
        Freccia_Nord_Ovest_Quad.GetComponent<Renderer>().material = StartingMaterial;
    }

    public void ActiveAllArrows() {
        Freccia_Nord_Curve.SetActive(true);
        Freccia_Nord_Est_Curve.SetActive(true);
        Freccia_Est_Curve.SetActive(true);
        Freccia_Sud_Est_Curve.SetActive(true);
        Freccia_Sud_Curve.SetActive(true);
        Freccia_Sud_Ovest_Curve.SetActive(true);
        Freccia_Ovest_Curve.SetActive(true);
        Freccia_Nord_Ovest_Curve.SetActive(true);

        Freccia_Nord_Quad.SetActive(true);
        Freccia_Nord_Est_Quad.SetActive(true);
        Freccia_Est_Quad.SetActive(true);
        Freccia_Sud_Est_Quad.SetActive(true);
        Freccia_Sud_Quad.SetActive(true);
        Freccia_Sud_Ovest_Quad.SetActive(true);
        Freccia_Ovest_Quad.SetActive(true);
        Freccia_Nord_Ovest_Quad.SetActive(true);
    }
}
