using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


public class Boss : MonoBehaviour
{
    #region Prefabricated
    private Transform bossTrans;
    private AudioSource bossAudio;
    private Renderer rend;
    private Collider2D boss;

    private int HP;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float repeatRate;
    [SerializeField]
    private float speed = 4;

    private float MaxX;
    private float MinX;
    private float MaxY;
    private float MinY;
    private Vector3 mobile;
    #endregion
    private void Awake()
    {
        bossTrans = GetComponent<Transform>();
        bossAudio = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        boss = GetComponent<Collider2D>();
        bullet.GetComponent<Transform>();

        HP = 10;
        InvokeRepeating("Fire", 0f, repeatRate);

        MaxX = PlayXY.MaxX;
        MinX = PlayXY.MinX;
        MaxY = PlayXY.MaxY;
        MinY = PlayXY.MinY;
        //Vector3 rightUp = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0.95f, 0));
        //Vector3 leftDown = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0.05f, 0));
        //MaxX = rightUp.x;
        //MinX = leftDown.x;
        //MaxY = rightUp.y;
        //MinY = leftDown.y;
        mobile = Vector3.left;
    }

    private void Update()
    {
        //bossTrans.Translate(Vector3.down * Time.deltaTime * speed);

        if (transform.position.x > MaxX)
        {
            mobile = Vector3.left;
        }
        else if (transform.position.x < MinX)
        {
            mobile = Vector3.right;
        }
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            HP--;
            print("Boss.HP:" + HP);
            if (HP == 0)
            {
                boss.enabled = false;
                bossAudio.Play();
                rend.enabled = false;
                Destroy(this.gameObject, bossAudio.clip.length);
            }
        }
    }

    private void Move()
    {
        transform.Translate(mobile * Time.deltaTime * speed);
    }

    private void Fire()
    {
        Instantiate(bullet, this.transform.position + new Vector3(0, -1f, 0), Quaternion.identity);
    }
}
