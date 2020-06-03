using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BrushImporter : MonoBehaviour
{
    
    // Start is called before the first frame update
    public static void ImportBrushes(string brushFolder, DrawingPalette drawingPalette)
    {
        // getting brush files content from resources
        TextAsset[] files =  Resources.LoadAll<TextAsset>(brushFolder);
        for (int i = 0; i < files.Length; i++) {
            BrushImporter.ImportBrush(files[i].text, drawingPalette);
        }
    }

    private static void ImportBrush(string data, DrawingPalette drawingPalette)
    {
        Brush brush = new Brush();
        brush.name = GetBrushName(data, GetBrushNameSize(data));
        brush.size = GetBrushSize(data);
        brush.pixels = GetPixels(brush.size, data.Substring(32 + brush.name.Length, brush.size * brush.size));
        drawingPalette.AddBrush(brush);
    }

    private static short GetBrushSize(string input)
    {
        string brushSizeStr = input.Substring(0, 16);
        return ReadShortFromStr(brushSizeStr);
    }

    private static short GetBrushNameSize(string input)
    {
        string brushNameSizeStr = input.Substring(16, 16);
        return ReadShortFromStr(brushNameSizeStr);
    }

    private static string GetBrushName(string input, int nameSize)
    {
        return input.Substring(32, nameSize);
    }

    // this method translates binary value written using string to short value
    private static short ReadShortFromStr(string str)
    {
        short brushSize = 0x0000;
        short mask = 0x0001;
        for (int i = 0; i < 15; i++)
        {
            if (str[i] == '1')
            {
                brushSize |= mask;
            }
            brushSize <<= 1;
        }
        if (str[15] == '1')
        {
            brushSize |= mask;
        }
        return brushSize;
    }

    // this method reading pixels array from input string and returns it as 2d array (using lists)
    private static List<List<bool>> GetPixels(int brushSize, string input)
    {
        List<List<bool>> pixelsList = new List<List<bool>>();
        for (int i = 0; i < brushSize; i++)
        {
            pixelsList.Add(new List<bool>());
        }

        for (int i = 0; i < brushSize; i++)
        {
            for (int j = 0; j < brushSize; j++)
            {
                if (input[(i * brushSize) + j] == '1')
                {
                    pixelsList[j].Add(true);
                }
                else
                {
                    pixelsList[j].Add(false);
                }
            }
        }
        return pixelsList;
    }
}
