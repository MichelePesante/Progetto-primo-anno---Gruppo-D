using UnityEngine;
using UnityEngine.UI;

public class CommandController : MonoBehaviour
{

    public Sprite Command_1;
    public Sprite Command_2;
    public Sprite Command_3;

    private bool isOnCommand_1;
    private bool isOnCommand_2;
    private bool isOnCommand_3;

    private Image command_Image;

    void Start()
    {
        command_Image = GetComponent<Image>();
        isOnCommand_1 = true;
    }

    void Update()
    {
        if (isOnCommand_1 && Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            command_Image.sprite = Command_2;
            isOnCommand_1 = false;
            isOnCommand_2 = true;
        }
        else if (isOnCommand_2)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                command_Image.sprite = Command_1;
                isOnCommand_2 = false;
                isOnCommand_1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                command_Image.sprite = Command_3;
                isOnCommand_2 = false;
                isOnCommand_3 = true;
            }
        }
        else if (isOnCommand_3 && Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            command_Image.sprite = Command_2;
            isOnCommand_3 = false;
            isOnCommand_2 = true;
        }
    }
}