using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimal : MonoBehaviour
{
    // Rotatoins Geschwindigkeit
    private float rotationSpeed = 7f;
    
    void Update()
    {
        // Rotiert das Tier konstant auf der Y-Achse
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
