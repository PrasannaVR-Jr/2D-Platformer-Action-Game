using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PoolingObjects[] poolingObjects;

   
    public GameObject GetObjectFromPooling(string Tag,Vector2 Pos)
    {
        return GetObjectFromPooling(GetIndexFromTag(Tag),Pos);
    }

    public GameObject GetObjectFromPooling(int index, Vector2 Pos)
    {
        int QueueCount = poolingObjects[index].gameObjectsPool.Count;

        GameObject ObjectFromQueue;

        if (QueueCount > 0)
        {
            ObjectFromQueue = poolingObjects[index].gameObjectsPool.Dequeue();
            ObjectFromQueue.SetActive(true);
            ObjectFromQueue.transform.position = Pos;
        }
        else
        {
            ObjectFromQueue = Instantiate(poolingObjects[index].Reference,Pos,Quaternion.identity);
        }
        return ObjectFromQueue;
    }

    public int GetIndexFromTag(string Tag)
    {
        switch (Tag)
        {
            case "Enemy1":
                return 0;
            case "Enemy2":
                return 1;
            case "Enemy3":
                return 2;
        }

        return 0;
    }

    public void EnqueToQueue(string Tag, GameObject gameObject)
    {
        poolingObjects[GetIndexFromTag(Tag)].gameObjectsPool.Enqueue(gameObject);
    }

}
