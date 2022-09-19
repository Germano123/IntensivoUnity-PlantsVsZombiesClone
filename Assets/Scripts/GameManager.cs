using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool LevelStart { get; private set; }
    LevelController levelController;

    #region Callbacks
    public delegate void OnEndGame();
    public OnEndGame onEndGame;
    #endregion

    // Start is called before the first frame update
    void Start() {
        levelController = FindObjectOfType<LevelController>();
        LevelStart = false;
    }

    // Update is called once per frame
    void Update() {
        if (!LevelStart && Input.GetKeyDown(KeyCode.Space)) {
            StartLevel();
        }
    }

    public void StartLevel() {
        LevelStart = true;
        levelController.StartLevel();
    }

    public void EndGame() {
        onEndGame?.Invoke();
    }
}
