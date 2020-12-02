using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public GameObject target;

    //public GameObject targetMap;

    // Use this for initialization
    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        col.transform.position = target.transform.GetChild(0).transform.position;
    }
}
