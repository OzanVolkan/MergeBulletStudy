using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
[CreateAssetMenu(fileName ="GameData",menuName ="Data/GameData", order =1)]
public class GameData : ScriptableObject
{
    public List<Tuple<int, int>> bulletsIndexes;
    public Vector3 highScore;
    public int levelIndex;
    public int money;
    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    [Button]
    public void SetDefault()
    {
        levelIndex = 1;
        Money = 160;
        highScore = new Vector3(0, 0, 45f);
        bulletsIndexes.Clear();
    }
}
