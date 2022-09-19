using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    GameManager gameManager;
    WaveController waveController;
    CoinManager coinManager;

    public int CurrentLevel { get; private set; }
    [SerializeField] int coinLevelFactor;
    
    #region Callbacks
    public delegate void OnLevelStart(int level);
    public OnLevelStart onLevelStart;
    #endregion

    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        coinManager = FindObjectOfType<CoinManager>();
        waveController = FindObjectOfType<WaveController>();

        gameManager.onEndGame += OnEndGame;
        waveController.onEnemiesCountUpdate += OnEnemiesCountUpdate;
        // TODO: get level from loader
        CurrentLevel = 1;
    }

    void NextLevel() {
        CurrentLevel++;
    }

    public void StartLevel() {
        if (waveController.WaveStatus == EWaveStatus.Waiting) {
            // Debug.Log($"Started Wave {CurrentLevel}");
            onLevelStart?.Invoke(CurrentLevel);
            waveController.ReleaseWave(CurrentLevel-1);
            coinManager.AddCoins(coinLevelFactor * CurrentLevel);
        }
    }

    void OnEnemiesCountUpdate(int enemiesCount) {
        if (enemiesCount <= 0) {
            NextLevel();
        }
    }

    void OnEndGame() {
        CharacterStats[] gamePieces = FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats piece in gamePieces) {
            if (piece.gameObject.activeInHierarchy) {
                piece.enabled = false;
            }
        }
    }

    void OnDestroy() {
        gameManager.onEndGame -= OnEndGame;
        waveController.onEnemiesCountUpdate -= OnEnemiesCountUpdate;    
    }
}
