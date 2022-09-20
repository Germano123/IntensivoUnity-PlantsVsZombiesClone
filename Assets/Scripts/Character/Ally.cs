using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : CharacterStats {

    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update() {
        CheckEnemy();
    }

    void CheckEnemy() {
        // check if there's any enemy in front
        // TODO: check if object 
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, AttackRange, LayerMask.GetMask("GameInteractable"));
        if (hits.Length > 0) {
            foreach (RaycastHit2D hit in hits) {
                // if hit is an enemy shoot it
                if (hit.collider.tag == "Enemy") {
                    // check last shoot
                    if (Time.time >=  lastAttack + AttackSpeed) {
                        lastAttack = Time.time;
                        Shoot();
                        break;
                    }
                }
            }
        }
    }

    void Shoot() {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // Debug.Log($"Plant {transform.name} Shoot.");
    }

    protected override void Die() {
        base.Die();
        GridManager gridManager = FindObjectOfType<GridManager>();
        Vector2Int pos = gridManager.GetGridCoordinate(transform.position.x, transform.position.y);
        gridManager.RemoveAllyAt(pos.x, pos.y);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * AttackRange);
    }
}
