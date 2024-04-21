using System.Collections;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Counter _counter;
    [SerializeField] private PauseService _pauseService;
    private Transform _target;
    private Player _player;
    private Enemy _enemy;
    private float _spawnTick = 1f;
    private IEnumerator _coroutine;

    private void Awake()
    {
        _player = Resources.Load<Player>("Player/Player");
        _enemy = Resources.Load<Enemy>("Enemy/Enemy");
    }

    private void OnEnable()
    {
        _spawnTick = 2f;
        _coroutine = SpawnTick();
    }

    private void Start()
    {
        CreatedPlayer();
        StartCoroutine(_coroutine);
    }
    
    private void CreatedPlayer()
    {
        Player player = Instantiate(_player, _playerSpawnPoint.position, Quaternion.identity,_playerSpawnPoint.transform);
        player.GetComponent<PlayerHealth>().Setup(_pauseService);
        _target = player.transform;
    }
    
    private void CreatedEnemy()
    {
        Enemy enemy = Instantiate(_enemy, _enemySpawnPoint.position, Quaternion.identity, _enemySpawnPoint.transform);
        enemy.GetComponent<EnemyMovement>().Setup(_target);
        enemy.GetComponent<EnemyHealth>().Setup(_counter);
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            CreatedEnemy();
            yield return new WaitForSeconds(_spawnTick);
        }
    }
}
