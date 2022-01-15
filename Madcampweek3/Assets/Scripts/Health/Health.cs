using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;
    private Animator anim;

    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage) {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0) {
            anim.SetTrigger("hurt");
        }
        else {
            if(!dead) {

                //Player
                if(GetComponent<PlayerMove>() != null)
                    GetComponent<PlayerMove>().enabled = false;
                
                //Enemy
                if(GetComponent<EnemyMove>() != null)
                    GetComponent<EnemyMove>().enabled = false;
                
                dead = true;
            }
        }
    }

    public void AddHealth(float _value) {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
