using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;
using TMPro;
using UnityEngine.UI;

public class projectUIScript : MonoBehaviour
{
    public GameObject task1;
    GameObject instantiated = null;
    int currentTask = 1;
    // Start is called before the first frame update
    void Start()
    {
        instantiated = Instantiate(task1, transform.position, Quaternion.identity);
        instantiated.transform.position = transform.position + Vector3.forward * 0.04f + 
        Vector3.up * -0.02f + Vector3.right * 0.015f;
        instantiated.transform.rotation = transform.rotation;
        instantiated.transform.Rotate(Vector3.up, 180f);
        instantiated.transform.Rotate(Vector3.right, -45f);

        instantiated.transform.parent = transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        
    }

    public void startTask()
    {
        // GameObject mpm = GameObject.FindWithTag("myProjectManager");
        GameObject mpm = this.transform.parent.gameObject;
        mpm.GetComponent<myProjectManager>().startTask(currentTask);
    }

    public void callCloseMyProject()
    {
        // GameObject mpm = GameObject.FindWithTag("myProjectManager");
        GameObject mpm = this.transform.parent.gameObject;
        mpm.GetComponent<myProjectManager>().closeMyProject();
    }
}
