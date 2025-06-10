using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el volumen de un AudioSource en función de la distancia del jugador.
/// A menor distancia, mayor volumen. A mayor distancia, menor volumen.
/// </summary>
public class ProximityVolume : MonoBehaviour
{
    // Referencia al jugador o al objeto cuya proximidad se mide
    public Transform player;

    // Distancia máxima a la que el sonido puede escucharse
    public float maxDistance = 10f;

    // Volumen mínimo cuando el jugador está a la distancia máxima
    public float minVolume = 0f;

    // Volumen máximo cuando el jugador está muy cerca
    public float maxVolume = 1f;

    // Referencia interna al componente AudioSource del GameObject
    private AudioSource audioSource;

    void Start()
    {
        // Obtiene el componente AudioSource adjunto al mismo GameObject
        audioSource = GetComponent<AudioSource>();

        // Establece el volumen inicial en cero (silenciado)
        audioSource.volume = 0f;

        // Comienza a reproducir el sonido (aunque inicialmente no se escuche)
        audioSource.Play(); 
    }

    void Update()
    {
        // Calcula la distancia al jugador usando magnitud cuadrada (más eficiente que Mathf.Sqrt)
        float distance = (player.position - transform.position).sqrMagnitude;

        // Si el jugador está dentro del rango audible
        if (distance < maxDistance)
        {
            // Interpola el volumen entre el máximo y el mínimo en función de la distancia
            float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
            audioSource.volume = volume;
        }
        else
        {
            // Fuera del rango: silencia el audio
            audioSource.volume = 0f;
        }
    }
}
