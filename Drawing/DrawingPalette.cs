using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPalette : MonoBehaviour
{
    private List<Brush> brushesList;
    private int actualBrushSelected;
    private Color actualColor;
    public string brushesFolder;
    public bool colorPickerEnabled { get; set; }
    public bool eraserEnabled { get; set; }
    public UIController uIController;
    public bool paletteEnabled { get; set; }

    public void Start()
    {
        actualBrushSelected = 0;
        brushesList = new List<Brush>();
        // adding default brushes
        AddBrush(DefaultBrushes.GetCircleBrush());
        AddBrush(DefaultBrushes.GetSquareBrush());
        AddBrush(DefaultBrushes.GetTriangleBrush());
        colorPickerEnabled = false;
        eraserEnabled = false;
        paletteEnabled = false;
        // importing brushes from resources
        BrushImporter.ImportBrushes(brushesFolder, this);
        uIController.Init();
    }

    public Brush GetBrush()
    {
        return brushesList[actualBrushSelected];
    }

    public void AddBrush(Brush brush)
    {
        brushesList.Add(brush);
    }

    public Color GetColor()
    {
        return actualColor;
    }

    public void SetColor(Color color)
    {
        actualColor.r = color.r;
        actualColor.g = color.g;
        actualColor.b = color.b;
        actualColor.a = 1f;
    }

    public void SwitchBrush(int brushPositionNum)
    {
        actualBrushSelected = brushPositionNum;
    }

    public List<Brush> GetBrushes()
    {
        return brushesList;
    }

}
