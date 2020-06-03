using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsController : MonoBehaviour
{
    public UIController uIController;
    public VideoPlayerController videoPlayerController;

    // Update is called once per frame
    void Update()
    {
        if (videoPlayerController && videoPlayerController.isPlaying)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            uIController.SwitchMapEnabled();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uIController.SwitchGamePause();
        }
    }

}
