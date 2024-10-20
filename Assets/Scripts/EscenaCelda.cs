using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaCelda : MonoBehaviour
{
    public GameObject cama;
    public GameObject calendario;

    public GameObject poster;
    public GameObject inodoro;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJsonCalendario;
    // Start is called before the first frame update

/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
    void Start(){
        if(EditJson.GetDatos().N_de_Iteracion == 0){
            Debug.Log("Primera Iteracion");
            calendario.GetComponent<DialogueTrigger>().inkJSON = inkJsonCalendario;
            inodoro.GetComponent<DialogueTrigger>().canInteract = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
