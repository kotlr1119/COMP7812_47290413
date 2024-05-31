using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class commentScript : NetworkBehaviour
{
    public GameObject commentUI;
    public GameObject privateButton;
    string objectName;

    public TMP_InputField commentInput;
    Dictionary<string, string> comments = new Dictionary<string, string>();
    Dictionary<string, string> commentsP = new Dictionary<string, string>();

    public GameObject taskUI;
    public GameObject nextButton;
    public GameObject endButton;
    

    void Start()
    {
        commentUI.SetActive(false);
        nextButton.SetActive(true);
        endButton.SetActive(false);
        taskUI.SetActive(false);
    }
    // Start is called before the first frame update
    public void CallMessageRPC(bool priv)
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        string name = gm.GetComponent<gameManagerScript>().getID();
        string targetObject = objectName;
        string message = commentInput.text;
        RPC_SendMessage(priv, targetObject, name, message, "send");
        commentInput.text = "";
        commentUI.SetActive(false);
        privateButton.SetActive(false);

    }

    public void askComment(string obj)
    {
        RPC_SendMessage(true, obj, "", "", "ask");
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SendMessage(bool priv, string targetObject, string name, string message, string type, RpcInfo rpcInfo = default)
    {
        
        string newComment = $"{name}: {message}\n";

        if (type != "ask")
        {
            if (priv == true)
            {
                if (commentsP.ContainsKey(targetObject))
                {
                    commentsP[targetObject] += newComment;
                }
                else
                {
                    commentsP.Add(targetObject, newComment);
                }
            }
            else
            {
                if (commentsP.ContainsKey(targetObject))
                {
                    commentsP[targetObject] += newComment;
                }
                else
                {
                    commentsP.Add(targetObject, newComment);
                }

                if (comments.ContainsKey(targetObject))
                {
                    comments[targetObject] += newComment;
                }
                else
                {
                    comments.Add(targetObject, newComment);
                }

            }
        }

        if (type == "ask")
        {
            if (comments.ContainsKey(targetObject))
            {
                RPC_returnMessage(targetObject, comments[targetObject], commentsP[targetObject]);
            }
        }

        
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_returnMessage(string targetObject, string c, string cp, RpcInfo rpcInfo = default)
    {
        
        comments[targetObject] = c;
        commentsP[targetObject] = cp;
        
    }


    public string loadComment(string n)
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        int priv = gm.GetComponent<gameManagerScript>().checkPrivilege();

        if (commentsP.ContainsKey(n))
        {
            if (priv > 1)
            {
                return commentsP[n];
            }
            else
            {
                return comments[n];
            }
        }
        else
        {
            return "";
        }
        
    }

    public void openComment(string n)
    {
        commentUI.SetActive(true);
        objectName = n;

        if (n == "myProjectManager")
        {
            privateButton.SetActive(true);
        }
        else
        {
            privateButton.SetActive(false);
        }
    }

    public void closeComment()
    {
        commentUI.SetActive(false);
    }

    public void switchTaskUI(bool b)
    {
        if (b)
        {
            taskUI.SetActive(true);
        }
        else
        {
            taskUI.SetActive(false);
        }
    }
    public void callMoveStep(bool b)
    {
        GameObject target_led = transform.Find("myProjectManager(Clone)").gameObject;
        target_led.GetComponent<myProjectManager>().moveStep(b);
    }

    public void showEndButton(bool b)
    {
        if (b)
        {
            nextButton.SetActive(false);
            endButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            endButton.SetActive(false);
        }
    }

    public void callEndTask()
    {
        GameObject target_led = transform.Find("myProjectManager(Clone)").gameObject;
        target_led.GetComponent<myProjectManager>().endTask();
    }
}
