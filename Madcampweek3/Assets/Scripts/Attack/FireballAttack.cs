using UnityEngine;

public class FireballAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private PlayerMove playerMove;
    private float cooldownTimer = Mathf.Infinity;
    public bool isAttack0 = false;
    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && cooldownTimer > attackCooldown && playerMove.canAttack() && playerMove.skill == 0 && playerMove.num_skill > 0)
        {
            isAttack0 = true;
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        playerMove.num_skill--;
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Fireball>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}