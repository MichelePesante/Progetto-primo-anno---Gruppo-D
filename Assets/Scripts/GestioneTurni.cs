using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestioneTurni : MonoBehaviour
{
    public int TurnCount = 1;
    public Text TimerText;
    public Text TurnCountText;
    private float startTime;
    private void Start()
    {
        startTime = 20.5f;
        TurnCount = 0;
    }
    private void Update()
    {
        AlternanzaTurni();
    }



    void AlternanzaTurni()
    {
        if (startTime - Time.time > 0)
        {
            float t = startTime - Time.time;
            string seconds = (t % 60).ToString("f0");
            TimerText.text = seconds;
        }
        else if (startTime - Time.time == 0)
        {
            //il timer si ferma ma non si resetta

            float t = startTime;
            string seconds = "20.5";
            TimerText.text = seconds;
            int c = TurnCount;
            string turn = (c + 1).ToString();
            TurnCountText.text = turn;
            

        }

    }
}
