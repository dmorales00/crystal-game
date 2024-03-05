using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public Vector3 RotAngle;
    public float RotSpeed;


    void Update()
    {
          this.transform.Rotate(RotAngle * RotSpeed * Time.deltaTime);
    }
    public void DistanceColor(int Pos)
    {
        MeshRenderer rend = this.gameObject.GetComponent<MeshRenderer>();
        Debug.Log(Pos.ToString());
        rend.material.color = Color.white;
        Color newColor = Color.white / Pos;
       
        rend.material.color = new Color(newColor.r, newColor.g, newColor.b, 1);
        
      

        
    }
}
