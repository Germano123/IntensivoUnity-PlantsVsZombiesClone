using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable/Wave", order = 0)]
public class WaveData : ScriptableObject {
    public WaveEnemyCount[] waves;
}

[System.Serializable]
public class WaveEnemyCount {
    public CharacterData enemyData;
    public int amount;
}