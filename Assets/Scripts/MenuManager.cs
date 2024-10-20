using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Jugar()
    {
        EditJson.deleteSave();
        // Carga la escena del juego (Asegúrate de añadir la escena en Build Settings)
        SceneManager.LoadScene(1);
    }

    public void Opciones()
    {
        Debug.Log("Opciones seleccionadas"); // Aquí podrías abrir un submenú
    }

    public void Salir()
    {
        Application.Quit();
    }
}