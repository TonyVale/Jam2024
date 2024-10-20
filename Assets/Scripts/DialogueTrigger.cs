using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class DialogueTrigger : MonoBehaviour
{
    public static int max_Iteraciones = 2;

    [Header("Visual Cue")]
    //[SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;
    public GameObject indicador;
    public bool canInteract;
    private bool playerInRange;

    private void Awake() 
    {
        playerInRange = false;
        indicador.SetActive(false);
    }

    private void Update() 
    {

        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && canInteract) 
        {
            indicador.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Z)) 
            {
                canInteract = false;
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                
            }
        }
        else 
        {
            indicador.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {

        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}