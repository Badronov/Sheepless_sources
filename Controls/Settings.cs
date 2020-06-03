using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool minimapEnabled { get; set; }
    public UIController uIController;
    public Text moveForwardValue;
    public Text moveBackValue;
    public Text moveLeftValue;
    public Text moveRightValue;
    public Text rotateLeftValue;
    public Text rotateRightValue;
    public Text accelerationValue;

    public void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
        if (joysticksInverted) 
        {   
            SetJoysticksInverted(true);
        } else 
        {
            SetJoysticksInverted(false);
        }
        accelerationValue.text = "-";
#else
        moveForwardValue.text = "W";
        moveBackValue.text = "S";
        moveLeftValue.text = "A";
        moveRightValue.text = "D";
        rotateLeftValue.text = "Q";
        rotateRightValue.text = "E";
        accelerationValue.text = "Left Shift";
#endif
        minimapEnabled = false;
    }

    public void SetJoysticksInverted(bool state)
    {
        if (state)
        {
            moveForwardValue.text = "Right joystick";
            moveBackValue.text = "Right joystick";
            moveLeftValue.text = "Right joystick";
            moveRightValue.text = "Right joystick";
            rotateLeftValue.text = "Left joystick";
            rotateRightValue.text = "Left joystick";
        } else
        {
            moveForwardValue.text = "Left joystick";
            moveBackValue.text = "Left joystick";
            moveLeftValue.text = "Left joystick";
            moveRightValue.text = "Left joystick";
            rotateLeftValue.text = "Right joystick";
            rotateRightValue.text = "Right joystick";
        }
    }
}
