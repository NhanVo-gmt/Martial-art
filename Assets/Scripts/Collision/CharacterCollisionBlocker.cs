using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionBlocker : MonoBehaviour
{
    [SerializeField] Collider2D characterCollision;
    [SerializeField] Collider2D characterCollisionBlocker;

    private void Start() {
        Physics2D.IgnoreCollision(characterCollision, characterCollisionBlocker);
    }
}
