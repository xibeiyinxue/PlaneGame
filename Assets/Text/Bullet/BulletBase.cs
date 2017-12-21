using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBase : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1f;
    [SerializeField]
    protected int power = 1;

    private string myTag;
    protected Transform trans;

    private void Awake()
    {
        myTag = gameObject.tag;
        trans = GetComponent<Transform>();
    }
    
    private void Update()
    {
        Move();
    }

    protected virtual void Move();

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IHealth>() != null && !collision.CompareTag(myTag))
        {
            collision.GetComponent<IHealth>().Damage(power);
        }
    }
}
