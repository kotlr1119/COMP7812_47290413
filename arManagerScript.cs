using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;


public class arManagerScript : MonoBehaviour
{


    private ARTrackedImageManager trackedImages;
    public GameObject ArPrefabs;
    public GameObject myProjectManagerPrefab;

    private ARTrackedObjectManager trackedObjects;


    Dictionary<string, GameObject> ARObjects = new Dictionary<string, GameObject>();

    
    void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
        trackedObjects = GetComponent<ARTrackedObjectManager>();
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
         trackedObjects.trackedObjectsChanged += ObjectChanged;
    }

    void OnDisable()
    {
         trackedObjects.trackedObjectsChanged -= ObjectChanged;
    }


    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //Create object based on image tracked
        foreach (var trackedImage in eventArgs.added)
        {
            // foreach (var arPrefab in ArPrefabs)
            // {
            //     if(trackedImage.referenceImage.name == arPrefab.name)
            //     {
            //         var newPrefab = Instantiate(arPrefab, trackedImage.transform);
            //         ARObjects.Add(newPrefab);
            //     }
            // }

            GameObject targetPrefab = ArPrefabs;
            string newName = trackedImage.referenceImage.name;
            // if (newName == "myProjectManager")
            // {
            //     targetPrefab = myProjectManagerPrefab;
            // }
            GameObject newPrefab = Instantiate(targetPrefab, trackedImage.transform.position, Quaternion.identity);
            ARObjects.Add(newName, newPrefab);

            // if (newPrefab.TryGetComponent(out myProjectManager mpm))
            // {
            //     mpm.passName(newName);
            // }

            newPrefab.GetComponent<myProjectManager>().setName(newName);
            
        }
        
        //Update tracking position
        foreach (var trackedImage in eventArgs.updated)
        {
            
            foreach (var key in ARObjects.Keys)
            {
                if(trackedImage.referenceImage.name.Equals(key))
                {
                    ARObjects[key].transform.position = trackedImage.transform.position;
                    ARObjects[key].transform.rotation = trackedImage.transform.rotation;
                    
                }
            }
        }
        
    }

    private void ObjectChanged(ARTrackedObjectsChangedEventArgs eventArgs)
    {
        foreach(ARTrackedObject trackedObject in eventArgs.added)
        {
            // newPrefab = Instantiate(_placeablePrefabs[0], trackedObject.transform.position, trackedObject.transform.rotation);
            GameObject targetPrefab = ArPrefabs;
            string newName = trackedObject.referenceObject.name;

            GameObject newPrefab = Instantiate(targetPrefab, trackedObject.transform.position, Quaternion.identity);
            ARObjects.Add(newName, newPrefab);

            // if (newPrefab.TryGetComponent(out myProjectManager mpm))
            // {
            //     mpm.passName(newName);
            // }

            newPrefab.GetComponent<myProjectManager>().setName(newName);
        }

        foreach (ARTrackedObject trackedObject in eventArgs.updated)
        {
            foreach (var key in ARObjects.Keys)
            {
                if(trackedObject.referenceObject.name.Equals(key))
                {
                    ARObjects[key].transform.position = trackedObject.transform.position;
                    
                }
            }
        }


    }
}
