using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTalking : MonoBehaviour
{
    public bool isTalking;

    public void Talk(){
        isTalking = true;
        GetComponent<MovimientoHorizontal>().enabled = false;
        GetComponent<Interact>().enabled = false;
        GetComponent<Animator> ().SetBool ("Walking", false);
    }

    
    public void StopTalk(){
        GetComponent<MovimientoHorizontal>().enabled = true;
        GetComponent<Interact>().enabled = true;
        isTalking = false;
    }


}
