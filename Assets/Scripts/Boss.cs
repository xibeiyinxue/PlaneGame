using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


public class Boss : MonoBehaviour, IHealth
{
    #region Prefabricated

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float repeatRate;
    [SerializeField]
    private float speed = 1;

    //创建X，Y轴最大值与最小值
    private float MaxX;
    private float MinX;
    private float MaxY;
    private float MinY;
    private Vector3 direction;

    private int health = 10;

    public int Health
    {
        get { return health; }
        private set { health = value; }
    }
    #endregion
    private void Start()
    {
        //生成子弹
        InvokeRepeating("Fire", 0f, repeatRate);

        //读取框内X，Y的最大值与最小值
        MaxX = PlayXY.MaxX;
        MinX = PlayXY.MinX;
        MaxY = PlayXY.MaxY;
        MinY = PlayXY.MinY;

        //生成 Vector3 并使其向左移动
        direction = Vector3.left;
    }

    private void Update()
    {
        //使其在 X 值最大与最小范围内左右循环移动
        if (transform.position.x > MaxX)
        {
            direction = Vector3.left;
        }
        else if (transform.position.x < MinX)
        {
            direction = Vector3.right;
        }
        Move();
    }

    //创建移动的方法与设置移动速度
    private void Move()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    //子弹生成方法
    private void Fire()
    {
        Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
    }

    //生命值运作
    public void Damage(int val)
    {
        Health -= val;
        print("Boss:" + Health);
    }
}
