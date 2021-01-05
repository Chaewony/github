using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
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
        //isGround = Physics2D.OverlapCircle(pos.position, checkRadius, Islayer);//(Vector2 point, float radius, int layer)
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, Islayer);//(Vector2 point, float radius, int layer)

        //Stop Speed
        if (Input.GetButton("Horizontal"))//단발적인 키 입력 받기
        { 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);
        }
        //방향 전환
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        //Walking Animation
        if(rigid.velocity.normalized.x==0 || isGround == false)//횡 이동 방향 값이 0일 때, 즉 이동을 멈추면
        {
            anim.SetBool("isWalking", false);
        }

        else 
        {
            anim.SetBool("isWalking", true);
        }

        //점프 애니메이션
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        //점프 관련 입력
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)//첫 점프
        {
            rigid.velocity = Vector2.up * JumpPower;
            isDoubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround == false && isDoubleJump == true)//더블 점프
        {
            rigid.velocity = Vector2.up * JumpPower;
            anim.SetBool("isJumping", true);
            isDoubleJump = false;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //좌우 이동
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(Speed * h, rigid.velocity.y);

        /*if(h>0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(h<0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } 방향전환 각조로 조절했더니 카메라를 Player에 뒀을 때 카메라 각도가 같이 반전됨*/
    }
}
