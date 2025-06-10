using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la recogida de páginas por parte del jugador. Gestiona sonidos, interfaz, y la activación de una pantalla de victoria al recolectar todas.
/// </summary>
public class pickupLetter : MonoBehaviour
{
    public GameObject collectTextObj;         // Objeto de UI que muestra cuántas páginas se han recolectado
    public GameObject intText;                // Texto que indica al jugador que puede interactuar
    public AudioSource pickupSound;           // Sonido que se reproduce al recoger una página
    public AudioSource[] ambianceLayers;      // Capas de sonido ambiental que se activan con cada página recolectada

    public GameObject victoryScreen;          // Pantalla de victoria que se muestra al recolectar todas las páginas

    public bool interactable;                 // Indica si el jugador está en rango para recoger la página
    public static int pagesCollected;         // Contador global de páginas recolectadas
    public Text collectText;                  // Referencia al texto de UI que muestra el contador

    /// <summary>
    /// Se ejecuta mientras el jugador (la cámara principal) esté en el rango del trigger.
    /// Activa el texto de interacción.
    /// </summary>
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(true);
            interactable = true;
        }
    }

    /// <summary>
    /// Se ejecuta cuando el jugador sale del rango del trigger.
    /// Desactiva el texto de interacción.
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            interactable = false;
        }
    }

    /// <summary>
    /// Verifica si el jugador presiona la tecla de interacción y recoge la página si está en rango.
    /// Actualiza UI, reproduce sonidos, y activa eventos según el progreso.
    /// </summary>
    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            // Incrementar el contador de páginas
            pagesCollected++;

            // Actualizar UI de texto
            collectText.text = pagesCollected + "/8 pages";
            collectTextObj.SetActive(true);

            // Reproducir sonido de recogida
            pickupSound.Play();

            // Activar capa de ambiente si existe una correspondiente
            if (pagesCollected - 1 < ambianceLayers.Length)
            {
                ambianceLayers[pagesCollected - 1].Play();
            }

            // Desactivar texto de interacción y el objeto de la carta
            intText.SetActive(false);
            this.gameObject.SetActive(false);
            interactable = false;

            // Si el jugador recolectó las 8 páginas, mostrar pantalla de victoria
            if (pagesCollected == 8)
            {
                if (victoryScreen != null)
                {
                    victoryScreen.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    AudioListener.pause = true;
                    pickupLetter.pagesCollected = 0;
                    Time.timeScale = 0f; // Pausar el juego
                }
            }
        }
    }
}
