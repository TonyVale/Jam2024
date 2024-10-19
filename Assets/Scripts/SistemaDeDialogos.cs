using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDialogos : MonoBehaviour
{

    public GameObject player;
    public GameObject DialogosCanvas;
    public Text textoDialogo;          // Referencia al UI Text

    [TextArea(3, 10)] 
    public List<string> lineas;           // Arreglo de líneas de diálogo
    private int indiceActual = 0;      // Línea actual del diálogo

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    private void Update() {
        if(player.GetComponent<IsTalking>().isTalking == true && Input.GetKeyDown(KeyCode.Z)){
            SiguienteLinea();
        }
    }

    public void Interact(List<string> passlineas)
    {   
        DialogosCanvas.SetActive(true);
        lineas = passlineas;
        MostrarLinea();  // Mostrar la primera línea del diálogo
    }

    public void SiguienteLinea()
    {
        indiceActual++;  // Avanzar al siguiente diálogo

        if (indiceActual < lineas.Count)
        {
            MostrarLinea();
        }
        else
        {
            FinalizarDialogo();
        }
    }

    void MostrarLinea()
    {
        textoDialogo.text = lineas[indiceActual];  // Mostrar la línea actual
    }

    public void FinalizarDialogo()
    {
        player.GetComponent<IsTalking>().StopTalk();
        textoDialogo.text = "";  // Limpiar el texto
        indiceActual = 0;
        lineas.Clear();
        DialogosCanvas.SetActive(false);
    }

    

}
