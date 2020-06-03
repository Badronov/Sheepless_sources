using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class TimelineController : MonoBehaviour
{

    public PlayableDirector playableDirector;
    public UIController uIController;
    private bool isPlaying;
    private bool isPlayed;
    private RuntimeAnimatorController animator;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (!playableDirector)
            playableDirector = GetComponent<PlayableDirector>();
        isPlaying = false;
        isPlayed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // starting timeline if it had not started before
        if (!isPlayed)
        {
            // getting player gameobject from collider
            player = other.gameObject;
            player.GetComponent<Rigidbody>().isKinematic = true;
            // saving actual player animator
            animator = other.GetComponent<RuntimeAnimatorController>();
            uIController.SetUIEnabled(false);
            // starting timeline
            playableDirector.Play();
            isPlaying = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if timeline is paused and it had started before
        if (isPlaying && playableDirector.state == PlayState.Paused)
        {
            uIController.SetUIEnabled(true);
            isPlaying = false;
            isPlayed = true;
            playableDirector.Stop();
            // setting to the player animator it had before (doing this because of timelines bug that stucks player after playing timeline)
            player.GetComponent<Animator>().runtimeAnimatorController = animator;
            player.transform.position.Set(transform.position.x, player.transform.position.y, transform.position.z);
            player.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
