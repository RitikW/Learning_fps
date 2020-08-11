using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
    {
    PATROL,
    CHASE,
    ATTACK
    }

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_anim;
    private NavMeshAgent navagent;

    private EnemyState enem_state;

    public float walk_speed ;
    public float run_speed ;
    public float chase_dist ;
    private float current_chase_dist;
    public float attack_dist ;
    public float chase_after_attack ;

    public float patrol_rad_min = 200f, patrol_rad_max = 600f;
    public float patrol_time = 150f;
    private float patrol_timer;

    public float attack_pause = 1f;
    private float attack_timer;

    private Transform target1;
    private Enemy_Audio e_aud;
    public int health;
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    [SerializeField]
    private GameObject zombieprefab;
    public Transform[] zombie_spawnpoints;
    [HideInInspector]
    public int kill_count = 0;


    // Start is called before the first frame update
    void Awake()
    {
        enemy_anim = GetComponent<EnemyAnimator>();
        navagent = GetComponent<NavMeshAgent>();

        target1 = GameObject.FindWithTag("Player").transform;

        e_aud = GetComponentInChildren<Enemy_Audio>();
    }

    void Start()
    {
        enem_state = EnemyState.PATROL;

        patrol_timer = patrol_time;

        attack_timer = attack_pause;

        current_chase_dist = chase_dist;
    }

    // Update is called once per frame
    void Update()
    {
        if (enem_state == EnemyState.PATROL)
        {
            patrol();
        }
        if (enem_state == EnemyState.CHASE)
        {
            chase();
        }
        if (enem_state == EnemyState.ATTACK)
        {
            attack();
        }
    }

    void patrol()
    {
        navagent.isStopped = false;
        navagent.speed = walk_speed;

        patrol_timer += Time.deltaTime;
        if (patrol_timer > patrol_time)
        {
            SetNewRandomPlace();
            patrol_timer = 0f;
        }
        if (navagent.velocity.sqrMagnitude > 0)
        {
            enemy_anim.Walk(true);
        }
        else
        {
            enemy_anim.Walk(false);
        }
        if (Vector3.Distance(transform.position, target1.position) <= chase_dist)
        {
            enemy_anim.Walk(false);
            enem_state = EnemyState.CHASE;
            e_aud.ScreamSound();
        }
    }

    void chase()
    {
        navagent.isStopped = false;
        navagent.speed = run_speed;
        navagent.SetDestination(target1.position);
        if (navagent.velocity.sqrMagnitude > 0)
        {
            enemy_anim.Run(true);
        }
        else
        {
            enemy_anim.Run(false);
        }
        if (Vector3.Distance(transform.position, target1.position) <= attack_dist)
        {
            enemy_anim.Run(false);
            enemy_anim.Walk(false);
            enem_state = EnemyState.ATTACK;

            if (chase_dist != current_chase_dist)
            {
                chase_dist = current_chase_dist;
            }
        }
        else if (Vector3.Distance(transform.position, target1.position) > chase_dist)
        {
            enemy_anim.Run(false);
            enem_state = EnemyState.PATROL;
            patrol_timer = patrol_time;
            if (chase_dist != current_chase_dist)
            {
                chase_dist = current_chase_dist;
            }
        }



    }
    void attack()
    {
        navagent.velocity = Vector3.zero;
        navagent.isStopped = true;
        attack_timer += Time.deltaTime;
        if (attack_timer > attack_pause)
        {
            enemy_anim.Attack();
            attack_timer = 0f;
            e_aud.AttackSounds();
        }

        if (Vector3.Distance(transform.position, target1.position) > attack_dist + chase_after_attack)
        {
            enem_state = EnemyState.CHASE;
        }
    }
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
        sound();
        Destroy(gameObject);
        kill_count+=1;
        Debug.Log(+kill_count);
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
    }

    IEnumerator sound()
    {
        yield return new WaitForSeconds(.3f);
        e_aud.DeathSound();
    }

    void SetNewRandomPlace()
    {
        float random_rad = Random.Range(patrol_rad_min, patrol_rad_max);
        Vector3 random_dir = Random.insideUnitSphere * random_rad;
        random_dir += transform.position;

        NavMeshHit navhit;
        NavMesh.SamplePosition(random_dir, out navhit, random_rad, -1);
        navagent.SetDestination(navhit.position);
    }

   
}
