using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controla el encendido y apagado de una linterna con la tecla F.
/// </summary>
public class Flashlight : MonoBehaviour
{
    // Referencia al objeto de luz que simula la linterna
    [SerializeField] GameObject FlashlightLight;

    // Indica si la linterna está encendida o no
    private bool FlashlightActive = false;

    /// <summary>
    /// Se ejecuta al iniciar el juego.
    /// Inicialmente apaga la linterna.
    /// </summary>
    void Start()
    {
        FlashlightLight.gameObject.SetActive(false);   
    }

    /// <summary>
    /// Se ejecuta en cada frame.
    /// Activa o desactiva la linterna al presionar la tecla F.
    /// </summary>
    void Update()
    {
        // Si se presiona la tecla F
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Cambia el estado de la linterna
            if (!FlashlightActive)
            {
                FlashlightLight.gameObject.SetActive(true);
                FlashlightActive = true;
            }
            else
            {
                FlashlightLight.gameObject.SetActive(false);
                FlashlightActive = false;
            }
        }
    }
}
