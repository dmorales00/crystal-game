using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    // Variables

    // Unity Variables.
    public static PoolingSystem instance;


    // Body
    private void Awake()
    {
        instance = this;
    }
    public GameObject PickItem(GameObject[] allPrefabs, List<GameObject> itemsPool)
    {
        bool pickedItem = false;
        int i = 0;
        GameObject currentItem = itemsPool[i];
        

        while (!pickedItem)
        {
            currentItem = itemsPool[i];

            if (currentItem.activeInHierarchy) // If the current object is active, skip it.
            {
                i++;
                if (i > itemsPool.Count) 
                {
                    currentItem = Instantiate(allPrefabs[(int)(Random.Range(0, allPrefabs.Length + 1))], this.gameObject.transform.position,Quaternion.identity);
                    currentItem.transform.SetParent(this.gameObject.transform);
                    itemsPool.Add(currentItem);
                }
            }
            else // If the object is not-active, set it active and leave the while loop
            {
                currentItem.SetActive(true);
                pickedItem = true;
            }
        }

        return currentItem;
    }

    public void ReturnItem(GameObject returningItem,Transform poolGO) // When objects "despawn", returns it to the pool position and make it not-active.
    {
        returningItem.transform.position = poolGO.position;
        returningItem.SetActive(false);
    }
}
