using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaCelda : MonoBehaviour
{
    public GameObject cama;
    public GameObject poster;
    public GameObject inodoro;
    // Start is called before the first frame update

/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
    void Awake(){
        if(EditJson.GetDatos().N_de_Iteracion == 0){
            inodoro.GetComponent<DialogueTrigger>().canInteract = false;
            poster.GetComponent<DialogueTrigger>().canInteract = false;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
