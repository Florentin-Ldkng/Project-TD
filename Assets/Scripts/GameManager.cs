using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int Wave = 0;
    public int MaxWave;
    public int PlayerHP;
    public int Gold;

    public PlayerController _playerController;
    public EnemyController _enemyController;

    public int MapSize = 5;
    public Level CurrentLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyController.OnEmptyList += WaveCompleted;

        MaxWave = CurrentLevel.Waves.Count - 1;
        PlayerHP = CurrentLevel.PlayerHP;
        Gold = CurrentLevel.Gold;

        _enemyController.Waves = CurrentLevel.Waves;

        _enemyController.StartSpawning(Wave);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void WaveCompleted()
    {
        if (Wave < MaxWave)
        {
            Wave++;
            _enemyController.StartSpawning(Wave);
        }
        else
        {
            MapComplete();
        }
    }

    private void MapComplete()
    {
        Debug.Log("MapComplete");
    }

}

