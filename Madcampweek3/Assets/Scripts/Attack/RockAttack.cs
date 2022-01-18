using UnityEngine;

public class RockAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform rockPoint;
    [SerializeField] private GameObject[] rocks;
    private Animator anim;

    private PlayerMove playerMove;
    private float cooldownTimer = Mathf.Infinity;
    public bool isAttack2 = false;
    [SerializeField] AudioClip rockSound;
    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && cooldownTimer > attackCooldown && playerMove.canAttack() && playerMove.skill == 2 && playerMove.num_skill > 0)
        {
            isAttack2 = true;
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(rockSound);
        playerMove.num_skill--;
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