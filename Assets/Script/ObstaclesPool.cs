using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPool : MonoBehaviour
{
    // Variables

    // Lists & Arrays.
    public List<GameObject> obstaclesList;
    public GameObject[] allObstacles;
    // Unity Variables.
    private GameObject _currentItem;
    public static ObstaclesPool instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i <= 10; i++)
        {
            _currentItem = Instantiate(allObstacles[(int)(Random.Range(0,allObstacles.Length-1))], gameObject.transform.position, Quaternion.identity); // Instantiate a random prefab from "allObstacles"
            obstaclesList.Add(_currentItem);
            _currentItem.transform.SetParent(gameObject.transform);
            _currentItem.SetActive(false);
        }
    }


}
