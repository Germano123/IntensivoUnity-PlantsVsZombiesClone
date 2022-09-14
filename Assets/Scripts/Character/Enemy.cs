using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterStats {

    // Update is called once per frame
    void Update() {
        // check if there's an ally in front of
        // attack
        // else
        Move();
    }

    void CheckAlly() {
        // check if ally is in attack range in front
        // Collider2D ally = Physics2D.OverlapCircle(point, radius, LayerMask.GetMask(""))
    }

    void Move() {
        transform.position += Speed * Time.deltaTime * -Vector3.right;
    }

    void Attack() {

    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
