using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla la rotación de la cámara en primera persona basada en el movimiento del mouse.
/// Este script se debe adjuntar al objeto de la cámara, y se vincula al cuerpo del jugador (playerBody).
/// </summary>
public class MouseLook : MonoBehaviour
{
    [Header("Sensibilidad del mouse")]
    public float mouseSens = 100f; // Velocidad de rotación en función del movimiento del mouse

    [Header("Referencia al cuerpo del jugador")]
    public Transform playerBody; // Transform del objeto padre (usualmente el jugador) que rota horizontalmente

    float xRotation = 0f; // Acumula la rotación vertical para limitarla (evitar que la cámara gire completamente)

    /// <summary>
    /// Al iniciar el juego, el cursor se bloquea en el centro de la pantalla y se oculta.
    /// Esto es común en juegos en primera persona.
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// En cada frame, se calcula el movimiento del mouse y se aplica la rotación al jugador y la cámara.
    /// </summary>
    void Update()
    {
        // Se obtienen los movimientos del mouse en los ejes X e Y
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        // Se acumula la rotación vertical y se limita para evitar que la cámara rote completamente hacia atrás
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplica la rotación vertical (eje X) a la cámara (este objeto)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rota el cuerpo del jugador horizontalmente (eje Y) según el movimiento del mouse
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
