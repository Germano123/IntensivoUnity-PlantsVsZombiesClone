using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyInterfaceManager : MonoBehaviour {

    [SerializeField] GameObject allySlotUIPrefab;
    LevelController levelController;

    // Start is called before the first frame update
    void Start() {
        LoadShop();
    }

    void LoadShop() {
        AlliesManager allyManager = FindObjectOfType<AlliesManager>();
        List<CharacterData> alliesData = allyManager.GetAllies();
        if (alliesData == null)
            Debug.Log($"Allies data is null.");
        foreach (CharacterData charData in alliesData) {
            GameObject allySlotUI = Instantiate(allySlotUIPrefab, transform);
            allySlotUI.GetComponent<Image>().sprite = charData.spr;
            allySlotUI.GetComponent<Button>().onClick.AddListener(delegate {
                FindObjectOfType<AllyPlacerController>().SetPreview(charData);
            });
        }
    }
}
