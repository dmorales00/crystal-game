using System.Collections;
using UnityEngine;



public class Obstacle : MonoBehaviour
{
    // Variables
    [SerializeField] private float _despawnTime = 7f;
    private Transform _tf;
    public float Speed = 4;
    public float timer;
    private Color randomColor;

    // Body

    private void Awake()
    {
        _tf = GetComponent<Transform>();
    }


    private void Start()
    {
        _despawnTime = 7f;
        timer = 0;

        if (gameObject.CompareTag("Enemy"))
        {
            StartCoroutine("SlimeScale");
            RandomizeColor();
        }
    }

    private void OnEnable()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            StartCoroutine("SlimeScale");
            RandomizeColor();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        _tf.Translate(Vector3.left * Speed * Time.deltaTime, Space.World);

        if (timer >= _despawnTime)
        {
            timer = 0;
            PoolingSystem.instance.ReturnItem(gameObject,gameObject.transform.parent);
        }
        
    }
    public void RandomizeColor()
    {
        for (int i = 0; i < gameObject.transform.childCount;i++)
        {
            if (i != 2)
            {
                randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            }

            gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = randomColor;
        }
    }

    IEnumerator SlimeScale()
    {
        bool scaleUp = false;

        while (gameObject.activeInHierarchy)
        {
            if(!scaleUp)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.01f, gameObject.transform.localScale.y - 0.01f, gameObject.transform.localScale.z);

                if(gameObject.transform.localScale.x <= 0.1f)
                {
                    scaleUp = !scaleUp;
                }
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.01f, gameObject.transform.localScale.y + 0.01f, gameObject.transform.localScale.z);

                if (gameObject.transform.localScale.x > 0.1f && gameObject.transform.localScale.x <= 0.15f )
                {
                    scaleUp = !scaleUp;
                }
                yield return new WaitForSeconds(0.3f);
            }
            
        }
        yield return null;
    }
}


