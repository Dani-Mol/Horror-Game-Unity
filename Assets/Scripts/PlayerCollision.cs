using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Detecta colisiones del jugador con enemigos. Al colisionar, muestra una pantalla de derrota y pausa el juego.
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    // Referencia a la pantalla de derrota que se activa cuando el jugador colisiona con un enemigo.
    public GameObject loseScreen; 

    /// <summary>
    /// Método llamado automáticamente cuando otro collider entra en el trigger de este objeto.
    /// </summary>
    /// <param name="other">Collider del objeto que entró en el trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.gameObject.name);

        // Si el objeto que entra tiene la etiqueta "Enemy", se considera una derrota.
        if (other.CompareTag("Enemy"))
        {
            // Activar pantalla de derrota
            loseScreen.SetActive(true);

            // Pausar todo el audio
            AudioListener.pause = true;

            // Mostrar y liberar el cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Pausar el tiempo del juego
            Time.timeScale = 0f;
        }
    }
}
