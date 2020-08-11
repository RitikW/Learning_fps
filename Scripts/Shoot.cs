using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{
    public int damage;
    public float range;    
    public float firerate;
    public int maxammo;
    private int currentammo;
    public float reloadtime;
    private bool is_reloadin = false;
    private float nxtfire = 0f;
    public float i_force;
    public Camera fpscam;
    private Animator anim;
    [SerializeField]
    public ParticleSystem MuzzleFlash;
    [SerializeField]
    private AudioSource shootsound, reloadsound;
    public Text r_ammoUI;
    public Text m_ammoUI;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    

    void Start()
    {
        currentammo = maxammo;
    }
    void OnEnable()
    {
        is_reloadin = false;
        anim.SetBool("is_reloading", false);
    }  

    // Update is called once per frame
    void Update()
    {
        r_ammoUI.text = currentammo.ToString();
        m_ammoUI.text = maxammo.ToString();
        if (is_reloadin)
        {
            return;

        }
        if (currentammo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(reload());
            return;
        }
       
            if (Input.GetButton("Fire1") && Time.time >= nxtfire)
            {
                nxtfire = Time.time + 1 / firerate;
                shootanimation();
                shoot();
            }
    }


    IEnumerator reload()
    {
        is_reloadin = true;
        Debug.Log("reloading");
        anim.SetBool("is_reloading" , true);
        yield return new WaitForSeconds(reloadtime - .25f);
        anim.SetBool("is_reloading", false);
        yield return new WaitForSeconds(.25f);
        currentammo = maxammo;
        is_reloadin = false;
    }

    public void shoot()
    {
        MuzzleFlash.Play();
        currentammo--;
        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyController target = hit.transform.GetComponent<EnemyController>();
            if(target != null)
            {
                target.TakeDamage(damage);

            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal *100 * i_force);
            }
        }

    }

    void shootanimation()
    {
        anim.SetTrigger("Shoot");
    }

    void playshootsound()
    {
        shootsound.Play();
    }

    void playreloadsound()
    {
        reloadsound.Play();
    }

}
