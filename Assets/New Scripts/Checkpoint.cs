using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public TextAsset EnemyData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Animator>().SetBool("OpenFlag", true);
        }
    }

    
}
