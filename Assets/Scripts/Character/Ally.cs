using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : CharacterStats {

    public GameObject bulletPrefab;
    float lastShot = 0f;

    // Update is called once per frame
    void Update() {
        CheckEnemy();
    }

    void CheckEnemy() {
        // check if there's any enemy in front
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, AttackRange, LayerMask.GetMask("GameInteractable"));
        if (hit) {
            if (hit.collider.tag == "Enemy") {
                // check last shoot
                if (Time.time >=  lastShot + AttackSpeed) {
                    lastShot = Time.time;
                    Shoot();
                }
            }
        }
    }

    void Shoot() {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // Debug.Log($"Plant {transform.name} Shoot.");
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * AttackRange);
    }
}
