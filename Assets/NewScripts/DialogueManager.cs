using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{
    [Header("DialogueUI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;  

    private Story currentStory;

    public bool dialogueIsPlaying {get ; private set;}

    private static DialogueManager instance; 

    // Start is called before the first frame update
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("More Than one Dialogue Manager in the scene");
        }
        instance = this; 
    }

    public static DialogueManager GetInstance(){
        return instance;
    }

    private void Start() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }
    
    public void EnterDialogueMode(TextAsset inkJSON){


        currentStory = new Story(inkJSON.text);
        dialoguePanel.SetActive(true);
        dialogueIsPlaying = true;
        ContinueStory();
        
    }

    private IEnumerator ExitDialogueMode(){
        
        yield return new WaitForSeconds(0.2f);
        
        
        dialogueIsPlaying = false; 
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    // Update is called once per frame
    public void Update()
    {
        if(!dialogueIsPlaying){
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.Z)){
            ContinueStory();
        }
    }

    private void ContinueStory(){
        if(currentStory.canContinue){
            dialogueText.text = currentStory.Continue();
            Debug.Log(dialogueText.text);
        }
        else{
            StartCoroutine(ExitDialogueMode());
        }
    }


}
