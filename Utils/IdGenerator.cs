using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdGenerator : MonoBehaviour
{
    private static long id = 0L;
    public static long GetId()
    {
        return id++;
    }
}
