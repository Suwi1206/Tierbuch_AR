using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;

    // Diese Methode wird einmal beim Initialisieren des Objekts aufgerufen
    void Start()
    {
        // Holt die AudioSource-Komponente am gleichen GameObject
        audioSource = GetComponent<AudioSource>();

        // Spielt den Sound ab der im Objekt zugewiesen wurde
        audioSource.Play();
    }
}


