using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poster_Almacen : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start(){
        
    }

    public void storeItem(){
        if(gameObject.GetComponent<Poster_Almacen>().enabled == true)
        Debug.Log("ItemGuardado");
    }


}
