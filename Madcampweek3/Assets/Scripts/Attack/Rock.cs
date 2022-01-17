using UnityEngine;

public class Rock : MonoBehaviour
{
    private float lifetime;
    private bool canDamage;

    private BoxCollider2D boxCollider;

    Rigidbody2D rigid;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        canDamage = true;
    }
    private void Update()
    {
        if (lifetime > 0.2f) canDamage = false;
        else lifetime += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && canDamage) {
            collision.GetComponent<Health>().TakeDamage(1);
            if(collision.GetComponent<Health>().currentHealth == 0) {
                //Sprite Alpha
                collision.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.4f);
                //Sprite Flip Y
                collision.GetComponent<SpriteRenderer>().flipY = true;
                //Collider Disable
                collision.GetComponent<CapsuleCollider2D>().enabled = false;
                //Die Effect Jump
                collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            }
        }
    }
    public void RockActivate()
    {
        lifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
        Invoke("RockTrigger", 0.2f);
    }

    private void RockTrigger()
    {
        boxCollider.isTrigger =  false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}