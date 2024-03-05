using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private GameObject rootGO;
    private void Start()
    {
        rootGO = gameObject.transform.parent.parent.parent.gameObject; // Scenario
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            StartCoroutine(MoveFloor());
        }
    }


    IEnumerator MoveFloor()
    {
        yield return new WaitForSeconds(2);
        rootGO.transform.position = FloorGenerator.instance.nextSpawn.position;

        FloorGenerator.instance.nextSpawn.position = rootGO.transform.GetChild(0).position;
    }
}
