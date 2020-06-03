using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoving : MonoBehaviour
{

    public Movement movement;
    private Vector3 lastPos;
    public bool isMinimap;

    public void Start()
    {
        lastPos = Vector3.up;
    }

    public void Update()
    {
        // getting player actual position
        Vector3 playerPos = movement.transform.position;
        if (lastPos != playerPos)
        {
            // getting camera position
            Vector3 actualPos = transform.position;

            // changing camera x and z axes values (we do not change y values becase we want to keep the camera at fixed height)
            actualPos.x = movement.transform.position.x;
            actualPos.z = movement.transform.position.z;
            transform.position = actualPos;
            lastPos = actualPos;
        }
        if (isMinimap)
        {
            // changing minimap camera rotation
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, movement.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) ;
        }
    }

}
