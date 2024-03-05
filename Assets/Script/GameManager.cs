using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    // Variables
    public static GameManager instance;
    private Vector3 _playerBackStart;
    [SerializeField] private CinemachineVirtualCamera _camera;
    private const float _DistanceLimit = 300f;
    public float lastPos;
    private int currentMoney;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject lookAtObject;
    [HideInInspector] public float gameTime=0;



    void Awake()
    {
        if (UIManager.instance == null)
        {
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }
        instance = this;
        currentMoney = PlayerPrefs.GetInt("Money",0);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
    }
    private void Start()
    {
        GlobalVariables.gameScore = 0;
        lastPos = (float) System.Math.Round(character.transform.position.x,2);
    }

    // Body
    void Update()
    {
        gameTime += Time.deltaTime;

        if (UIManager.instance != null)
        {
            CalcScore();
        }
         
        // Code that moves everything back to 0,0,0
        if(Player.instance.transform.position.x >= _DistanceLimit)
        {   
            foreach(GameObject scenario in FloorGenerator.instance.floorList)
            {
                scenario.transform.position = new Vector3(scenario.transform.localPosition.x - 300, scenario.transform.position.y, scenario.transform.position.z);
            }

            foreach(GameObject obstacle in ObstaclesPool.instance.obstaclesList) // Moves every obstacle in-screen back to its current pos - 300f.
            {
                if (obstacle.activeSelf)
                {
                    obstacle.transform.position = new Vector3(obstacle.transform.position.x - 300f, obstacle.transform.position.y, obstacle.transform.position.z);
                }
            }

            

            foreach (GameObject collectible in CollectiblesPool.instance.collectiblesList) // Moves every obstacle in-screen back to its current pos - 300f.
            {
                if (collectible.activeInHierarchy)
                {
                    collectible.transform.position = new Vector3(collectible.transform.position.x - 300f, collectible.transform.position.y, collectible.transform.position.z);
                }
            }


            _playerBackStart = new Vector3(0, character.transform.position.y, character.transform.position.z);

            // Moves the next floor position to its actual position - 300f
            FloorGenerator.instance.nextSpawn.position = new Vector3(FloorGenerator.instance.nextSpawn.position.x - 300, FloorGenerator.instance.nextSpawn.position.y, FloorGenerator.instance.nextSpawn.position.z);

            // Cinemachine class. (Transform target, Vector3 positionDelta). Tells the camera that the player is going to warp so the camera does too.
            _camera.OnTargetObjectWarped(lookAtObject.transform,_playerBackStart - character.transform.position);
            
            // Returns the player to its position but x = x-300 so it returns to the start point.
            character.transform.position = _playerBackStart;
           
        }
    }
   
    private void CalcScore()
    {
        float currentPos = (float)System.Math.Round(character.transform.position.x, 2);
        int posResetted=0;
        //Debug.Log("Valor lastpos = " + lastPos);
        //Debug.Log("Valor currentpos = " + currentPos);
        if (lastPos > currentPos)
        {
            posResetted += 300;
        }
        GlobalVariables.gameScore += posResetted + (int)(currentPos - lastPos); // Gets the actual score and adds the distance diference between this and last frame.
        UIManager.instance.ScoreUI();
        lastPos = Mathf.Floor(currentPos);

        // When player returns to x = 0, make it so score keeps value and don't go back to 0.
    }

    public void LifesSpent()
    {
        PlayerPrefs.SetInt("Money", currentMoney + Player.instance.money);
        PlayerPrefs.SetInt("Score", GlobalVariables.gameScore);
        UIManager.instance.Defeat();
    }
}
