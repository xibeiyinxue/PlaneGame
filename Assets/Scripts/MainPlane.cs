using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainPlane : MonoBehaviour, IHealth
{
    #region Prefabricated
    //速度
    [SerializeField]
    private float speed = 5f;
    //添加子弹
    [SerializeField]
    private GameObject bullet;
    //添加死亡特效
    [SerializeField]
    private Object explosionFX;

    private Vector3 direction;

    private float fireRate = 0.5f;
    private float fireTime = 0f;


    private Transform trans;
    private Vector3 vectorSpeed;
    private Rigidbody2D rig;

    private Collider2D coll;

    private float MaxX;
    private float MinX;
    private float MaxY;
    private float MinY;

    private int health = 3;

    //设置死亡事件
    public delegate void OnDead();
    public event OnDead OnDeadEvent;

    public int Health { get { return health; } private set { health = value; } }

    #endregion
    private void Awake()
    {
        trans = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();
        //rig.velocity = Vector3.up;
        coll = GetComponent<Collider2D>();

        coll.enabled = false;
        StartCoroutine(DelayColl());
    }
    private void Start()
    {
        MaxX = PlayXY.MaxX -0.5f;
        MinX = PlayXY.MinX +0.5f;
        MaxY = PlayXY.MaxY -0.5f;
        MinY = PlayXY.MinY +0.5f;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireOnce();
        }
        if (Input.GetButton("Fire1"))
        {
            FireStart();
        }
        ClampFrame();
    }

    //连发
    public void FireStart()
    {
        if (health <= 0) return;
            fireTime += Time.deltaTime;
        if (fireTime > fireRate)
        {
            Instantiate(bullet, trans.position + new Vector3(0,0.5f,0), Quaternion.identity);
            fireTime = 0;
        }
    }

    //单次发射子弹
    public void FireOnce()
    {
        if (health <= 0) return;
        Instantiate(bullet, trans.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        fireTime = 0;
    }

    //限制飞机
    private void ClampFrame()
    {
        trans.position = new Vector3(Mathf.Clamp(trans.position.x, MinX, MaxX),
                                         Mathf.Clamp(trans.position.y, MinY, MaxY),
                                         trans.position.z);
    }

    //飞机移动
    private void FixedUpdate()
    {
        trans.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed;
        Move(direction);
    }

    private void Move(Vector3 direction)
    {
        rig.velocity = direction * speed;
    }

    //private void Fire()
    //{
    //    Instantiate(bullet, trans.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
    //}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!coll.gameObject.CompareTag(gameObject.tag))
        {
            Damage(100/*,this.gameObject*/);
        }
    }

    private IEnumerator DelayColl()
    {
        yield return new WaitForSeconds(3);
        coll.enabled = true;
    }

    public void Damage(int val/*,GameObject initiator*/)
    {
        Health -= val;
        print("MainPlane" + Health);
        if (Health <= 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Instantiate(explosionFX, trans.position, Quaternion.identity);
        if (OnDeadEvent != null)
            OnDeadEvent();
        Destroy(this.gameObject);
    }
}
