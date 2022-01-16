using UnityEngine;

public class RockAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform rockPoint;
    [SerializeField] private GameObject[] rocks;
    private Animator anim;

    private PlayerMove playerMove;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMove.canAttack() && playerMove.skill == 2)
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("rock");
        cooldownTimer = 0;

        rocks[FindRock()].transform.position = rockPoint.position;
        rocks[FindRock()].GetComponent<Rock>().RockActivate();
    }
    private int FindRock()
    {
        for (int i = 0; i < rocks.Length; i++)
        {
            if (!rocks[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}