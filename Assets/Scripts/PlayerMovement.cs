using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del jugador en primera persona.
/// Incluye caminar, salto y detección de suelo.
/// </summary>
public class PlayerMovement : MonoBehaviour
{ 
    // Referencia al componente CharacterController del jugador
    public CharacterController controller;

    // Velocidad de movimiento del jugador
    public float speed = 12f;

    // Valor de la gravedad aplicada al jugador
    public float gravity = -9.81f;

    // Altura del salto del jugador
    public float jumpHeight = 3f;

    // Transform que indica la posición desde donde se revisa si el jugador está en el suelo
    public Transform groundCheck;

    // Radio de la esfera usada para detectar el suelo
    public float groundDistance = 0.4f;

    // Capas consideradas como suelo
    public LayerMask groundMask;

    // Vector que almacena la velocidad vertical (gravedad y salto)
    Vector3 velocity;

    // Indica si el jugador está tocando el suelo
    bool isGrounded;

    void Start()
    {
        // (Vacío por ahora, pero útil para inicializaciones si se requiere en el futuro)
    }

    // Se llama una vez por frame
    void Update()
    {
        // Comprobamos si el jugador está en el suelo mediante una esfera invisible
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Si el jugador está en el suelo y descendiendo, se mantiene una pequeña fuerza hacia abajo para "pegarlo" al suelo
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Captura el input horizontal y vertical (A/D y W/S o flechas)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calcula la dirección del movimiento basado en la orientación del jugador
        Vector3 move = transform.right * x + transform.forward * z;

        // Mueve al jugador horizontalmente
        controller.Move(move * speed * Time.deltaTime);

        // Si el jugador está en el suelo y presiona el botón de salto, se aplica una velocidad vertical para el salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }

        // Aplica la gravedad de forma acumulativa al eje Y
        velocity.y += gravity * Time.deltaTime;

        // Mueve al jugador verticalmente (gravedad o salto)
        controller.Move(velocity * Time.deltaTime);
    }
}
