using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CeldaCinematica : MonoBehaviour
{
    SistemaDialogos sistemaDialogos;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<IsTalking>().Talk();
        sistemaDialogos = gameObject.GetComponent<SistemaDialogos>();
        sistemaDialogos.Interact(new List<string>{"[Z] Guardia: !!!!!BELL LUGASTEIN¡¡¡¡¡","[Z] Guardia: Disfruta tu ultimo día", "[Z] Guardia: Feliz día de la Beluga", "[Z] Guardia: Anda a desayunar"});
    }
    
}
