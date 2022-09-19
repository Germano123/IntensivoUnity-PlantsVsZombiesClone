using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    [SerializeField] string coinName;
    public string CoinName { get { return coinName; } }

    WaveController waveController;

    public int CoinAmount { get; private set; }
    // the amount of coins to recieve
    [SerializeField] int coinsPerSecond;
    // the delay time to recieve coins
    [SerializeField] float delayTime;

    #region Callbacks
    public delegate void OnCoinUpdate(int amount);
    public OnCoinUpdate onCoinUpdate;
    #endregion

    IEnumerator coinCounter;

    // Start is called before the first frame update
    void Start() {
        waveController = FindObjectOfType<WaveController>();
        waveController.onSpawEnemy += OnSpawnEnemy;
        waveController.onWaveStatusUpdate += OnWaveStatusUpdate;
        AddCoins(5);
        coinCounter = CoinCounter();
    }

    public void AddCoins(int amount) {
        CoinAmount += amount;
        onCoinUpdate?.Invoke(CoinAmount);
    }

    public bool RemoveCoins(int amount) {
        if (CoinAmount - amount >= 0) {
            CoinAmount -= amount;
            onCoinUpdate?.Invoke(CoinAmount);
            return true;
        } else return false;
    }

    IEnumerator CoinCounter() {
        while (true) {
            yield return new WaitForSeconds(delayTime);
            AddCoins(coinsPerSecond);
        }
    }

    void OnSpawnEnemy(GameObject enemyGO) {
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.onDied += OnEnemyDied;
    }

    void OnEnemyDied(GameObject enemyGO) {
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        AddCoins(enemy.CoinValue);
        enemy.onDied -= OnEnemyDied;
    }

    void OnWaveStatusUpdate(EWaveStatus waveStatus) {
        if (waveStatus == EWaveStatus.Releasing) {
            StartCoroutine(coinCounter);
        } else
        // comment next line if u want to stop coin recieving after the wave is released
        // if (waveStatus == EWaveStatus.Waiting)
        {
            StopCoroutine(coinCounter);
        }
    }

    void OnDestroy() {
        waveController.onSpawEnemy -= OnSpawnEnemy;
        waveController.onWaveStatusUpdate -= OnWaveStatusUpdate;
        StopAllCoroutines();
    }
}
