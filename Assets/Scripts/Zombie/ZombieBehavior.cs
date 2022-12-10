using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class ZombieBehavior : MonoBehaviour
{
    //// Collider support
    //[SerializeField]
    //SkinnedMeshRenderer meshRendererHead;
    //[SerializeField]
    //MeshCollider colliderHead;
    //[SerializeField]
    //SkinnedMeshRenderer meshRendererBody;
    //[SerializeField]
    //MeshCollider colliderBody;
    [SerializeField]
    AudioSource zomNoise;
    // State Support
    private IState currentState;
    
    // Status Alive or Death
    Health health;
    public bool isAlive = true;

    // Animation support
    [SerializeField] Animator animator;
    [SerializeField] ZombieAnimName currentAnim;

    // Moving
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public GameObject player;

    // Attack
    public bool canAttacck = false;
    [SerializeField] GameObject AttackBox;
    public float attackRange = 1.2f;
    public float attackDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
        OnInit();
    }
    private void OnEnable()
    {
        OnInit();
    }
    public void OnInit()
    {
        health = GetComponent<Health>();
        health.OnInit(Config.zombieMaxHealth);
        agent.speed = Config.zombieMoveSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        zomNoise.mute = false;
        DeSetTriggerAllCollider();

        ChangeState(new IdleState());
        canAttacck = false;
        isAlive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            CheckAlive();
        }
        

        if (isAlive)
        {

            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }
        else
        {
            
        }
        
    }

    #region METHOD CHECKING & Animation & Collider draw & ChangeState
    public void SetTriggerAllCollider()
    {
        var coll = gameObject.GetComponentsInChildren<Collider>();
        foreach(Collider c in coll)
        {
            if (c.tag == "Zombie")
            {
                c.isTrigger = true;
            }
        }
    }

    public void DeSetTriggerAllCollider()
    {
        var coll = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider c in coll)
        {
            if(c.tag == "Zombie")
            {
                c.isTrigger = false;
            }
           
        }
    }

    /// <summary>
    /// Change State
    /// </summary>
    /// <param name="newState">name of new state</param>
    public void ChangeState(IState newState)
    {
        // neu da co state thi exit truoc
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        // gan state moi vao
        currentState = newState;
        // chay onter cua state moi
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    private void CheckAlive()
    {
        if (health.currentHealth == 0)
        {

            isAlive = false;
            zomNoise.mute = true;
            AudioManager.Play(AudioName.ZombieDead);
            SetTriggerAllCollider();
            player.GetComponent<Player>().addScore();
            
            //Destroy(colliderHead);
            //Destroy(colliderBody);
            // random giua 2 anim chet
            ChangeAnim(Random.Range(0, 2) == 0 ? ZombieAnimName.Death1 : ZombieAnimName.Death2);
            agent.isStopped = true;
            Invoke(nameof(DeSpawn), 3f);
        }
    }

    private void ChangeAnim(ZombieAnimName anim)
    {

        if(currentAnim != anim)
        {
            currentAnim = anim;
            animator.SetTrigger(anim.ToString());
        }
    }

    private void DeSpawn()
    {
        ZombieSpawner.instance.DeSpawnAZombie(this.gameObject);
    }

    #endregion

    #region BEHAVIOR
    public void StopMoving()
    {
        agent.isStopped = true;
        ChangeAnim(ZombieAnimName.Idle);
    }

    public void RunToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        ChangeAnim(ZombieAnimName.AttackRun);
    }

    public bool CheckCanAttack()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            canAttacck = true;
            return true;
        }
        canAttacck = false;
        return false;
    }

    public void ActiveAttackBox()
    {
        AttackBox.SetActive(true);
        
    }
    public void DeActiveAttackBox()
    {
        AttackBox.SetActive(false);
    }

    public void AttackPlayer()
    {

        transform.LookAt(player.transform);

        

        agent.isStopped = true;
        ChangeAnim(ZombieAnimName.Attack);
        ActiveAttackBox(); // bat roi tat

    }
    #endregion
}
