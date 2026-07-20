using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Wave = 1;
    public int Map = 3;
    public int PlayerHP = 100;
    public int Gold = 10;
    public int EnemyBudget;

    public PlayerController _playerController;
    public MapController _mapController;
    public EnemyController _enemyController;
    public UIController _uiController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyBudget = Wave * Map;
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

        Gold += 5;

        EnemyBudget = Wave * Map;

        _enemyController.StartSpawning(EnemyBudget);
        
        if (Wave == 10)
        {
            MapComplete();
            Wave = 0;
        }

        Wave++;
        // Magicnumber aids - Wave needs + 1, map needs -2 because it is used for the generation and starts on 3
        _uiController.ChangeMapState(Wave, Map - 2);
    }

    private void MapComplete()
    {
        Map++;
        _mapController.mapSize = Map;
        _mapController.GenerateMap();
    }



}
