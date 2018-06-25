using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    public Sprite Tutorial_1;
    public Sprite Tutorial_2;
    public Sprite Tutorial_3;

    private bool isOnTutorial_1;
    private bool isOnTutorial_2;
    private bool isOnTutorial_3;

    private Image tutorial_Image;

	void Start ()
    {
        tutorial_Image = GetComponent<Image>();
        isOnTutorial_1 = true;
    }

	void Update ()
    {
        if (isOnTutorial_1 && Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            tutorial_Image.sprite = Tutorial_2;
            isOnTutorial_1 = false;
            isOnTutorial_2 = true;
        }
        else if (isOnTutorial_2)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                tutorial_Image.sprite = Tutorial_1;
                isOnTutorial_2 = false;
                isOnTutorial_1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                tutorial_Image.sprite = Tutorial_3;
                isOnTutorial_2 = false;
                isOnTutorial_3 = true;
            }
        }
        else if (isOnTutorial_3 && Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            tutorial_Image.sprite = Tutorial_2;
            isOnTutorial_3 = false;
            isOnTutorial_2 = true;
        }
    }
}