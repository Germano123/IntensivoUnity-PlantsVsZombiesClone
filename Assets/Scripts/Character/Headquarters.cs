using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headquarters : CharacterStats {

    [SerializeField] CharacterData hqData;

    #region Callbacks
    public delegate void OnDamageTaken(int amount);
    public OnDamageTaken onDamageTaken;
    #endregion

    // Start is called before the first frame update
    void Start() {
        SetStats(hqData);
        onDamageTaken?.Invoke(0);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public override void TakeDamage(int amount) {
        base.TakeDamage(amount);
        onDamageTaken?.Invoke(amount);
    }
}
