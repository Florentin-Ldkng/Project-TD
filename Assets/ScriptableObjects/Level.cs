using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public string Name;
    public short LevelID;
    public int PlayerHP;
    public List<Wave> Waves;
    public int Gold;
}
