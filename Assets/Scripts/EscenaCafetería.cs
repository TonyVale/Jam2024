using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscenaCafeteria: MonoBehaviour
{
    public GameObject[] initiallyOff;
    void Start(){
        if(EditJson.GetDatos().N_de_Iteracion == 0){
            foreach(GameObject turnOff in initiallyOff){
                turnOff.GetComponent<DialogueTrigger>().canInteract = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
