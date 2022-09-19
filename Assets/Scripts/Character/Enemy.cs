using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterStats {

    CharacterStats focus;

    // Update is called once per frame
    void Update() {
        CheckAlly();
    }

    void CheckAlly() {
        // check if ally is in attack range in front
        if (focus != null) {
            Attack();
        } else {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector3.right, AttackRange, LayerMask.GetMask("GameInteractable"));
            if (hits != null) {
                foreach (RaycastHit2D hit in hits) {
                    if (hit.transform.tag == "Ally") {
                        focus = hit.transform.GetComponentInParent<CharacterStats>();
                        return;
                    }
                }
                Move();
            }
        }
    }

    void Move() {
        transform.position += Speed * Time.deltaTime * -Vector3.right;
    }

    void Attack() {
        if (Time.time >=  lastAttack + AttackSpeed) {
            lastAttack = Time.time;
            focus.TakeDamage(Damage);
            if (focus.gameObject == null)
                focus = null;
        }        
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
