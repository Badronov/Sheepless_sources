using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public DrawingPalette drawingPalette;
    public Image colorImage;
    public Toggle colorPickerCheckbox;
    public Toggle eraserCheckbox;
    public Toggle minimapEnabledCheckbox;
    public BrushesListController brushesListController;
    public GameObject settingsMenu;
    public GameObject menu;
    public GameObject drawingTools;
    public GameObject drawingToolsButton;
    public GameObject minimap;
    public GameObject mapCamera;
    public GameObject mapButton;
    public GameObject joysticks;
    public GameObject invertJoystickCheckbox;
    public Movement movement;
    public Settings settings;
    public bool isMenuLevel;    

    public void Init()
    {
#if UNITY_IOS || UNITY_ANDROID
        SetInvertJoysticks(invertJoystickCheckbox.GetComponent<Toggle>().isOn);
#else
        // disabling android version ui components on pc version
        invertJoystickCheckbox.SetActive(false);
        joysticks.SetActive(false);
        mapButton.SetActive(false);
#endif
        colorPickerCheckbox.isOn = false;
        eraserCheckbox.isOn = false;
        // initializing brushes list
        brushesListController.Init(drawingPalette.GetBrushes());
    }

    public void SetColor(Color color)
    {
        drawingPalette.SetColor(color);
        colorImage.color = color;
    }

    // this method will set color picker on ui
    public void SetColorPicker(bool state)
    {
        if (state)
        {
            // disabling eraser checkbox
            if (drawingPalette.eraserEnabled)
            {
                eraserCheckbox.isOn = false;
            }
            // changing button image
            drawingToolsButton.GetComponent<DrawingToolsButton>().SetColorPickerImage();
        } else
        {
            // changing button image
            drawingToolsButton.GetComponent<DrawingToolsButton>().SetBrushImage();
        }
        drawingPalette.colorPickerEnabled = state;
    }

    // this method will set eraser on ui
    public void SetEraser(bool state)
    {
        if (state)
        {
            // disabling color picker checkbox
            if (drawingPalette.colorPickerEnabled)
            {
                colorPickerCheckbox.isOn = false;
            }
            // changing button image
            drawingToolsButton.GetComponent<DrawingToolsButton>().SetEraserImage();
        } else
        {
            // changing button image
            drawingToolsButton.GetComponent<DrawingToolsButton>().SetBrushImage();
        }
        drawingPalette.eraserEnabled = state;
    }

    public void SetBrush(int brushPositionNum)
    {
        drawingPalette.SwitchBrush(brushPositionNum);
    }

    public void SetPalette(bool state)
    {
        drawingPalette.paletteEnabled = state;
    }

    public void StartGame()
    {
        // loading first scene
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchSettingsEnabled()
    {
        // if settings menu object is enabled
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
        } else
        {
            settingsMenu.SetActive(true);
        }
    }

    public void SwitchGamePause()
    {
        // if settings menu object is enabled
        if (settingsMenu.activeSelf)
        {
            SwitchSettingsEnabled();
            return;
        }
        if (isMenuLevel)
        {
            return;
        }
        // if menu object is enabled
        if (!menu.activeSelf)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            menu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SetMinimapEnabled(bool state)
    {
        minimap.SetActive(state);
        settings.minimapEnabled = state;
    }

    public void SetInvertJoysticks(bool state)
    {
        settings.SetJoysticksInverted(state);
        movement.SetJoysticksInverted(state);
    }

    // this method turns on/off map window
    public void SwitchMapEnabled()
    {
        if (isMenuLevel)
        {
            return;
        }
        if (!mapCamera.activeSelf)
        {
            if (!menu.activeSelf&& !drawingTools.activeSelf)
            {
                drawingToolsButton.SetActive(false);
                minimap.SetActive(false);
                mapCamera.SetActive(true);
            }
        } else
        {
            drawingToolsButton.SetActive(true);
            if (settings.minimapEnabled)
            {
                minimap.SetActive(true);
            }
            mapCamera.SetActive(false);
        }
    }

    // this method will set movement sensitivity(movement speed)
    public void SetMovementSens(float movementSens)
    {
        movement.speed = movementSens;
    }

    // this method will set rotaion sensitivity(rotation speed)
    public void SetRotationSens(float rotationSens)
    {
        movement.rotationSpeed = rotationSens;
    }

    // this method will enable/disable UI elements
    public void SetUIEnabled(bool state)
    {
#if UNITY_IOS || UNITY_ANDROID
        joysticks.SetActive(state);
        mapButton.SetActive(state);
#endif
        drawingToolsButton.SetActive(state);
        colorImage.gameObject.SetActive(state);
        if (state) {
            minimap.SetActive(minimapEnabledCheckbox.isOn);
        } else
        {
            minimap.SetActive(state);
        }

    }

}
