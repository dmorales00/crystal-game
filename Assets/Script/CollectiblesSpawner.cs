using UnityEngine;

public class CollectiblesSpawner : MonoBehaviour
{
    // Variables

    // Integers & Floats.
    private float _spawnTime;
    private float _randomTime;
    private const float _ToPlayerDistance = 15;
    private float _randomY;


    // Vector3 Variables.
    private Vector3 _startingPosVector;
    private Vector3 _updatePosVector;


    void Start() 
    {
        _randomTime = Random.Range(3f,5f);
        _randomY = Random.Range(2f, 3f);
        _startingPosVector = new Vector3(Player.instance.gameObject.transform.position.x + _ToPlayerDistance,
                                        Player.instance.gameObject.transform.position.y,
                                        this.gameObject.transform.position.z);

        this.gameObject.transform.position = _startingPosVector;
    }


    void Update()
    {
        _spawnTime += Time.deltaTime;

        _updatePosVector = new Vector3(Player.instance.gameObject.transform.position.x + _ToPlayerDistance,
                              this.gameObject.transform.position.y,
                              this.gameObject.transform.position.z);

        this.gameObject.transform.position = _updatePosVector;

        SpawnCollectible();
    }

    private void SpawnCollectible()
    {
        if (_randomTime <= _spawnTime) // Random timer (3-5s)
        {
            // Moves the picked item to x and y SpawnObstacle position, but takes a random lane.
            int RandomPos = Random.Range(0, 3);
            GameObject currentItem = PoolingSystem.instance.PickItem(CollectiblesPool.instance.allCollectibles,CollectiblesPool.instance.collectiblesList);
            currentItem.transform.position = new Vector3(this.gameObject.transform.position.x, _randomY , PlayerPos.instance.allPos[RandomPos].transform.position.z); ;
            _randomTime = Random.Range(3f, 5f);
            _randomY = Random.Range(2f, 4f);
            _spawnTime = 0;
            currentItem.GetComponent<CoinRotation>().DistanceColor(RandomPos + 1);
        }
        
    }
}
