using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
   
	private AudioSource footstep_sound;

	[SerializeField]
	private AudioClip[] footstep_clip;
	private CharacterController char_cont;

	
	public float vol_min ,vol_max;

	private float accumulated_dist;

	
	public float step_dist;

	void Awake()
	{
		footstep_sound = GetComponent<AudioSource>();
		char_cont = GetComponentInParent<CharacterController>();
	}
    // Update is called once per frame
    void Update()
    {
		footstepchecker();
    }

	void footstepchecker()
	{
		if(!char_cont.isGrounded)
			return;
		if(char_cont.velocity.sqrMagnitude > 0)
		{
			accumulated_dist += Time.deltaTime;
			if(accumulated_dist > step_dist)
			{
				footstep_sound.volume = Random.Range(vol_min,vol_max);
				footstep_sound.clip = footstep_clip[Random.Range(0,footstep_clip.Length)];
				footstep_sound.Play();

				accumulated_dist = 0f;
			}
		}
		else
		{
			accumulated_dist =0f;
		}
	}
}
