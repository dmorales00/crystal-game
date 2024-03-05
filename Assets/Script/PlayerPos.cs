using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    //Variables
    public GameObject[] allPos;
    public static PlayerPos instance;

    //Body
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(Player.instance.transform.position.x,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z);
    }
}
