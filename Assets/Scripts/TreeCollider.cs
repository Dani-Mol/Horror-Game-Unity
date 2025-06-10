using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fuerza la reactivación del componente TerrainCollider para solucionar posibles problemas
/// de detección de colisiones con árboles u otros elementos del terreno.
/// </summary>
public class TreeCollider : MonoBehaviour
{
    void Awake()
    {
        // Desactiva el TerrainCollider
        GetComponent<TerrainCollider>().enabled = false;

        // Lo vuelve a activar inmediatamente
        GetComponent<TerrainCollider>().enabled = true;
    }
}
