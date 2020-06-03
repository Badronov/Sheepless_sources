using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushesListController : MonoBehaviour
{

    public Dropdown dropdown;
    public UIController uIController;
    
    // this method wili initialize ui dropdown object with brushes names
    public void Init(List<Brush> brushesList)
    {
        dropdown.ClearOptions();
        List<string> brushesNames = new List<string>();
        for (int i = 0; i < brushesList.Count; i++)
        {
            brushesNames.Add(brushesList[i].name);
        }
        dropdown.AddOptions(brushesNames);
    }

    public void DropdownValueChanged(int newPosition) {
        uIController.SetBrush(newPosition);
    }

}
