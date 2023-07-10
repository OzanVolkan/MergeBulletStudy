using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[CreateAssetMenu(fileName ="GameData",menuName ="Data/GameData", order =1)]
public class GameData : ScriptableObject
{
    public int money;

    [Button]
    public void SetDefault()
    {
        money = 160;
    }
}
