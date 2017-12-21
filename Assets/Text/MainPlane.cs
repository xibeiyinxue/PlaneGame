using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainPlane : MonoBehaviour , IHealth
{
    #region Prefabricated
    private float speed = 5;
    private Transform planeTrans;
    [SerializeField]
    private GameObject bullet;

    private int HP;
    private AudioSource dle;
    private Renderer plane;
    private Collider2D player;

    [SerializeField]
    private GameObject Box;

    private float MaxX;
    private float MinX;
    private float MaxY;
    private float MinY;

    public int Health { get; private set; }

    #endregion
    private void Awake()
    {
        planeTrans = GetComponent<Transform>();
        bullet.GetComponents<Transform>();

        HP = 3;
        dle = GetComponent<AudioSource>();
        plane = GetComponent<Renderer>();
        player = GetComponent<Collider2D>();

        MaxX = PlayXY.MaxX;
        MinX = PlayXY.MinX;
        MaxY = PlayXY.MaxY;
        MinY = PlayXY.MinY;
    }

    private void Update()
    {
        planeTrans.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bulletOne();
        }
        planeTrans.position = new Vector3(Mathf.Clamp(planeTrans.position.x, MinX, MaxX),
                                          Mathf.Clamp(planeTrans.position.y, MinY, MaxY),
                                          planeTrans.position.z);
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Boss" || player.tag == "BossBullet")
        {
            HP--;
            print("HP:" + HP);
            if (HP == 0)
            {
                player.enabled = false;
                dle.Play();
                plane.enabled = false;
                Destroy(this.gameObject, dle.clip.length);
            }
        }
    }
    private void bulletOne()
    {
        Instantiate(bullet, planeTrans.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
    }

    public void Damage(int val)
    {
        Health -= val;
        print("MainAirPlane" + Health);
    }
    #region laji
    //private void OnCollosonEnter2D(Collider2D player)
    //{
    //    if (player.tag == "Boss")
    //    {
    //        player.enabled = false;
    //        dle.Play();
    //        plane.enabled = false;
    //        Destroy(this.gameObject, dle.clip.length);
    //    }
    //}
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print("OnCollisionEnter2D");
    //    if (collision.gameObject.tag == "Gold")
    //    {
    //        print("OnCollisionEnter2D2");
    //        Destroy(collision.gameObject);
    //    }
    //}
    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    print("OnCollisionEnter2D");
    //    if (coll.gameObject.tag == "Gold")
    //    {
    //        print("OnCollisionEnter2D2");
    //        Destroy(coll.gameObject);
    //    }
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    print("OnTriggerEnter2D");
    //}
    #endregion
}
