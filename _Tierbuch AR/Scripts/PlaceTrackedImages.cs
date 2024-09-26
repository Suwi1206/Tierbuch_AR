using System;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


/*
Diese Klasse reagiert darauf ob ein Bild erkannt bzw. wieder entfernt wurde.
Sobald ein Bild erkannt wird, wird in der Liste ein Objekt gesucht welches den selben Namen trägt.
Danach wird dieses Objekt in die Spielewelt instanziiert.
*/

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{

    private ARTrackedImageManager _trackedImagesManager;

    // Liste von Objekten die instanziiert werden sollen - Der Name sollte dabei identisch mit den Bildern sein
    public GameObject[] ArPrefabs;
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        // Referenz zur ARTrackedImageManager-Komponente
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        // Eventhandler wenn Bilder erkannt werden
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        // Entfernt den Eventhandler wieder
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        // Loop through all new tracked images that have been detected
        foreach (var trackedImage in eventArgs.updated)
        {
            // Bekommt den Namen des getrackten Bildes
            var imageName = trackedImage.referenceImage.name;

            // Neuer Loop durch die Liste der Objekte
            foreach (var curPrefab in ArPrefabs)
            {
                // Prüft auf den Namen der Objekte und ob dieses Objekt bereits instanziiert wurde
                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0
                    && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    // Instanziiere das Objekt
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);

                    // Fügt das Objekt in eine Liste ein
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            _instantiatedPrefabs[trackedImage.referenceImage.name]
                .SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }

        // Sobald ein Bild nicht mehr zu erkennen ist werden die entsprechenden Objekte wieder gelöscht
        foreach (var trackedImage in eventArgs.removed)
        {
            Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
            _instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
        }
    }
}