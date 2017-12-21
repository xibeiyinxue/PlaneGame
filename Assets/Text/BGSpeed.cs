using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGSpeed : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Renderer rend;
    private Material met;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        met = rend.material;
    }
    
    private void Update()
    {
        met.SetTextureOffset("_MainTex", new Vector3(0, Time.time * speed));
    }
}
