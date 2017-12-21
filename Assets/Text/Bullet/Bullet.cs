using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private Transform bullet;
    [SerializeField]
    private float speed = 5;
    private AudioSource bulletAudio;
    private Renderer see;
    private Collider2D bulletsobj;

    private void Start()
    {
        bullet = GetComponent<Transform>();
        bulletAudio = GetComponent<AudioSource>();
        see = GetComponent<Renderer>();
        bulletsobj = GetComponent<Collider2D>();
        bulletAudio.Play();
    }

    private void Update()
    {
        bullet.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D bullets)
    {
        if (bullets.tag == "Player" || bullets .tag == "Boss")
        {
            bulletsobj.enabled = false;
            see.enabled = false;
            Destroy(gameObject);
        }
        else if (bullets.tag == "Bullet" && bullets.tag == "BossBullet")
        {
            bulletsobj.enabled = false;
            see.enabled = false;
            Destroy(gameObject);
        }
        else if (bullet .tag == "Bullet"  && bullet.tag == "Bullet")
        {

        }
        else if (bullet.tag == "BossBullet" && bullet.tag == "BossBullet")
        {

        }
    }
}
