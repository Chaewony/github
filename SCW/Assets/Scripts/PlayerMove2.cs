using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    [SerializeField]
    private float Speed;
    [SerializeField]
    private float JumpPower;
    [SerializeField]
    Transform pos;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask Islayer;

    bool isDoubleJump = false;//더블점프인지 확인
    bool isGround; //땅에 닿았는지 확인

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();//GetComponent메서드를 사용해서 Rigidbody2D 컴포넌트가 가진 데이터에 접근
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, Islayer);//(Vector2 point, float radius, int layer)
        GetInput();
        AnimationController();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(Speed * h, rigid.velocity.y);
    }

    void GetInput()
    {
        //좌우 키 입력
        if (Input.GetButton("Horizontal"))
        {
            //좌우 이동
            /*float h = Input.GetAxisRaw("Horizontal");
            rigid.velocity = new Vector2(Speed * h, rigid.velocity.y);*/

            //멈춤
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);

            //방향전환
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //스페이스 바 입력
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)//첫 점프
        {
            rigid.velocity = Vector2.up * JumpPower;
            isDoubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround == false && isDoubleJump == true)//더블 점프
        {
            rigid.velocity = Vector2.up * JumpPower;
            isDoubleJump = false;
        }
    }

    void AnimationController()
    {
        //Walking 애니메이션
        if (rigid.velocity.normalized.x == 0 || isGround == false)//횡 이동 방향 값이 0일 때, 즉 이동을 멈추면
        {
            anim.SetBool("isWalking", false);
        }

        else
        {
            anim.SetBool("isWalking", true);
        }

        //Jump 애니메이션
        if (Input.GetKey(KeyCode.Space))//일반 점프
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround == false && isDoubleJump == true)//더블 점프
        {
            anim.SetBool("isJumping", true);
        }

        //Attack 애니메이션
        if (Input.GetKey(KeyCode.Z))
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }
}
