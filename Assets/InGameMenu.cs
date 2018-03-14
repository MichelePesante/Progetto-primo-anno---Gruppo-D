using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour {

    
    private bool IsActive = false;

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update () {
       
    }

    public void GameMenuActivation()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && IsActive == false)
        {

        }

    }
}
