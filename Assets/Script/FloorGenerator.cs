using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    // Variables

    // Unity Variables
    public GameObject scenario;
    public Transform nextSpawn;
    private GameObject _lastFloor;
    public static FloorGenerator instance;
    public List<GameObject> floorList;
    [SerializeField] private GameObject _firstfloor;

    // Body
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        floorList.Add(_firstfloor);
        for (int i = 0; i < 5; i++)
        {
            SpawnFloor();

        }
    }

    public void SpawnFloor()
    {
        _lastFloor = Instantiate(scenario,nextSpawn.position,Quaternion.identity);
        _lastFloor.transform.SetParent(_firstfloor.transform.parent);
        floorList.Add(_lastFloor);
        nextSpawn.position = _lastFloor.transform.GetChild(0).position;
    }
}
