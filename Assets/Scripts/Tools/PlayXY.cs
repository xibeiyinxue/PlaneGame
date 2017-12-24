using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlayXY
{
    public static float MaxX { get { return GetWordPos(new Vector3(1, 1, 0)).x; } }
    public static float MinX { get { return GetWordPos(new Vector3(0, 0, 0)).x; } }
    public static float MaxY { get { return GetWordPos(new Vector3(1, 1, 0)).y; } }
    public static float MinY { get { return GetWordPos(new Vector3(0, 0, 0)).y; } }

    private static Vector3 GetWordPos(Vector3 v)
    {
        return Camera.main.ViewportToWorldPoint(v);
    }

    #region Lv1
    //private static PlayXY instance;
    //public static PlayXY Instance
    //{
    //    get
    //    {
    //        if(instance == null)
    //        {
    //            instance = new PlayXY();
    //        }
    //        return instance;
    //    }
    //}

    //private PlayXY()
    //{
    //    Vector3 rightUp = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0.95f, 0));
    //    Vector3 leftDown = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0.05f, 0));
    //    MaxX = rightUp.x;
    //    MinX = leftDown.x;
    //    MaxY = rightUp.y;
    //    MinY = leftDown.y;
    //}
    #endregion
}
