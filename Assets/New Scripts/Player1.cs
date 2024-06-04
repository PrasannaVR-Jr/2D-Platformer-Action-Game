using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    PlayerMotion1 PlayerMotion1;
    public LayerMask enemyLayer;
    [Header("GroundCheck")]
    public Transform ReferenceOffset;
    [SerializeField] Transform GroundTransformRef;
    public bool isGrounded;
    public LayerMask GroundLayers;
    [Header("Player Attributes")]
    public float speed = 3;
    public float jumpSpeed = 10f;
    public float Health=100;
    public float MaxHealth;
    [Header("Other Attributes")]
    public UnityEngine.UI.Slider HealthBar;
    public UnityEngine.UI.Image HealthMaskImage;
    public Transform GridHealth;
    List<GameObject> Hearts=new List<GameObject>();

    GameManager gameManager;
    int HeartCount;
    public int NumberOfHearts;

    [HideInInspector] public float HorizontalInput;

    void Start()
    {
        MaxHealth = Health;
        gameManager=FindObjectOfType<GameManager>();
        PlayerMotion1=GetComponent<Animator>().GetBehaviour<PlayerMotion1>();
        PlayerMotion1.Player1 = this;
        if(HealthBar!=null)
            HealthBar.value = Health;

        foreach (Transform Imag in GridHealth)
        {
            Hearts.Add(Imag.gameObject);
        }
        //HealthDiv= Health / Hearts.Count;
        HeartCount = Hearts.Count;

        for(int i = 0; i < NumberOfHearts; i++)
        {
            Destroy(Hearts[0]);
            Hearts.Remove(Hearts[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");


        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            HorizontalInput *= 2;
        }

        if (HorizontalInput > 0)
        {
            transform.rotation = Quaternion.identity;
        }
        if (HorizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        transform.Translate(Mathf.Abs(HorizontalInput) * speed * Time.deltaTime, 0, 0);

        isGrounded = Physics2D.OverlapCircle(GroundTransformRef.position, 0.3f, GroundLayers) != null ? true:false ;

        if(transform.position.y<-8f || Health<=0f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    void Attack()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(new Vector2(ReferenceOffset.position.x, ReferenceOffset.position.y), 0.4f);

        for (int i = 0; i < coll.Length; i++)
        {
            coll[i].GetComponent<EnemyController>().TakeDamage(20);
        }
    }

    public void TakeDamage(float Damage)
    {
        HealthMaskImage.fillAmount = Health / MaxHealth;

        if (Health > 0)
        {
            Health -= Damage;
            if(HealthBar!=null)
                HealthBar.value = Health;
        }
        else
        {
            Health = 0;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnDrawGizmos()
    {
        //Vector3 transformOffset = transform.TransformDirection(Vector3.right + new Vector3(0.4f, 0.4f, 0));

        Gizmos.DrawWireSphere(ReferenceOffset.position, 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint1"))
        {
            //Debug.Log(collision.GetComponent<Checkpoint>().EnemyData);

            string TextAsset = collision.GetComponent<Checkpoint>().EnemyData.ToString();
            ReadTextAsset(TextAsset);
        }

        if(collision.CompareTag("Finish"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    void ReadTextAsset(string TextAsset)
    {
        
        EnemyDetailsList enemyDetailsList = JsonUtility.FromJson<EnemyDetailsList>(TextAsset);
        EnemyDetails[] enemyDetails = enemyDetailsList.enemyDetailsArray;

        for (int i = 0; i < enemyDetails.Length; i++)
        {
            //Debug.Log(enemyDetails[i].Tag + " " + enemyDetails[i].Position);
            gameManager.GetObjectFromPooling(enemyDetails[i].Tag, enemyDetails[i].Position);
        }
    }
}
