using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth;
    public bool hurt;

    private Animator anim;
    
    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage) {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0) {
            if(anim != null)
                anim.SetTrigger("hurt");
            hurt = true;
        }

    }

    public void AddHealth(float _value) {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
