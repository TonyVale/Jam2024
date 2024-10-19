using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{

    [Header("DialogueUI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;  

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    public GameObject player;

    private Story currentStory;

    public bool dialogueIsPlaying; 
    private static DialogueManager intance; 

    private bool first;
    // Start is called before the first frame update
    private void Awake() {
        intance = this; 
    }

    public static DialogueManager GetInstance(){
        return intance;
    }

    private void Start() {
        
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices ){
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    
    public void EnterDialogueMode(TextAsset inkJSON){
        
        player.GetComponent<MovimientoHorizontal>().enabled = false;
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        first = true; 
        ContinueStory();
    }

    private void ExitDialogueMode(){
        player.GetComponent<MovimientoHorizontal>().enabled = true;
        dialogueIsPlaying = false; 
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialogueIsPlaying){
            return;
        }
        else if(Input.GetKeyDown(KeyCode.Z) && first == false){
            ContinueStory();
        }
        first = false;
    }

    private void ContinueStory(){
         if(currentStory.canContinue){
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
         }else{
            ExitDialogueMode();
         }
    }

    private void DisplayChoices(){
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length){
            Debug.LogError("Not choices supported");
        }
        int index = 0; 
        foreach(Choice choice in currentChoices){
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index ; i < choices.Length; i++){
            choices[i].gameObject.SetActive(false);
        } 
        selectFirstChoice();
    }

    private void selectFirstChoice(){
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void makeChoice(int choiceIndex){
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
