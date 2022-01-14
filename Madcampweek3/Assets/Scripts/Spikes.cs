using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggrtEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
