using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Audio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] screamclip, dieclip,attackclips;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ScreamSound()
    {
        audioSource.clip = screamclip[Random.Range(0, screamclip.Length)];
        audioSource.Play();
    }

    public void AttackSounds()
    {
        audioSource.clip = attackclips[Random.Range(0 , attackclips.Length)];
        audioSource.Play();
    }

    public void DeathSound()
    {
        audioSource.clip = dieclip[Random.Range(0, dieclip.Length)];
        audioSource.Play();
    }
}
