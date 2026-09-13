using UnityEngine;

public class TowerGeneric : MonoBehaviour
{
    public event System.Action OnLevelUp;
    public Tower[] TowerBaseStats = new Tower[5];
    public GameObject[] TowerLevels = new GameObject[5];
    public int CurrentLevel = 0;
    public int XP = 0;
    public int XPThreshhold;    
    public ParticleSystem Levelup;
    public int Range;
    public int Damage;
    public float ShootingDelay;
    public bool Multitarget;
    public bool UsesRange;
    public int MaxTargets;
    public int DamageAllocation;
    public GameObject DetectionRange;
    public TowerDetection towerDetection;
    public void EarnXP(int earnedXP)
    {
        XP += earnedXP;
        if (XP >= XPThreshhold && CurrentLevel < 4)
        {
            XP = XP - XPThreshhold;
            CurrentLevel++;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Levelup.Play();
        OnLevelUp.Invoke();
        EnableTower();
        UpdateRange();
    }
    public void UpdateRange()
    {
        DetectionRange.transform.localScale = new Vector3(Range, Range, Range);
    }
    public void LoadStats()
    {
        Range = TowerBaseStats[CurrentLevel].BaseRange;
        Damage = TowerBaseStats[CurrentLevel].BaseDamage;
        ShootingDelay = TowerBaseStats[CurrentLevel].BaseShootingDelay;
        Multitarget = TowerBaseStats[CurrentLevel].Multitarget;
        UsesRange = TowerBaseStats[CurrentLevel].UsesRange;
        MaxTargets = TowerBaseStats[CurrentLevel].MaxTargets;
        DamageAllocation = TowerBaseStats[CurrentLevel].DamageAllocation;
        XPThreshhold = TowerBaseStats[CurrentLevel].XPThreshhold;
    }

    public void EnableTower()
    {
        TowerLevels[Mathf.Clamp(CurrentLevel - 1, 0, 4)].SetActive(false);
        TowerLevels[CurrentLevel].SetActive(true);
    }
}
