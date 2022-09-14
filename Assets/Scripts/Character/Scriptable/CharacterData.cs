using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable/Characters/CharacterData", order = 0)]
public class CharacterData : ScriptableObject {
    public Sprite spr;
    public int health;
    public int damage;
    public float speed;
    public float attackRange;
    public float attackSpeed;
    public int coinValue;
}