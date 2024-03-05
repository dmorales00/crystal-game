using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class ParticlesPool : MonoBehaviour
{

    public VisualEffect[] particles;
    public static ParticlesPool instance;

    private void Awake()
    {
        instance = this;
    }
}
