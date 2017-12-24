﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup gameOverGroup;
    private LevelDirector director;

    private void Start()
    {
        director = LevelDirector.Instance;
        director.GameOverAction += DisplayText;
        gameOverGroup.alpha = 0;
    }

    private void DisplayText()
    {
        gameOverGroup.alpha = 1;
    }
}
