using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinInterfaceManager : MonoBehaviour {

    CoinManager coinManager;

    [SerializeField] TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start() {
        coinManager = FindObjectOfType<CoinManager>();
        coinManager.onCoinUpdate += OnCoinUpdate;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCoinUpdate(int amount) {
        coinText.text = amount.ToString();
    }

    void OnDestroy() {
        coinManager.onCoinUpdate -= OnCoinUpdate;
    }
}
