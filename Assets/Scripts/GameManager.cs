using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Wave = 1;
    public int PlayerHP = 100;
    public int Gold = 10;


    public int MapSize = 5;


    public PlayerController _playerController;
    public MapController _mapController;
    public EnemyController _enemyController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_enemyController.OnEmptyList += WaveCompleted;
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
