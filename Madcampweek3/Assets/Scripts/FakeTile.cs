using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTile : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    void Awake()
    {   
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1,1,1,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void ColorActivate(){
        spriteRenderer.color = new Color(1,1,1,1);
    }
}
