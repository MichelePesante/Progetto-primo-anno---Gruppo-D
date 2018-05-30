using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour {

    private int discoCount;

    private bool isDiscoActive;

    public Gradient g;
    private GradientColorKey[] gck;
    private GradientAlphaKey[] gak;

    List<Color> colors = new List<Color>();

    void Start()
    {
        for (int i = 0; i < FindObjectsOfType<Light>().Length; i++)
        {
            colors.Add(new Color());
            colors[i] = FindObjectsOfType<Light>()[i].color;
        }

        g = new Gradient();
        gck = new GradientColorKey[4];
        gak = new GradientAlphaKey[2];

        gck[0].color = Color.red;
        gck[0].time = 0f;
        gck[1].color = Color.green;
        gck[1].time = 0.33f;
        gck[2].color = Color.blue;
        gck[2].time = 0.66f;
        gck[3].color = Color.red;
        gck[3].time = 1f;

        gak[0].alpha = 1f;
        gak[0].time = 0f;
        gak[1].alpha = 1f;
        gak[1].time = 1f;

        g.SetKeys(gck, gak);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.D) && discoCount == 0 && !isDiscoActive && GameMenu.GameIsPaused == false)
        {
            discoCount++;
        }
        else if (Input.GetKeyDown(KeyCode.I) && discoCount == 1 && !isDiscoActive && GameMenu.GameIsPaused == false)
        {
            discoCount++;
        }
        else if (Input.GetKeyDown(KeyCode.S) && discoCount == 2 && !isDiscoActive && GameMenu.GameIsPaused == false)
        {
            discoCount++;
        }
        else if (Input.GetKeyDown(KeyCode.C) && discoCount == 3 && !isDiscoActive && GameMenu.GameIsPaused == false)
        {
            discoCount++;
        }
        else if (Input.GetKeyDown(KeyCode.O) && discoCount == 4 && !isDiscoActive && GameMenu.GameIsPaused == false)
        {
            isDiscoActive = true;
            discoCount = 0;
        }
        else if (Input.anyKeyDown && !isDiscoActive)
        {
            discoCount = 0;
        }
        else if (Input.GetKeyDown(KeyCode.D) && discoCount == 0 && isDiscoActive && GameMenu.GameIsPaused == false)
        {
            discoCount++;
        }
        else if (Input.GetKeyDown(KeyCode.I) && discoCount == 1 && isDiscoActive && GameMenu.GameIsPaused == false)
        {
            discoCount++;
        }
        else if (Input.GetKeyDown(KeyCode.S) && discoCount == 2 && isDiscoActive && GameMenu.GameIsPaused == false)
        {
            discoCount++;
        }
        else if (Input.GetKeyDown(KeyCode.C) && discoCount == 3 && isDiscoActive && GameMenu.GameIsPaused == false)
        {
            discoCount++;
        }
        else if (Input.GetKeyDown(KeyCode.O) && discoCount == 4 && isDiscoActive && GameMenu.GameIsPaused == false)
        {
            isDiscoActive = false;
            discoCount = 0;
        }
        else if (Input.anyKeyDown && isDiscoActive)
        {
            discoCount = 0;
        }

        if (isDiscoActive)
        {
            Discoooooo();
        }
        else {
            HomeSweetHome();
        }
    }

    private void Discoooooo()
    {
        foreach (Light light in FindObjectsOfType<Light>())
        {
            float t = Mathf.Repeat(Time.time, 0.5f) / 0.5f;
            light.color = g.Evaluate(t);
        }
    }

    private void HomeSweetHome() {
        for (int i = 0; i < colors.Count; i++)
        {
            FindObjectsOfType<Light>()[i].color = colors[i];
        }
    }
}