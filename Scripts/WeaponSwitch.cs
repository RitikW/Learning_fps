using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSwitch : MonoBehaviour
{
    public int selected;
   

    // Start is called before the first frame update
    void Start()
    {
        selectweapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousweapon = selected;

        if(Input.GetKeyDown(KeyCode.Alpha1))
                {
            selected = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
                {
            selected = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
                {
            selected = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selected = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            selected = 4;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6)
        {
            selected = 5;
        }
        if (previousweapon != selected)
        {
            selectweapon();
        }
    }

    public void selectweapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == selected)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

   
}
