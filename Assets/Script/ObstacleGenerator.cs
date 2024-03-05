using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Variables

    // Integers & Floats.
    private float _spawnTime;
    private float _randomTime;
    private const float _ToPlayerDistance = 15;

    // Vector3 Variables.
    private Vector3 _startingPosVector;
    private Vector3 _updatePosVector;
    

    void Start()
    {
        _randomTime = Random.Range(0.5f, 2);
        //_startingPosVector = new Vector3(Player.instance.gameObject.transform.position.x + _ToPlayerDistance,
        //                                Player.instance.gameObject.transform.position.y,
        //                                gameObject.transform.position.z);      

        //gameObject.transform.position = _startingPosVector;
    }


    void Update()
    {
        _spawnTime += Time.deltaTime;

        _updatePosVector = new Vector3(Player.instance.gameObject.transform.position.x + _ToPlayerDistance,
                              gameObject.transform.position.y,
                              gameObject.transform.position.z);

        gameObject.transform.position = _updatePosVector;

        SpawnObstacle();
    }

    private void SpawnObstacle()
    {
        GameObject currentItem;
        if (_randomTime <= _spawnTime) // Random timer (0.5-2s)
        {
            // Moves the picked item to x and y SpawnObstacle position, but takes a random lane. 
            currentItem = PoolingSystem.instance.PickItem(ObstaclesPool.instance.allObstacles, ObstaclesPool.instance.obstaclesList);
            currentItem.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y, PlayerPos.instance.allPos[Random.Range(0, 3)].transform.position.z); ;
            _randomTime = Random.Range(0.5f, 2);
            _spawnTime = 0;
        }
    }
}