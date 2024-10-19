using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimientoSuave : MonoBehaviour
{
    public Transform objetivo;  // El personaje a seguir
    public Vector3 offset;      // Desplazamiento de la cámara respecto al objetivo
    public float suavizado = 0.125f;  // Velocidad del suavizado

    private Vector3 velocidad = Vector3.zero;  // Velocidad actual del SmoothDamp

    void LateUpdate()
    {
        // Posición deseada de la cámara
        Vector3 posicionDeseada = objetivo.position + offset;

        // Movimiento suavizado usando SmoothDamp
        Vector3 posicionSuavizada = Vector3.SmoothDamp(transform.position, posicionDeseada, ref velocidad, suavizado);

        // Aplicar la nueva posición
        transform.position = posicionSuavizada;
    }
}
