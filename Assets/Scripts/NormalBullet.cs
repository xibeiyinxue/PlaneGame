using System.Collections;
using System.Collections.Generic;
using UnityEngine;
                                                             
                                                             
public class NormalBullet : BulletBase
{                                                            
    protected override void Move()                           
    {                                                        
        trans.Translate(Vector3.up * Time.deltaTime * speed);
    }                                                            
}                                                            
                                                             