using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public Health health;

    CapsuleCollider2D capsuleCollider;
    // Start is called before the first frame update
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        health = GetComponent<Health>();
    }

    public void OnDamaged(){
        if(health.currentHealth <= 0) {
            //Sprite Alpha
            spriteRenderer.color = new Color(1,1,1,0.4f);
            //Sprite Flip Y
            spriteRenderer.flipY = true;
            //Collider Disable
            capsuleCollider.enabled = false;
            //Die Effect Jump
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            return;
        }
        //Sprite Alpha
        spriteRenderer.color = new Color(1,1,1,0.4f);
        //Destroy
        Invoke("DeActive", 3);
    }

    void DeActive(){
        gameObject.SetActive(false);
    }
    
}
