using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Camera mainCamera;
    private float speedBase;
    private float rotationSpeedBase;
    public float speed { get; set; }
    public float rotationSpeed { get; set; }

    public GameObject movePositionRight;
    public GameObject movePositionLeft;
    public GameObject movePositionForward;
    public GameObject movePositionBack;

    public Joystick leftJoystick;
    public Joystick rightJoystick;

    private float joystickLowerBound;
    private float joystickUpperBound;
    private float speedMultiplier;

    private bool joysticksInverted;

    // Start is called before the first frame update
    void Start()
    {
        if (!mainCamera)
            mainCamera = GameObject.FindObjectOfType<Camera>();
        // setting up default values
        speedBase = 30f;
        rotationSpeedBase = 1f;
        speed = 1;
        rotationSpeed = 1f;
        joystickLowerBound = 0.3f;
        joystickUpperBound = 0.95f;
        speedMultiplier = 2f;
    }

    // Update is called once per frame
    public void Update()
    {
        //calling methods depending on platform
#if UNITY_IOS || UNITY_ANDROID
        MoveOnPhone();
#else
        MoveOnPC();
#endif
    }

    public void MoveOnPhone()
    {
        // calculating rotation speed
        float actualRotationSpeed = rotationSpeedBase * rotationSpeed * speedMultiplier;
        float horizontalSpeed;
        float verticalSpeed;
        float rotation;
        // getting values from joysticks
        if (joysticksInverted)
        {
            horizontalSpeed = rightJoystick.Horizontal;
            verticalSpeed = rightJoystick.Vertical;
            rotation = leftJoystick.Horizontal;
        }
        else
        {
            horizontalSpeed = leftJoystick.Horizontal;
            verticalSpeed = leftJoystick.Vertical;
            rotation = rightJoystick.Horizontal;
        }
        
        if (!(Mathf.Abs(horizontalSpeed) <= joystickLowerBound && Mathf.Abs(verticalSpeed) <= joystickLowerBound))
        {
            // calculating speed
            float actualSpeed = speedBase * speed * speedMultiplier;
            // multiplying speed by two if joystick value is greater that upper bound
            if (horizontalSpeed >= joystickUpperBound || verticalSpeed >= joystickUpperBound)
            {
                actualSpeed *= 2;
            }
            // calling move methods depending on joystick values
            if (verticalSpeed > joystickLowerBound)
            {
                MoveForward(actualSpeed);
            }
            if (verticalSpeed < -joystickLowerBound)
            {
                MoveBack(actualSpeed);
            }
            if (horizontalSpeed < -joystickLowerBound)
            {
                MoveLeft(actualSpeed);
            }
            if (horizontalSpeed > joystickLowerBound)
            {
                MoveRight(actualSpeed);
            }
        }
        if (Mathf.Abs(rotation) > joystickLowerBound)
        {
            // rotating object depenping on joystick value
            if (rotation > 0f)
            {
                transform.Rotate(new Vector3(0f, actualRotationSpeed, 0f));
            } else
            {
                transform.Rotate(new Vector3(0f, -actualRotationSpeed, 0f));
            }
        }
    }

    public void MoveOnPC()
    {
        // calculating speed
        float actualSpeed = speedBase * speed * speedMultiplier;
        // multiplying speed by two if the left shift button had pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            actualSpeed *= 2;
        }
        // calling move methods depending on keys pressed
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward(actualSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveBack(actualSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft(actualSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight(actualSpeed);
        }
        // calculating rotation speed
        float actualRotationSpeed = rotationSpeedBase * rotationSpeed * speedMultiplier;
        // rotating object depenping on key pressed value
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0f, actualRotationSpeed, 0f));
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0f, -actualRotationSpeed, 0f));
        }
    }

    // this method moves object to the movePositionForward position
    private void MoveForward(float actualSpeed)
    {
        Vector3 newPosition = movePositionForward.transform.position;
        newPosition.x += ((newPosition.x - transform.position.x) * actualSpeed);
        newPosition.z += ((newPosition.z - transform.position.z) * actualSpeed);
        transform.position = newPosition;
    }

    // this method moves object to the movePositionBack position
    private void MoveBack(float actualSpeed)
    {
        Vector3 newPosition = movePositionBack.transform.position;
        newPosition.x += ((newPosition.x - transform.position.x) * actualSpeed);
        newPosition.z += ((newPosition.z - transform.position.z) * actualSpeed);
        transform.position = newPosition;
    }

    // this method moves object to the movePositionLeft position
    private void MoveLeft(float actualSpeed)
    {
        Vector3 newPosition = movePositionLeft.transform.position;
        newPosition.x += ((newPosition.x - transform.position.x) * actualSpeed);
        newPosition.z += ((newPosition.z - transform.position.z) * actualSpeed);
        transform.position = newPosition;
    }

    // this method moves object to the movePositionRight position
    private void MoveRight(float actualSpeed)
    {
        Vector3 newPosition = movePositionRight.transform.position;
        newPosition.x += ((newPosition.x - transform.position.x) * actualSpeed);
        newPosition.z += ((newPosition.z - transform.position.z) * actualSpeed);
        transform.position = newPosition;
    }

    public void SetJoysticksInverted(bool state)
    {
        joysticksInverted = state;
    }

}
