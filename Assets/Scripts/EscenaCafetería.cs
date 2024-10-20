using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscenaCafeteria: MonoBehaviour
{
    public GameObject[] initiallyOff;
    public GameObject disco_dan;

    [Header("Ink JSON")]
    [SerializeField] public TextAsset mensajeDeSalidaJSON;
    [SerializeField] public TextAsset DiscoDancer_Scape_Data;

    void Start(){
        DialogueManager.iteraciones = 2;
        if(EditJson.GetDatos().N_de_Iteracion == 0){
            DialogueManager.iteraciones--;
            foreach(GameObject turnOff in initiallyOff){
                turnOff.GetComponent<DialogueTrigger>().canInteract = false;
            }
        }
        if(EditJson.GetDatos().escape_thimothy != 0){
            disco_dan.GetComponent<DialogueTrigger>().inkJSON = DiscoDancer_Scape_Data;
        }
    }

    public void Update(){
        if(DialogueManager.iteraciones == 0 && !DialogueManager.GetInstance().dialogueIsPlaying){
            DialogueManager.GetInstance().EnterDialogueMode(mensajeDeSalidaJSON);
        }
    }

}
