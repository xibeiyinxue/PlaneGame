using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossEnemy : MonoBehaviour,IHealth
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float repeatRate;
    [SerializeField]
    private GameObject explosionFX;

    private float MaxX;
    private float MinX;
    private float MaxY;
    private float MinY;
    private Vector3 direction;

    private Vector3 leftDirection;
    private Vector3 rightDirection;
    
    protected int health = 10;
    public int Health
    {
        get { return health; }
        protected set { health = value; }
    }

    private void Start()
    {
        InvokeRepeating("Fire", 0f, repeatRate);
        MaxX = PlayXY.MaxX - 1;
        MinX = PlayXY.MinX + 1;
        MaxY = PlayXY.MaxY;
        MinY = PlayXY.MinY - 1;

        leftDirection = (Vector3.left + Vector3.down * 0.1f).normalized;
        rightDirection = (Vector3.right + Vector3.down * 0.1f).normalized;
        direction = leftDirection;
    }
    
    private void Update()
    {
        if (transform.position.y < MinY)
            Destroy(this.gameObject);

        if (transform.position.x > MaxX)
        {
            direction = leftDirection;
        }
        else if (transform.position.x <MinX)
        {
            direction = rightDirection;
        }

        Move();
    }

    private void Move()
    {
        transform.Translate(direction * Time.deltaTime * 0.5f);
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
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
