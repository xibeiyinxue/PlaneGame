using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalEnemy : MonoBehaviour, IHealth
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float repeatRate;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private GameObject explosionFX;

    protected int health = 10;
    public int Health
    {
        get { return health; }
        protected set { health = value; }
    }

    private float MinY;
    private Vector3 direction;

    private MainPlane mainPlane;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, repeatRate);
        MinY = PlayXY.MinY - 1;
        direction = Vector3.down;
    }

    private void Update()
    {
        if (transform.position.y < MinY)
            Destroy(this.gameObject);

        Move();
    }

    private void Move()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
    }

    public void Damage(int val)
    {
        Health -= val;
        print("Boss (1) :" + health);
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
