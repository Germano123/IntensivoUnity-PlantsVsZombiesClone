using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is meant to be used when we create the
// dynamic wave generation
public class EnemiesManager : MonoBehaviour {

    [SerializeField] CharacterData[] enemies;
    List<CharacterData> enemiesList;

    // Start is called before the first frame update
    void Start() {
        enemiesList = new List<CharacterData>();
        foreach (CharacterData charData in enemies) {
            enemiesList.Add(charData);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public List<CharacterData> Getenemies() {
        return enemiesList;
    }
}
