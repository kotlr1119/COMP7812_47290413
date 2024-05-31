using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public GameObject target;
    GameObject instantiated = null;
    // Start is called before the first frame update
    void Start()
    {
        instantiated = Instantiate(target, transform.position, Quaternion.identity);
    }

    void Update()
    {
        instantiated.transform.position = transform.position + Vector3.up * 0.2f;
    }

    public void passName(string n)
    {
        instantiated.GetComponent<infoBoardScript>().assignName("t1");
    }

}
