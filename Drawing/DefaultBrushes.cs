using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class contains methods for creating default brushes
public class DefaultBrushes : MonoBehaviour
{
    public static Brush GetCircleBrush()
    {
        Brush brush = new Brush();
        brush.size = 11;
        brush.name = "Circle";
        bool [,] pixels = {
            { false, false, false, false, true, true, true, false, false, false, false },
            { false, false, true, true, true, true, true, true, true, false, false },
            { false, true, true, true, true, true, true, true, true, true, false },
            { false, true, true, true, true, true, true, true, true, true, false },
            { true, true, true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true, true, true },
            { false, true, true, true, true, true, true, true, true, true, false },
            { false, true, true, true, true, true, true, true, true, true, false },
            { false, false, true, true, true, true, true, true, true, false, false },
            { false, false, false, false, true, true, true, false, false, false, false }
        };
        List<List<bool>> pixelsList = new List<List<bool>>();
        for (int i = 0; i < brush.size; i++)
        {
            pixelsList.Add(new List<bool>());
            for (int j = 0; j < brush.size; j++)
            {
                pixelsList[i].Add(pixels[i,j]);
            }
        }
        brush.pixels = pixelsList;
        return brush;
    }

    public static Brush GetSquareBrush()
    {
        Brush brush = new Brush();
        brush.size = 7;
        brush.name = "Square";
        bool[,] pixels = {
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true }
        };
        List<List<bool>> pixelsList = new List<List<bool>>();
        for (int i = 0; i < brush.size; i++)
        {
            pixelsList.Add(new List<bool>());
            for (int j = 0; j < brush.size; j++)
            {
                pixelsList[i].Add(pixels[i, j]);
            }
        }
        brush.pixels = pixelsList;
        return brush;
    }

    public static Brush GetTriangleBrush()
    {
        Brush brush = new Brush();
        brush.size = 9;
        brush.name = "Triangle";
        bool[,] pixels = {
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, false },
            { true, true, true, true, true, true, true, false, false },
            { true, true, true, true, true, true, false, false, false },
            { true, true, true, true, true, false, false, false, false },
            { true, true, true, true, false, false, false, false, false },
            { true, true, true, false, false, false, false, false, false },
            { true, true, false, false, false, false, false, false, false },
            { true, false, false, false, false, false, false, false, false }
        };
        List<List<bool>> pixelsList = new List<List<bool>>();
        for (int i = 0; i < brush.size; i++)
        {
            pixelsList.Add(new List<bool>());
            for (int j = 0; j < brush.size; j++)
            {
                pixelsList[i].Add(pixels[i, j]);
            }
        }
        brush.pixels = pixelsList;
        return brush;
    }
}
