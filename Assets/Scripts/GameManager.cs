using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Wave = 1;
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
        //_enemyController.OnEmptyList += WaveCompleted;

        MaxWave = CurrentLevel.Waves.Count;
        PlayerHP = CurrentLevel.PlayerHP;
        Gold = CurrentLevel.Gold;

        _enemyController.Waves = CurrentLevel.Waves;

        _enemyController.StartSpawning(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void WaveCompleted()
    {
    }

    private void MapComplete()
    {
    }

}
