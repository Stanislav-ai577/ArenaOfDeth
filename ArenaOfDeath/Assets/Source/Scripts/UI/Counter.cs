using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SaveService _saveService;
    public int EnemyCount { get; private set; }

    private void Update()
    {
        UpdateKillCount();
    }
    
    public void SaveCount()
    {
        _saveService.SaveCount(this);
    }

    public void LoadCount()
    {
        PlayerData data = _saveService.LoadCount();

        EnemyCount = data.Count;
    }
    
    public void EnemyKillCount(int enemyCount)
    {
        EnemyCount += enemyCount;
        UpdateKillCount();
    }

    private void UpdateKillCount()
    {
        _text.text = EnemyCount.ToString();
    }
    
}
