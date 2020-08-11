using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private GameObject zombieprefab;
    public Transform[] zombie1_spawnpoints;
    public Transform[] zombie2_spawnpoints;
    private EnemyController e_con;

    private void Awake()
    {
        e_con = GetComponent<EnemyController>();
    }
    void Start()
    {
        for (int i = 0; i < zombie1_spawnpoints.Length; i++)
        {
            spawn_zombie1();
           
        }
        for (int i = 0; i < zombie2_spawnpoints.Length; i++)
        {
            spawn_zombie2();

        }
    }

    void spawn_zombie2()
    {
       
            int randomNumber = Mathf.RoundToInt(UnityEngine.Random.Range(0f, zombie2_spawnpoints.Length - 1));
            Instantiate(zombieprefab, zombie2_spawnpoints[randomNumber].transform.position, Quaternion.identity);
      
        
    }

    private void OnEnable()
    {
       EnemyController.OnEnemyKilled += spawn_zombie1;
        

    }

    void spawn_zombie1()
    {
        
            int randomNumber = Mathf.RoundToInt(UnityEngine.Random.Range(0f, zombie1_spawnpoints.Length - 1));
            Instantiate(zombieprefab, zombie1_spawnpoints[randomNumber].transform.position, Quaternion.identity);
            

    }

    void stopspawnning()
    {
        if (e_con.kill_count == 25)
        {
            zombieprefab.SetActive(false);
        }
    }
        
  
   
}
