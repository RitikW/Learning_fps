using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health;
    private Enemy_Audio e_aud;
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        Destroy(gameObject);
        sound();
    }
    IEnumerator sound()
    {
        yield return new WaitForSeconds(.3f);
        e_aud.DeathSound();
    }
}
