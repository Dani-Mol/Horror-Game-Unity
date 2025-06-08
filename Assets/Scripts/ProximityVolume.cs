using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityVolume : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 10f;
    public float minVolume = 0f;
    public float maxVolume = 1f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        audioSource.Play(); 
    }

    void Update()
    {
        float distance = (player.position - transform.position).sqrMagnitude;

        if (distance < maxDistance)
        {
            float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }
}
