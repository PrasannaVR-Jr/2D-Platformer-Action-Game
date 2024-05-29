using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReader : MonoBehaviour
{
    public string[] Tags;
    public string FileName;
    List<EnemyDetails> enemyDetails=new List<EnemyDetails>();

    // Start is called before the first frame update
    void Start()
    {
        ReadSceneObjects();
    }

    void ReadSceneObjects()
    {
        for(int i=0;i<Tags.Length;i++)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(Tags[i]);

            for(int j = 0; j < gameObjects.Length; j++)
            {
                Vector2 pos = gameObjects[j].transform.position;
                enemyDetails.Add(new EnemyDetails(Tags[i], pos));
            }
        }

        EnemyDetailsList enemyDetailsList = new EnemyDetailsList(enemyDetails.ToArray());

        string Data=JsonUtility.ToJson(enemyDetailsList);


        System.IO.File.WriteAllText(Application.dataPath+"/"+FileName+".json",Data);
    }
}
