using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesManager : MonoBehaviour {

    [SerializeField] CharacterData[] allies;
    List<CharacterData> alliesList;

    // Awake is called when the object is created
    void Awake() {
        alliesList = new List<CharacterData>();
        foreach (CharacterData charData in allies) {
            alliesList.Add(charData);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public List<CharacterData> GetAllies() {
        return alliesList;
    }
}
