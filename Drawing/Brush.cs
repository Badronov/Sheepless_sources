using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{

    public int size { get; set; }
    public string name { get; set; }
    public List<List<bool>> pixels { get; set; }

}
