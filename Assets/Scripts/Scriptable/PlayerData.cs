using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//上方菜单栏创建
[CreateAssetMenu(fileName = "PlayerData",menuName = "CreateScriptable/PlayerData",order = 1)]
public class PlayerData : ScriptableObject
{
    public int maxScore;
}
