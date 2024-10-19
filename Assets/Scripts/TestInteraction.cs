using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : Interactable
{
    private void Start() {
        sistemaDialogos = gameController.GetComponent<SistemaDialogos>();
    }

    public override void Interaction(){

        player.GetComponent<IsTalking>().Talk();

        sistemaDialogos.FinalizarDialogo();
        
        sistemaDialogos.Interact();

        gameObject.tag = "No Interactuable";
    
    }

}
