using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyDetailsList
{
    public EnemyDetails[] enemyDetailsArray;

    public EnemyDetailsList(EnemyDetails[] enemyDetailsArray)
    {
        this.enemyDetailsArray = enemyDetailsArray;
    }

}

[System.Serializable]
public class EnemyDetails
{
    public string Tag;
    public Vector2 Position;

    public EnemyDetails(string tag,Vector2 pos)
    {
        Tag = tag;
        Position = pos;
    }
}

[System.Serializable]
public class PoolingObjects
{
    public string Tag;
    public GameObject Reference;
    public Queue<GameObject> gameObjectsPool=new Queue<GameObject>();
}
