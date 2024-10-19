using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected SistemaDialogos sistemaDialogos;
    public GameObject player;
    public GameObject gameController; 

    private void Start() {
        sistemaDialogos = gameController.GetComponent<SistemaDialogos>();
    }

    public abstract void Interaction();
}
