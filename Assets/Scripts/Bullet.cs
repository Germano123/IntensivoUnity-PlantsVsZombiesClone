using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] int damage;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    // LateUpdate is called once per frame in the end of the frame
    void LateUpdate() {
        CheckCollision();
        CheckRenderer();
    }

    void Move(){
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
    
    void CheckCollision() {
        Vector3 pos = transform.position + Vector3.right * collisionRadius;
        Vector2 center = new Vector2(pos.x, pos.y);
        Collider2D col = Physics2D.OverlapCircle(center, collisionRadius, LayerMask.GetMask("GameInteractable"));
        if (col != null) {
            if (col.tag == "Enemy") {
                col.transform.GetComponentInParent<CharacterStats>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    void CheckRenderer() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x > 1f) {
            Destroy(gameObject);
        }
    }

    [SerializeField] float collisionRadius = 0.125f;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.right * collisionRadius, collisionRadius);
    }
}
