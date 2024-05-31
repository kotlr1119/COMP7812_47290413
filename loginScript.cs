using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text title;
    public Button loginButton;
    Hashtable credentials = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        credentials.Add("t1","111");
        credentials.Add("s1","aaa");
        credentials.Add("s2","bbb");
        credentials.Add("s3","ccc");
        credentials.Add("s4729041","kkk");
    }



    // Update is called once per frame
    public void login()
    {
        bool isExists = false;


        if (credentials.Contains(usernameInput.text))
        {
            if (credentials[usernameInput.text].Equals(passwordInput.text))
            {
                isExists = true;
            }
        }
        else
        {
            isExists = false;
        }

        if (isExists)
        {
            title.text = "Success";

            GameObject gm = GameObject.FindWithTag("GameController");
            gm.GetComponent<gameManagerScript>().storeID(usernameInput.text);

            SceneManager.LoadScene("AR");
        }
        else
        {
            title.text = "Fail";
        }
    }

}