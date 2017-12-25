using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tank : MonoBehaviour, IHealth
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject track;
    [SerializeField]
    private GameObject explosionFX;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform barrel;

    private float fireTime;
    private float fireRate = 0.5f;

    private float MaxX;
    private float MinX;
    private Vector3 direction;
    private Vector3 fireDirection;
    
    public float rotateSpeed;

    protected int health = 10;
    public int Health
    {
        get { return health; }
        protected set { health = value; }
    }

    private void Awake()
    {
        MaxX = PlayXY.MaxX + 1;
        MinX = PlayXY.MinX - 1;
    }

    private void Start()
    {
        //InvokeRepeating("Fire", 0f, 1);
        //FireStart();
    }

    private void Update()
    {
        direction = (Vector3.left).normalized;
        if (transform.position.x < MinX || transform.position.x > MaxX)
            Destroy(this.gameObject);
        Move();
        if (LevelDirector.Instance.CurrentPlane == null) return;
        player = LevelDirector.Instance.CurrentPlane.gameObject;
        FireStart();
        BrrelRotate();
    }

    private void Move()
    {
        transform.Translate(direction * Time.deltaTime * 0.5f);
    }

    //private void Fire()
    //{
    //    Instantiate(bulletPrefab, track.transform.position, Quaternion.Euler(0,0,float z));
    //}

    private void FireStart()
    {
        fireTime += Time.deltaTime;
        if (fireTime > fireRate)
        {
            Instantiate(bulletPrefab, track.transform.position + new Vector3(0, 0.5f, 0), barrel.rotation);
            fireTime = 0;
        }
    }

    private void BrrelRotate()
    {
        fireDirection = player.transform.position - transform.position;
        fireDirection.z = 0;
        fireDirection = fireDirection.normalized;
        barrel.rotation = Quaternion.RotateTowards(barrel.rotation, Quaternion.FromToRotation(Vector3.down, fireDirection), rotateSpeed * Time.deltaTime);
    }

    public void Damage(int val)
    {
        Health -= val;
        print("Boss :" + health);
        if (Health <= 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        LevelDirector.Instance.Score += 10;
        Destroy(this.gameObject);
    }
}
