using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainPlane : MonoBehaviour, IHealth
{
    #region Prefabricated
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private GameObject bullet;
    private Vector3 direction;
    [SerializeField]
    private Object explosionFX;

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
        rig.velocity = Vector3.up;
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
        StartCoroutine(DelayColl());
    }
    private void Start()
    {
        MaxX = PlayXY.MaxX;
        MinX = PlayXY.MinX;
        MaxY = PlayXY.MaxY;
        MinY = PlayXY.MinY;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        ClampFrame();
    }

    private void ClampFrame()
    {
        trans.position = new Vector3(Mathf.Clamp(trans.position.x, MinX, MaxX),
                                         Mathf.Clamp(trans.position.y, MinY, MaxY),
                                         trans.position.z);
    }

    private void FixedUpdate()
    {
        trans.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed;
        Move(direction);
    }

    private void Move(Vector3 direction)
    {
        rig.velocity = direction * speed;
    }

    private void Fire()
    {
        Instantiate(bullet, trans.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!coll.gameObject.CompareTag(gameObject.tag))
        {
            Damage(100/*,this.gameObject*/);
        }
    }

    private IEnumerator DelayColl()
    {
        yield return new WaitForSeconds(2);
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
