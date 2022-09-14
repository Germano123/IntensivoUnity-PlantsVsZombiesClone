using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPlacerController : MonoBehaviour {

    [SerializeField] GameObject allyPrefab;
    [SerializeField] Transform alliesParent;

    CharacterData charData;
    Ally allyPreview;

    GridManager gridManager;
    CoinManager coinManager;

    void Start() {
        GameObject allyGO = Instantiate(allyPrefab, transform);
        allyPreview = allyGO.GetComponent<Ally>();
        allyPreview.enabled = false;
        allyPreview.gameObject.SetActive(false);
        gridManager = FindObjectOfType<GridManager>();
        coinManager = FindObjectOfType<CoinManager>();
    }

    // Update is called once per frame
    void Update() {
        if (charData != null) {
            // update preview position with mouse position
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // clamp position with grid values
            pos = gridManager.GetGridPosition(pos.x, pos.y);
            // Debug.Log($"X and Y Pos are: ({pos.x}, {pos.y})");
            if (pos.x != 0f && pos.y != 0f) {
                allyPreview.gameObject.SetActive(true);
                allyPreview.transform.position = pos;
            } else {
                allyPreview.gameObject.SetActive(false);
            }

            // mouse right click
            if (Input.GetMouseButtonDown(0)) {
                // if click was in grid
                if (pos.x != 0f && pos.y != 0f) {
                    // if can buy current selected ally
                    if (coinManager.RemoveCoins(allyPreview.CoinValue)) {
                        // instantiate new ally in the mouse clamped position in grid
                        GameObject allyGO = Instantiate(allyPrefab, pos, Quaternion.identity);
                        allyGO.transform.SetParent(alliesParent);
                        allyGO.GetComponent<Ally>().SetStats(charData);
                    }
                }
                ResetPreview();
            }

            // mouse left click
            if (Input.GetMouseButtonDown(1)) {
                ResetPreview();
            }
        }
    }

    public void SetPreview(CharacterData charData) {
        this.charData = charData;
        allyPreview.SetStats(charData);
    }

    void ResetPreview() {
        charData = null;
        allyPreview.gameObject.SetActive(false);
    }
}
