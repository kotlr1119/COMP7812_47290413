using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class gameManagerScript : MonoBehaviour
{
    Hashtable privileges = new Hashtable();
    Dictionary<string, string> infoContents  = new Dictionary<string, string>();

    string userID = "t1";
    public static gameManagerScript manager;

    // public GameObject taskUI;
    // public GameObject nextButton;
    // public GameObject endButton;

    void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

        infoContents.Add("myProjectManager", "Breadboard:\n\nPrototyping tool for electronics;" +
        "plastic board with interconnected holes to easily build and test circuits without soldering.");

        infoContents.Add("powerSupply", "Power supplies:\n\nDevices providing electrical energy" +
        "to electronic circuits or devices, converting input power into specified voltages" +
        "or currents reliably.");

        infoContents.Add("oscilloscope", "Oscilloscope:\n\nInstrument displaying voltage signals" + 
        "graphically over time, crucial for analyzing and troubleshooting electronic circuits, " +
        "vital in electronics troubleshooting and experimentation.");

        infoContents.Add("generator", "Waveform generator:\n\nDevice producing electrical signals of "+
        "varying frequencies and shapes for testing, calibration, and characterization of electronic circuits.");

    }

    // Start is called before the first frame update
    void Start()
    {
        privileges.Add("t1", 2);
        privileges.Add("s1", 1);
        privileges.Add("s2", 1);
        privileges.Add("s3", 1);

        // nextButton.SetActive(true);
        // endButton.SetActive(false);
        // taskUI.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(userID);

    }

    public void storeID(string id)
    {
        userID = id;
    }

    public string getID()
    {
        return userID;
    }

    public int checkPrivilege()
    {
        int result = (int)privileges[userID];

        if (userID == "s1")
        {
            result = 2;
        }
        return result;
    }

    public string loadInfo(string n)
    {
        return infoContents[n];
    }

    // public void switchTaskUI(bool b)
    // {
    //     if (b)
    //     {
    //         taskUI.SetActive(true);
    //     }
    //     else
    //     {
    //         taskUI.SetActive(false);
    //     }
    // }

    // public void callMoveStep(bool b)
    // {
    //     GameObject target_led = transform.Find("myProjectManager(Clone)").gameObject;
    //     target_led.GetComponent<myProjectManager>().moveStep(b);
    // }

    // public void showEndButton(bool b)
    // {
    //     if (b)
    //     {
    //         nextButton.SetActive(false);
    //         endButton.SetActive(true);
    //     }
    //     else
    //     {
    //         nextButton.SetActive(true);
    //         endButton.SetActive(false);
    //     }
    // }

    // public void callEndTask()
    // {
    //     GameObject target_led = transform.Find("myProjectManager(Clone)").gameObject;
    //     target_led.GetComponent<myProjectManager>().endTask();
    // }

}
