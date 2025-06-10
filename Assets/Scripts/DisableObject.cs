using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Desactiva un objeto especificado luego de un tiempo determinado cuando este se encuentra activo.
/// </summary>
public class DisableObject : MonoBehaviour
{
    [Header("Objeto a desactivar")]
    public GameObject Obj; // Objeto que se desactivará

    [Header("Tiempo antes de desactivar (segundos)")]
    public float activeTime; // Tiempo que permanecerá activo antes de desactivarse


    /// <summary>
    /// Se ejecuta una vez por frame.
    /// Si el objeto está activo, se inicia una corrutina que lo desactiva después de cierto tiempo.
    /// </summary>
    [System.Obsolete] // Marca el uso del flag 'active' como obsoleto, mejor usar 'activeSelf'
    void Update()
    {
        // Verifica si el objeto está activo
        if (Obj.active == true)
        {
            StartCoroutine(Disableobj()); // Inicia corrutina para desactivarlo
        }
    }

    /// <summary>
    /// Espera un tiempo determinado y luego desactiva el objeto.
    /// </summary>
    IEnumerator Disableobj()
    {
        yield return new WaitForSeconds(activeTime); // Espera 'activeTime' segundos
        Obj.SetActive(false); // Desactiva el objeto
    }
}
