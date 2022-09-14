using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWaveStatus { Waiting, Releasing, End }

public class WaveController : MonoBehaviour {

    [SerializeField] WaveData[] waves;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemiesParent;
    [SerializeField] Transform[] waveSpawns;
    public float timeBetweenSpawn;

    public int EnemiesCount { get; private set; }
    public EWaveStatus WaveStatus { get; private set; }

    void Start() {
        WaveStatus = EWaveStatus.Waiting;
    }

    #region Callbacks
    public delegate void OnEnemiesCountUpdate(int enemiesCount);
    public OnEnemiesCountUpdate onEnemiesCountUpdate;

    public delegate void OnWaveStatusUpdate(EWaveStatus waveStatus);
    public OnWaveStatusUpdate onWaveStatusUpdate;

    public delegate void OnSpawEnemy(GameObject enemyGO);
    public OnSpawEnemy onSpawEnemy;
    #endregion

    public WaveData GetWave(int index) {
        if (index <= waves.Length)
            return waves[index];
        return null;
    }

    void UpdateWaveStatus(EWaveStatus waveStatus) {
        WaveStatus = waveStatus;
        onWaveStatusUpdate?.Invoke(WaveStatus);
    }

    public void ReleaseWave(int waveIndex) {
        UpdateWaveStatus(EWaveStatus.Releasing);
        StartCoroutine(Cor_ReleaseWave(waveIndex));
    }

    IEnumerator Cor_ReleaseWave(int waveIndex) {
        EnemiesCount = 0;
        // Debug.Log($"Releasing wave {waveIndex}...");
        // TODO: randomize enemies spawn
        foreach (WaveEnemyCount enemyWaveCount in waves[waveIndex].waves) {
            Transform randomSpawnPoint;
            int enemiesCount = 0;
            while (enemyWaveCount.amount > enemiesCount) {
                // rand spawn point
                randomSpawnPoint = GetRandomSpawnPoint();
                // TODO: upgrade this to a pool
                // instantiate enemy
                GameObject enemyGO = Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
                enemyGO.transform.SetParent(enemiesParent);
                onSpawEnemy?.Invoke(enemyGO);
                // get enemy component
                Enemy enemy = enemyGO.GetComponent<Enemy>();
                enemy.SetStats(enemyWaveCount.enemyData);
                enemy.onDied += OnEnemyDied;
                // update wave enemies count
                enemiesCount++; EnemiesCount++;
                onEnemiesCountUpdate?.Invoke(EnemiesCount);
                // wait until next enemy spawn
                yield return new WaitForSeconds(timeBetweenSpawn);
            }
        }
        // Debug.Log("Wave released.");
        UpdateWaveStatus(EWaveStatus.End);
    }

    void OnEnemyDied(GameObject enemyGO) {
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.onDied -= OnEnemyDied;
        EnemiesCount--;
        if (EnemiesCount <= 0) {
            UpdateWaveStatus(EWaveStatus.Waiting);
        }
        onEnemiesCountUpdate?.Invoke(EnemiesCount);
    }

    Transform GetRandomSpawnPoint() {
        return waveSpawns[Random.Range(0, waveSpawns.Length)];
    }
}
