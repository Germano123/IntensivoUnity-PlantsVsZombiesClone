using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeadquartersInterface : MonoBehaviour {

    [SerializeField] Headquarters hq;

    [SerializeField] TextMeshProUGUI hqHealth;

    // Start is called before the first frame update
    void Start() {
        hq.onDied += OnDieHq;
        hq.onDamageTaken += OnDamageTaken;
        hqHealth.text = hq.Health.ToString();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnDieHq(GameObject go) {
        FindObjectOfType<GameManager>().EndGame();
    }

    void OnDamageTaken(int amount) {
        hqHealth.text = hq.Health.ToString();
    }

    void OnDestroy() {
        hq.onDied -= OnDieHq;
    }
}
