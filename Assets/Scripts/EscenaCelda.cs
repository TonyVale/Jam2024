using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaCelda : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextScene(){
        Debug.Log("NextScene");
        if(gameObject.GetComponent<EscenaCelda>().enabled==true)SceneManager.LoadScene(3);
    }
}
