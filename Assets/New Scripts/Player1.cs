using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    PlayerMotion1 PlayerMotion1;
    public LayerMask enemyLayer;
    public float speed = 3;
    public float jumpSpeed = 10f;
    public Transform ReferenceOffset;
    public float Health=100;
    public UnityEngine.UI.Slider HealthBar;
    public UnityEngine.UI.Image HealthMaskImage;
    public float MaxHealth;
    public Transform GridHealth;
    List<GameObject> Hearts=new List<GameObject>();
    float HealthDiv;
    GameManager gameManager;
    int HeartCount;
    public int NumberOfHearts;
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
        //3D
        //Collider[] coll=Physics.OverlapSphere(ReferenceOffset.position, 0.4f,enemyLayer);
        HealthMaskImage.fillAmount = Health / MaxHealth;

        //int NumberofHearts = (int)(Health / HealthDiv);
        //int TotalHearts = HeartCount - NumberOfHearts;

        
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
        if (Health > 0)
        {
            Health -= Damage;
            if(HealthBar.gameObject!=null)
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
