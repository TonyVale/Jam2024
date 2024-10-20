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

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    public GameObject player;

    public static int lastIndex;
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

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;

        player.GetComponent<MovimientoHorizontal>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        dialoguePanel.SetActive(true);
        first = true;         
        
        currentStory.BindExternalFunction("ChangeSceneComedor", () => SceneManager.LoadScene(2));
        currentStory.BindExternalFunction("ChangeSceneEnfermeria", ()=>SceneManager.LoadScene(3));
        currentStory.BindExternalFunction("ChangeScenePatio", ()=>SceneManager.LoadScene(4));
        currentStory.BindExternalFunction("ChangeSceneLavanderia", ()=>SceneManager.LoadScene(5));
        currentStory.BindExternalFunction("ChangeSceneCelda", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.N_de_Iteracion++;
            EditJson.SetDatos(datos);
            SceneManager.LoadScene(1);
        });

        currentStory.BindExternalFunction("patio_scarface", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.patio_scarface++;
            EditJson.SetDatos(datos);
            SceneManager.LoadScene(1);
        });
        
        currentStory.BindExternalFunction("fish_lavanderia", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.fish_lavanderia++;
            EditJson.SetDatos(datos);
            SceneManager.LoadScene(1);
        });
        
        currentStory.BindExternalFunction("drHousearrest_enfermeria", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.drHousearrest_enfermeria++;
            EditJson.SetDatos(datos);
            SceneManager.LoadScene(1);
        });
        
        currentStory.BindExternalFunction("nurseHappy_enfermeria", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.nurseHappy_enfermeria++;
            EditJson.SetDatos(datos);
            SceneManager.LoadScene(1);
        });
        
        currentStory.BindExternalFunction("uniforme", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.uniforme++;
            EditJson.SetDatos(datos);
            SceneManager.LoadScene(1);
        });
        
        currentStory.BindExternalFunction("llaves", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.llaves++;
            EditJson.SetDatos(datos);
            SceneManager.LoadScene(1);
        });
        
        currentStory.BindExternalFunction("riot", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.riot++;
            EditJson.SetDatos(datos);
            SceneManager.LoadScene(1);
        });
        ContinueStory();

    }

    private void ExitDialogueMode(){
        player.GetComponent<MovimientoHorizontal>().enabled = true;
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
        else if(Input.GetKeyDown(KeyCode.Z) && first == false){
            ContinueStory();
        }
        first = false;
    }

    private void ContinueStory(){
         if(currentStory.canContinue){
            dialogueText.text = currentStory.Continue();
            if( dialogueText.text == "" && !currentStory.canContinue){
                ExitDialogueMode();
            }
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
        if(choices!= null)
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void makeChoice(int choiceIndex){
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
