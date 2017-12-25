using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public abstract class BulletBase : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1f;//子弹速度
    [SerializeField]
    protected int power = 1;//子弹威力

    protected Transform trans;

    private void Awake()
    {
        trans = GetComponent<Transform>();
    }
    
    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IHealth>() != null && !collision.CompareTag(gameObject.tag))
        {
            collision.GetComponent<IHealth>().Damage(power);
            //print("Bullet" + collision.GetComponent<IHealth>().Health);
            Destroy(this.gameObject);
        }
    }
}
