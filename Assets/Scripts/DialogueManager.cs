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

    public static int iteraciones;

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
    private static DialogueManager instance; 

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private bool first;
    // Start is called before the first frame update
    private void Awake() {
        instance = this; 
    }

    public static DialogueManager GetInstance(){
        return instance;
    }

    private void Start() {
        iteraciones = 2;
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
        
        currentStory.BindExternalFunction("ChangeSceneComedor", () => SceneManager.LoadScene(2));
        currentStory.BindExternalFunction("ChangeSceneEnfermeria", ()=>SceneManager.LoadScene(3));
        currentStory.BindExternalFunction("ChangeScenePatio", ()=>{Debug.Log("LLego Bien a ChangeScenePatio()");;SceneManager.LoadScene(4);});
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

        });
        
        currentStory.BindExternalFunction("fish_lavanderia", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.fish_lavanderia++;
            EditJson.SetDatos(datos);

        });
        
        currentStory.BindExternalFunction("drHousearrest_enfermeria", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.drHousearrest_enfermeria++;
            EditJson.SetDatos(datos);

        });
        
        currentStory.BindExternalFunction("nurseHappy_enfermeria", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.nurseHappy_enfermeria++;
            EditJson.SetDatos(datos);

        });
        
        currentStory.BindExternalFunction("uniforme", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.uniforme++;
            EditJson.SetDatos(datos);

        });
        
        currentStory.BindExternalFunction("llaves", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.llaves++;
            EditJson.SetDatos(datos);

        });
        
        currentStory.BindExternalFunction("riot", ()=>{
            Datos datos = EditJson.GetDatos();
            datos.riot++;
            EditJson.SetDatos(datos);

        });
        ContinueStory();

    }

    private IEnumerator ExitDialogueMode(){
        
        yield return new WaitForSeconds(0.2f);

        player.GetComponent<MovimientoHorizontal>().enabled = true;
        
        dialogueIsPlaying = false; 
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        iteraciones--;
    }

    // Update is called once per frame
    public void Update()
    {
        if(!dialogueIsPlaying){
            return;
        }
        if(Input.GetKeyDown(KeyCode.Z) && canContinueToNextLine && currentStory.currentChoices.Count == 0 ){
            Debug.Log("Presionaste Z de forma valida para pasar de dialogo");
            ContinueStory();
        }
        first = false;
    }

    private void ContinueStory(){
         if(currentStory.canContinue){

            if (displayLineCoroutine != null) 
            {
                StopCoroutine(displayLineCoroutine);
            }

            string nextLine = currentStory.Continue();
            
            if( nextLine.Equals("") && !currentStory.canContinue){
                StartCoroutine(ExitDialogueMode());
            }else{
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
         }else{
            StartCoroutine(ExitDialogueMode());
         }
    }

    private IEnumerator DisplayLine(string line){
        dialogueText.text = line;
        yield return new WaitForEndOfFrame();
        DisplayChoices();
        canContinueToNextLine = true;
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

        StartCoroutine(SelectFirstChoice());
    
    }

    private IEnumerator SelectFirstChoice() 
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        Debug.Log("OK");
    }

    public void makeChoice(int choiceIndex){
        
        if (canContinueToNextLine) 
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
            ContinueStory();
        }
    }
}
