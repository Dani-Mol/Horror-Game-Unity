using System.Collections;
using UnityEngine;

/// <summary>
/// Controla la reproducción de sonidos de pasos según la superficie bajo el jugador.
/// </summary>
public class FootStepsSound : MonoBehaviour
{
    // Audios para pasos en pasto
    public AudioSource grassFoot1, grassFoot2;

    // Audios para pasos en concreto
    public AudioSource concreteFoot1, concreteFoot2;

    // Audios para pasos en madera
    public AudioSource woodFoot1, woodFoot2;

    // Intervalo entre pasos en segundos
    public float stepRate = 0.5f;

    // Alternador para reproducir sonidos de pasos intercaladamente
    private bool stepToggle = false;

    // Superficie actual detectada bajo el jugador
    private string currentSurface = "Grass";

    // Referencia a la corrutina de pasos en curso
    private Coroutine footstepCoroutine;

    /// <summary>
    /// Se llama en cada frame. Detecta movimiento y administra la reproducción de sonidos.
    /// </summary>
    void Update()
    {
        // Detecta si el jugador se está moviendo (WASD o flechas)
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                        Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
                        Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow); 

        // Inicia la corrutina de pasos si el jugador comienza a moverse
        if (isMoving && footstepCoroutine == null)
        {
            footstepCoroutine = StartCoroutine(PlayFootsteps());
        }
        // Detiene la corrutina de pasos si el jugador se detiene
        else if (!isMoving && footstepCoroutine != null)
        {
            StopCoroutine(footstepCoroutine);
            footstepCoroutine = null;
        }

        // Detecta el tipo de superficie bajo el jugador
        DetectSurface();
    }

    /// <summary>
    /// Corrutina que reproduce sonidos de pasos alternando clips según la superficie.
    /// </summary>
    IEnumerator PlayFootsteps()
    {
        while (true)
        {
            // Selecciona y reproduce el sonido correspondiente a la superficie detectada
            if (currentSurface == "Grass")
            {
                if (stepToggle)
                    grassFoot1.Play();
                else
                    grassFoot2.Play();
            }
            else if (currentSurface == "Concrete")
            {
                if (stepToggle)
                    concreteFoot1.Play();
                else
                    concreteFoot2.Play();
            }
            else if (currentSurface == "Wood")
            {
                if (stepToggle)
                    woodFoot1.Play();
                else
                    woodFoot2.Play();
            }

            // Alterna entre los dos sonidos para mayor realismo
            stepToggle = !stepToggle;

            // Espera antes de reproducir el siguiente paso
            yield return new WaitForSeconds(stepRate);
        }
    }

    /// <summary>
    /// Lanza un rayo hacia abajo para detectar el tipo de superficie bajo el jugador.
    /// </summary>
    void DetectSurface()
    {
        RaycastHit hit;
        // Lanza un raycast hacia abajo para detectar colisiones con el suelo
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if (hit.collider.CompareTag("Grass"))
            {
                currentSurface = "Grass";
            }
            else if (hit.collider.CompareTag("Concrete"))
            {
                currentSurface = "Concrete";
            }
            else if (hit.collider.CompareTag("Wood"))
            {
                currentSurface = "Wood";
            }
        }
    }
}
