using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float EnterHealth = 100;
    [HideInInspector] public float Health;
    EnemyMotion enemyMotion;
    public float speed = 5;
    float WalkLimiter = 0;
    public float WalkLimit = 3;
    bool isPlayerDetected = false;
    [HideInInspector] public float velocity;
    [HideInInspector] public bool canAttack = false;
    [HideInInspector] public GameObject player;
    public UnityEngine.UI.Slider HealthBar;
    float maxHealth;
    GameManager gameManager;
    private void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
        //gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();

        Health = EnterHealth;
        HealthBar.value = Health/maxHealth;
        maxHealth = Health;

        enemyMotion = GetComponent<Animator>().GetBehaviour<EnemyMotion>();
        enemyMotion.Enemy = this;
    }
    

    private void Update()
    {
        RaycastHit2D rh2D= Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 5f,PlayerLayer);
        if(rh2D)
        {
            isPlayerDetected = true;
            velocity = 1;
        }
        
        

        //Debug.DrawRay(transform.position,transform.TransformDirection(Vector2.right)*5f);
        
        if(isPlayerDetected && player.GetComponent<Player1>().Health>0)
        {
            Vector3 PlayerPosition = FindObjectOfType<Player1>().transform.position;

            float PlayerDistance = Vector2.Distance(transform.position, PlayerPosition);
            Vector3 direction = (PlayerPosition - transform.position).normalized;
            

            if (PlayerDistance > 2f && PlayerDistance < 10f)
            {
                transform.position = Vector2.MoveTowards(transform.position, PlayerPosition, (speed + 0.4f) * Time.deltaTime);
                if (direction.x > 0)
                    transform.rotation = Quaternion.Euler(0,0,0);
                else if (direction.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else
            {
                velocity = 0;

                if (PlayerDistance < 2f)
                {
                    canAttack = true;

                }
                else
                    canAttack = false;
            }


        }
            

        else
        {
            velocity = 1;
            WalkLimiter += Time.deltaTime;
            transform.Translate(speed * Time.deltaTime, 0, 0);

            if (WalkLimiter > WalkLimit)
            {
                transform.Rotate(0, 180, 0);
                WalkLimiter = 0;
            }
        }
        

    }

    public void TakeDamage(float DamageAmount)
    {
        if(Health>0)
            Health -= DamageAmount;
        else
            Health = 0;
            
        
        HealthBar.value = Health / maxHealth;
    }

    void Dissapear()
    {

        gameObject.SetActive(false);
    }

    void Attack()
    {
        if(Physics2D.OverlapCircle(transform.position + transform.right, 0.8f,PlayerLayer))
        {
            player.GetComponent<Player1>().TakeDamage(5);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position+transform.right,0.8f);
    }

    private void OnDisable()
    {
        gameManager.EnqueToQueue(tag,gameObject);
    }
}
