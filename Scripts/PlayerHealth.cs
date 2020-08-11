using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int phealth =100;
    private int damage=1;
    private int current_health;
    public healthUI health;
    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        current_health = phealth;
        health.SetMaxHealth(phealth);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {
            current_health -= damage;
            Debug.Log("health=" +current_health);
            if(current_health<=0)
            {
                SceneManager.LoadScene(0);
            }
            health.SetHealth(current_health);

        }
    }
}
