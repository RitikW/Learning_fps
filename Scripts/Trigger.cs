using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject go1;
    public GameObject go2;

    private void OnTriggerEnter(Collider other)
    {
        go1.SetActive(true);
        go2.SetActive(false);
    }
}
