using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public UIController uIController;
    public bool isPlaying { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // disabling UI on screen
        uIController.SetUIEnabled(false);
        if (!videoPlayer)
            videoPlayer = GetComponent<VideoPlayer>();

        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPaused)
        {
            videoPlayer.gameObject.SetActive(false);
            isPlaying = false;
            // enabling ui on screen
            uIController.SetUIEnabled(true);
            Destroy(gameObject);
        }   
    }
}
