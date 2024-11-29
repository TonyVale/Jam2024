using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DialogueTrigger : MonoBehaviour
{

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;
    


    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        
    }

    private void Update()
    {
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){
            visualCue.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Z)){
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);
            }
        }
        else{
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            playerInRange = false;
        }
    }

}
