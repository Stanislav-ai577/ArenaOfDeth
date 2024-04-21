using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Count;

    public PlayerData(Counter counter)
    {
        Count = counter.EnemyCount;
    }
}
