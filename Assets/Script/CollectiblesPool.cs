using System.Collections.Generic;
using UnityEngine;

public class CollectiblesPool : MonoBehaviour
{
    // Variables

    // Lists & Arrays.
    public List<GameObject> collectiblesList;
    public GameObject[] allCollectibles;
    // Unity Variables.
    private GameObject _currentItem;
    public static CollectiblesPool instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i <= 5; i++)
        {
            _currentItem = Instantiate(allCollectibles[(int)(Random.Range(0, allCollectibles.Length - 1))], this.gameObject.transform.position, Quaternion.identity); // Instantiate a random prefab from "allColectibles"
            collectiblesList.Add(_currentItem);
            _currentItem.transform.SetParent(this.gameObject.transform);
            _currentItem.SetActive(false);
        }
    }




}
