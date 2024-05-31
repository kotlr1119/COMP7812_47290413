using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;
using TMPro;
using UnityEngine.UI;

public class myProjectManager : MonoBehaviour
{
    public GameObject prjUI;
    GameObject instantiated = null;

    Dictionary<string, GameObject> task1Objects = new Dictionary<string, GameObject>();
    public GameObject task1Prefab;
    int currentStep = -1;
    TMP_Text dialogContent;

    public GameObject infoPrefab;
    string nameForPass = "powerSupply";

    // Start is called before the first frame update
    void Start()
    {
        instantiated = Instantiate(infoPrefab, transform.position+ Vector3.up * 0.1f, Quaternion.identity);
        instantiated.transform.parent = this.transform;
        // if (instantiated.TryGetComponent(out infoBoardScript infoScript))
        // {
        //     infoScript.assignName("myProjectManager");
        // }

         if (instantiated.TryGetComponent(out infoBoardScript infoScript))
        {
            infoScript.assignName(nameForPass);
        }

        if (nameForPass == "myProjectManager")
        {
            // GameObject gm = GameObject.FindWithTag("GameController");
            GameObject cs = GameObject.FindWithTag("commentSys");
            transform.parent = cs.transform;
        }
    

        StartCoroutine("updateTransform");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setName(string n)
    {
       nameForPass = n;
    }

    public void startTask(int currentTask)
    {
        Destroy(instantiated);
        GameObject gm = GameObject.FindWithTag("GameController");
        GameObject cs = GameObject.FindWithTag("commentSys");
        cs.GetComponent<commentScript>().switchTaskUI(true);
        task1(-1);
    }

    public void endTask()
    {
        Destroy(instantiated);
        currentStep = -1;
        GameObject gm = GameObject.FindWithTag("GameController");
        GameObject cs = GameObject.FindWithTag("commentSys");
        cs.GetComponent<commentScript>().switchTaskUI(false);

        task1Objects = new Dictionary<string, GameObject>();

        cs.GetComponent<commentScript>().showEndButton(false);

        instantiated = Instantiate(prjUI, transform.position, Quaternion.identity);
        instantiated.transform.parent = this.transform;

    }

    IEnumerator updateTransform()
    {
        if (currentStep >= 0)
        {
            instantiated.transform.position = transform.position + Vector3.up * 0.01f;
            Quaternion currentRotation = transform.rotation;
            instantiated.transform.rotation = Quaternion.Euler(0f, currentRotation.eulerAngles.y, 0f);
            yield return new WaitForSeconds(1);
            StartCoroutine("updateTransform");
        }
        else
        {
            instantiated.transform.position = transform.position + Vector3.up * 0.1f;
            yield return new WaitForSeconds(1);
            StartCoroutine("updateTransform");
        }
    }

    void task1Reset()
    {
        foreach (GameObject obj in task1Objects.Values)
        {
            obj.SetActive(false);
        }
    }

    void configObjects()
    {
        GameObject display_led = instantiated.transform.Find("display_led").gameObject;
        task1Objects.Add("DiodeLed1", display_led.transform.Find("DiodeLed1").gameObject);
        task1Objects.Add("DiodeLed2", display_led.transform.Find("DiodeLed2").gameObject);
        task1Objects.Add("redwire1", display_led.transform.Find("redwire1").gameObject);
        task1Objects.Add("redwire2", display_led.transform.Find("redwire2").gameObject);
        task1Objects.Add("redwire3", display_led.transform.Find("redwire3").gameObject);
        task1Objects.Add("redwire4", display_led.transform.Find("redwire4").gameObject);
        task1Objects.Add("button", display_led.transform.Find("button").gameObject);
        task1Objects.Add("blackWire1", display_led.transform.Find("blackWire1").gameObject);
        task1Objects.Add("blackWire2", display_led.transform.Find("blackWire2").gameObject);
        task1Objects.Add("breadboard", display_led.transform.Find("breadboard").gameObject);
        task1Objects.Add("resistor", display_led.transform.Find("resistor").gameObject);
        task1Objects.Add("Battery", display_led.transform.Find("Battery").gameObject);

        dialogContent = instantiated.transform.Find("wizard").Find("dialog").Find("Canvas").Find("content").gameObject.GetComponent<TMP_Text>();
        dialogContent.text = ("Greetings, adventurer! I am Kola the Wizard, "+
        "here to guide you through the enchanting world of electronic circuits.");
    }

    public void moveStep(bool next)
    {
        if (next)
        {
            if (currentStep < 8)
            {
                currentStep += 1;
                task1(currentStep);
            }
        }
        else
        {
            if (currentStep > 0)
            {
                currentStep -= 1;
                task1(currentStep);
            }
        }
    }

    void task1(int step)
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        GameObject cs = GameObject.FindWithTag("commentSys");
        switch(step) 
        {
        case -1:
            instantiated = Instantiate(task1Prefab, transform.position, Quaternion.identity);
            instantiated.transform.position = transform.position + Vector3.up * 0.01f;
            Quaternion currentRotation = transform.rotation;
            instantiated.transform.rotation = Quaternion.Euler(0f, currentRotation.eulerAngles.y, 0f);

            configObjects();
            task1Reset();
            currentStep = 0;
            task1(currentStep);
            break;
        case 0:
            task1Reset();
            task1Objects["breadboard"].SetActive(true);
            break;
        case 1:
            task1Reset();
            task1Objects["breadboard"].SetActive(true);

            dialogContent.text = "Today, I shall illuminate your path as you embark on a quest to master "+
            "the Push Button LED Circuit With Breadboard. Are you ready to delve into the arcane arts of "+
            "electronics? Let us begin!";
            break;
        
        case 2:
            task1Reset();
            task1Objects["breadboard"].SetActive(true);

            dialogContent.text = "Gather Your Components: \n1 LED\n1 Push Button\n1 Resistor (typically around 220 ohms)\nJumper Wires\nBreadboard";
            break;
        case 3:
            task1Reset();
            task1Objects["breadboard"].SetActive(true);
            task1Objects["DiodeLed2"].SetActive(true);

            dialogContent.text = "Begin by placing your LED on the breadboard. The longer leg of the LED is the positive (+) side, called the anode, and the shorter leg is the negative (-) side, called the cathode.";
            break;
        case 4:
            task1Reset();
            task1Objects["breadboard"].SetActive(true);
            task1Objects["DiodeLed2"].SetActive(true);
            task1Objects["button"].SetActive(true);

            dialogContent.text = "Insert your push button switch onto the breadboard. Ensure that its legs are not connected to each other within the breadboard.";
            break;
        case 5:
            task1Reset();
            task1Objects["breadboard"].SetActive(true);
            task1Objects["DiodeLed2"].SetActive(true);
            task1Objects["button"].SetActive(true);
            task1Objects["resistor"].SetActive(true);

            dialogContent.text = "Place a resistor on the breadboard. One leg of the resistor should be connected to the anode (longer leg) of the LED, and the other leg should be inline with button switch's leg.";
            break;
        case 6:
            task1Reset();
            task1Objects["breadboard"].SetActive(true);
            task1Objects["DiodeLed2"].SetActive(true);
            task1Objects["button"].SetActive(true);
            task1Objects["resistor"].SetActive(true);
            task1Objects["redwire1"].SetActive(true);
            task1Objects["redwire2"].SetActive(true);
            task1Objects["redwire3"].SetActive(true);
            task1Objects["blackWire1"].SetActive(true);

            dialogContent.text = "Connect LED, resistor and push button switch with jumper wire. Then Connect two ends of jumper wire to positive and negative rails.";

            break;
        case 7:
            task1Reset();
            task1Objects["breadboard"].SetActive(true);
            task1Objects["DiodeLed2"].SetActive(true);
            task1Objects["button"].SetActive(true);
            task1Objects["resistor"].SetActive(true);
            task1Objects["redwire1"].SetActive(true);
            task1Objects["redwire2"].SetActive(true);
            task1Objects["redwire3"].SetActive(true);
            task1Objects["blackWire1"].SetActive(true);
            task1Objects["blackWire2"].SetActive(true);
            task1Objects["redwire4"].SetActive(true);
            task1Objects["Battery"].SetActive(true);

            dialogContent.text = "To complete the circuit, connect jumper wire from the positive (+) terminal of the power supply to the positive rail (+) on the breadboard. Then do the same process on negative side.";

            cs.GetComponent<commentScript>().showEndButton(false);
            break;
        case 8:
            task1Reset();
            task1Objects["breadboard"].SetActive(true);
            task1Objects["DiodeLed1"].SetActive(true);
            task1Objects["button"].SetActive(true);
            task1Objects["resistor"].SetActive(true);
            task1Objects["redwire1"].SetActive(true);
            task1Objects["redwire2"].SetActive(true);
            task1Objects["redwire3"].SetActive(true);
            task1Objects["blackWire1"].SetActive(true);
            task1Objects["blackWire2"].SetActive(true);
            task1Objects["redwire4"].SetActive(true);
            task1Objects["Battery"].SetActive(true);

            dialogContent.text = "Congratulations, brave adventurer! You have successfully completed the quest to create a Push Button LED Circuit With Breadboard. May your circuits be stable, and your electrons ever-flowing!";

            cs.GetComponent<commentScript>().showEndButton(true);
            break;
        default:
            // code block
            break;
        }
    }

    public void openMyProject()
    {
        Destroy(instantiated);
        instantiated = Instantiate(prjUI, transform.position + Vector3.up * 0.1f, Quaternion.identity);
        instantiated.transform.parent = this.transform;
    }

    public void closeMyProject()
    {
        Destroy(instantiated);
        instantiated = Instantiate(infoPrefab, transform.position + Vector3.up * 0.1f, Quaternion.identity);
        instantiated.transform.parent = this.transform;
        if (instantiated.TryGetComponent(out infoBoardScript infoScript))
        {
            infoScript.assignName("myProjectManager");
        }
    }

}
