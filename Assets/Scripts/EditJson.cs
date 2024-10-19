using System.Collections;
using System.Collections.Generic;
using System.IO;  // Para trabajar con archivos
using UnityEngine;

public abstract class EditJson : MonoBehaviour
{
    static string rutaArchivo = Application.streamingAssetsPath + "/datos.json";


    public static Datos GetDatos(){
        
        string jsonContenido = File.ReadAllText(rutaArchivo);
        Datos datos = JsonUtility.FromJson<Datos>(jsonContenido);
        
        return JsonUtility.FromJson<Datos>(jsonContenido);
    }

    public static  void SetDatos(Datos getdatos){
        string jsonContenido_toSet = JsonUtility.ToJson(getdatos, true);
        File.WriteAllText(rutaArchivo, jsonContenido_toSet);
    }

}
