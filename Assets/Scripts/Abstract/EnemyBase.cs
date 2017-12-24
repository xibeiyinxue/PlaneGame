﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour, IHealth
{
    protected int health = 100;
    public int Health
    {
        get { return health; }
        protected set { health = value; }
    }

    public virtual void Damage(int val) { }
}
