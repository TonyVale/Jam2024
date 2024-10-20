using System.Collections;
using System.Collections.Generic;
using System.IO;  // Para trabajar con archivos
using UnityEngine;

public abstract class EditJson : MonoBehaviour
{
    static string rutaArchivo = Application.streamingAssetsPath + "/save.json";



    public static void deleteSave(){
        string jsonOriginal = File.ReadAllText(Application.streamingAssetsPath + "/data.json");
        Datos datos = JsonUtility.FromJson<Datos>(jsonOriginal);
        SetDatos(datos);
    }


    public static Datos GetDatos(){
        
        string jsonContenido = File.ReadAllText(rutaArchivo);
        return JsonUtility.FromJson<Datos>(jsonContenido);
        
    }

    public static  void SetDatos(Datos getdatos){
        string jsonContenido_toSet = JsonUtility.ToJson(getdatos, true);
        File.WriteAllText(rutaArchivo, jsonContenido_toSet);
    }

}
