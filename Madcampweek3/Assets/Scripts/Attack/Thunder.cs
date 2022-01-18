using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private float duration;
    private float lifetime;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > duration) {
            boxCollider.enabled = false;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "Boss") {
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
    public void ThunderActivate()
    {
        lifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}