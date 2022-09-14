using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveInterfaceManager : MonoBehaviour {

    WaveController waveController;

    [SerializeField] TextMeshProUGUI statusTxt;
    [SerializeField] TextMeshProUGUI enemyCountTxt;

    // Start is called before the first frame update
    void Start() {
        waveController = FindObjectOfType<WaveController>();
        waveController.onEnemiesCountUpdate += UpdateEnemyCountUI;
        waveController.onWaveStatusUpdate += UpdateWaveStatusUI;
    }

    void UpdateEnemyCountUI(int enemiesCount) {
        enemyCountTxt.text = enemiesCount.ToString();
    }

    void UpdateWaveStatusUI(EWaveStatus waveStatus) {
        statusTxt.text = waveStatus.ToString();
    }

    void OnDestroy() {
        waveController.onEnemiesCountUpdate -= UpdateEnemyCountUI;
        waveController.onWaveStatusUpdate -= UpdateWaveStatusUI;
    }
}
