using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyGameObjByTime : MonoBehaviour
{
    [SerializeField]
    private float time = 1;

    private void Start()
    {
        Destroy(this.gameObject, time);
    }

}
