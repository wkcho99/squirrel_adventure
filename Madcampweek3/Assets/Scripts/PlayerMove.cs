using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public float maxSpeed;
    public float jumPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsuleCollider;
    Health health;
    [SerializeField] private LayerMask groundLayer;
    private float wallJumpCooldown;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        health = GetComponent<Health>();
    }

    private void Update() {
        //Jump
        if(wallJumpCooldown > 0.2f) {
            /*if(onWall() && !isGrounded()) {
                rigid.gravityScale = 0;
                VelocityZero();
            } else {
                rigid.gravityScale = 4;
            }*/

            if(Input.GetButtonDown("Jump"))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;

        //Stop speed when no input
         if(Input.GetButtonUp("Horizontal")){
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
         }

        //Direction change
        if(Input.GetButton("Horizontal")){
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
        }

        //walk animation setting
        if(Mathf.Abs(rigid.velocity.x)< 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

    }

    private void Jump() {
        if(isGrounded()) {
            rigid.AddForce(Vector2.up * jumPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        else if(onWall() && !isGrounded()) {
            wallJumpCooldown = 0;
            rigid.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*20, 20);
            transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wallJumpCooldown > 0.2f) {
            //Move by Control
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h * 4, ForceMode2D.Impulse);
        }

        //Maxspeed control
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //Landing Platform
        if(rigid.velocity.y<0){
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
            if(isGrounded()){
                anim.SetBool("isJumping", false);
            }
        }

    }
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy"){
            //Attack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y){
                OnAttack(collision.transform);
            }else{
                health.TakeDamage(2);
                OnDamaged(collision.transform.position);
            }
        }
        if(collision.gameObject.tag == "Spike"){
            health.TakeDamage(1);
            OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Item"){
            //Point Earn
            bool isBronze =  collision.gameObject.name.Contains("Bronze");
            bool isSilver =  collision.gameObject.name.Contains("Silver");
            bool isGold =  collision.gameObject.name.Contains("Gold");
            if(isBronze)
                gameManager.stagePoint += 50;
            else if(isSilver)
                gameManager.stagePoint += 100;
            else if(isGold)
                gameManager.stagePoint += 300;
            //Deactivate Item
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "Finish"){
            //Finish -> to Next Stage
            gameManager.NextStage();
        }
    }

    void OnDamaged(Vector2 targetPos){
        //Change Layer
        gameObject.layer = 11;
        //View Alpha 피격시
        spriteRenderer.color = new Color(1,1,1,0.4f);
        //Reaction Force
        int direction = transform.position.x-targetPos.x > 0 ? 1: -1;
        rigid.AddForce(new Vector2(direction,1)*7, ForceMode2D.Impulse);

        Invoke("OffDamaged", 1.5f);
    }

    void OffDamaged(){
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1,1,1,1);
    }

    void OnAttack(Transform enemy){
        //Point
        gameManager.stagePoint += 100;
        //Reaction Force
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //Enemy die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    public void VelocityZero(){
        rigid.velocity = Vector2.zero;
    }

    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}