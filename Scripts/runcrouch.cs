using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runcrouch : MonoBehaviour
{

    private playermove _playermove;
	[HideInInspector]
    public float runspeed = 7f;
	[HideInInspector]
	public float crouchspeed = 1f;
	[HideInInspector]
	public float speed = 4f;

    private Transform Playerbody;
	private float height = 0f;
    private float crouchheight = -.75f;

    private bool is_crouching;
	private Audio _audio;
	[HideInInspector]
	public float runspeed_vol;
	[HideInInspector]
	public float crouchspeed_vol ;
	[HideInInspector]
	public float speed_vol_min , speed_vol_max ;
	[HideInInspector]
	public float nor_step_dist;
	[HideInInspector]
	public float runspeed_step_dist ;
	[HideInInspector]
	public float crouchspeed_step_dist ;


    // Start is called before the first frame update
    void Awake()
    {
        _playermove = GetComponent<playermove>();
        Playerbody = transform.GetChild(0); //check
		_audio  = GetComponentInChildren<Audio>();
    }

	void Start()
	{
		_audio.vol_min = speed_vol_min;
		_audio.vol_max = speed_vol_max;
		_audio.step_dist = nor_step_dist;
	}

    // Update is called once per frame
    void Update()
    {
		
		crouch();
		run();
    }

	void crouch()
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			if(is_crouching)
			{
				Playerbody.localPosition = new Vector3(0f,height,0f);
				_playermove.speed = speed;
				_audio.vol_min = speed_vol_min;
				_audio.vol_max = speed_vol_max;
				_audio.step_dist = nor_step_dist;
				
				is_crouching = false;
			}
			else
			{
				Playerbody.localPosition = new Vector3(0f,crouchheight,0f);
				_playermove.speed = crouchspeed;
				_audio.vol_min = crouchspeed_vol;
				_audio.vol_max = crouchspeed_vol;
				_audio.step_dist = crouchspeed_step_dist;
				
				is_crouching = true;
			}

		}	
	}

	void run()
	{
		if(Input.GetKeyDown(KeyCode.LeftShift) && !is_crouching)
		{
			_playermove.speed = runspeed;
			_audio.vol_min = runspeed_vol;
			_audio.vol_max = runspeed_vol;
			_audio.step_dist = runspeed_step_dist;
		}
		
		if (Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching)
		{
			_playermove.speed = speed;
			_audio.vol_min = speed_vol_min;
			_audio.vol_max = speed_vol_max;
			_audio.step_dist = nor_step_dist;
		}
	}


}
