using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Wave = 1;
    public int Map = 3;
    public int PlayerHP = 100;
    public int Gold = 10;
    public int EnemyBudget = 10;

    public PlayerController _playerController;
    public MapController _mapController;
    public EnemyController _enemyController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mapController.mapSize = Map;
        _mapController.GenerateMap();
        _enemyController.StartSpawning(EnemyBudget);

        _enemyController.OnEmptyList += WaveCompleted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void WaveCompleted()
    {
        Wave++;
        EnemyBudget = Wave * Map;
        _enemyController.StartSpawning(EnemyBudget);

        if (Wave == 10)
        {
            MapComplete();
            Wave = 1;
        }
    }

    private void MapComplete()
    {
        Map++;
        _mapController.mapSize = Map;
        _mapController.GenerateMap();
    }



}
