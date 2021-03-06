﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Gold : MonoBehaviour
{
    private AudioSource gameAudio;
    private Renderer rend;
    private Collider2D coll;

    private void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent("MainPlane"))
        {
            coll.enabled = false;
            gameAudio.Play();
            rend.enabled = false;
            LevelDirector.Instance.Score += 10;
            Destroy(this.gameObject, gameAudio.clip.length);
        }
    }

}

