using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;
using TMPro;
using UnityEngine.UI;

public class infoBoardScript : MonoBehaviour
{
    bool infoActive = true;
    public TMP_Text commentContent;
    public TMP_Text infoContent;
    string objectName = "powerSupply";
    public GameObject myPrjButton;
    bool minimized = false;
    public GameObject minButton;

    // Start is called before the first frame update
    void Start()
    {
        Transform notePage = transform.Find("pages").transform.Find("notePage");
        CanvasGroup canvasGroup = notePage.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.15f;

        updateComment();

        myPrjButton.SetActive(false);
        if (objectName == "myProjectManager")
        {
            myPrjButton.SetActive(true);
        }

        StartCoroutine("updateOneSec");
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    IEnumerator updateOneSec()
    {
        if (infoActive == false)
        {
            updateComment();
        }
        else
        {
            updateInfo();
        }

        yield return new WaitForSeconds(1);
        StartCoroutine("updateOneSec");
    }

    public void switchPanel()
    {


        if (infoActive == true)
        {
            infoActive = false;
            Transform infoPage = transform.Find("pages").transform.Find("infoPage");
            CanvasGroup canvasGroup = infoPage.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0.18f;

            Transform notePage = transform.Find("pages").transform.Find("notePage");
            canvasGroup = notePage.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;

            Transform pages = transform.Find("pages");
            pages.Translate(Vector3.left * 0.2f);

        }
        else
        {
            infoActive = true;

            Transform infoPage = transform.Find("pages").transform.Find("infoPage");
            CanvasGroup canvasGroup = infoPage.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;

            Transform notePage = transform.Find("pages").transform.Find("notePage");
            canvasGroup = notePage.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0.18f;

            Transform pages = transform.Find("pages");
            pages.Translate(Vector3.right * 0.2f);
        }
    }

    public void openComment()
    {
        GameObject cs = GameObject.FindWithTag("commentSys");
        cs.GetComponent<commentScript>().openComment(objectName);
    }

    public void updateComment()
    {
        GameObject cs = GameObject.FindWithTag("commentSys");
        cs.GetComponent<commentScript>().askComment(objectName);
        string newComment = cs.GetComponent<commentScript>().loadComment(objectName);
        commentContent.text = newComment;
    }

    public void updateInfo()
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        string newInfo = gm.GetComponent<gameManagerScript>().loadInfo(objectName);
        infoContent.text = newInfo;
    }

    public void assignName(string n)
    {
        objectName = n;
        updateInfo();
    }

    public void callMyProject()
    {
        GameObject mpm = this.transform.parent.gameObject;
        mpm.GetComponent<myProjectManager>().openMyProject();

        // GameObject mpm = GameObject.FindWithTag("myProjectManager");
        // mpm.GetComponent<myProjectManager>().openMyProject();
    }

    public void minimize()
    {
        if (!minimized)
        {
            minimized = true;
            GameObject pages = transform.Find("pages").gameObject;
            pages.SetActive(false);

        }
        else
        {
            minimized = false;
            GameObject pages = transform.Find("pages").gameObject;
            pages.SetActive(true);
        }
    }

}
