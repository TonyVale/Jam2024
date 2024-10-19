using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public GameObject gameController; 
    SistemaDialogos sistemaDialogos;

    private void Start() {
        sistemaDialogos = gameController.GetComponent<SistemaDialogos>();
    }

    private void OnTriggerEnter2D(Collider2D collision){ 
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Interacctuable"))
        {
            sistemaDialogos.Interact(new List<string>{"[Z]: Interactuar "});
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Interactable script = collision.gameObject.GetComponent<Interactable>();
        if (collision.gameObject.CompareTag("No Interactuable"))
        {
            sistemaDialogos.FinalizarDialogo();
        }
        else if(Input.GetKeyDown(KeyCode.Z) && collision.gameObject.CompareTag("Interacctuable")){
            script.Interaction();
        }
 
    }

        // Se llama una vez cuando la colisión 2D termina
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interacctuable"))
        {
            sistemaDialogos.FinalizarDialogo();
        }
    }


}


