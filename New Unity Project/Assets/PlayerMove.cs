using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;

    //이동관련변수
    [SerializeField]
    private float speed;
    private Vector3 movement;

    [SerializeField]
    private float jumpPower;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //애니메이션 관리
    }

    //////////////////////////////////////////////////////////////////////////////

    // 이동
    public void Move(float h) //h는 호라이젠탈 값
    {
        movement.Set(h, 0, 0);
        movement = movement.normalized * speed * Time.deltaTime; //델타타임: 1초에 어느 방향으로 얼만큼 움직일거냐

        rb.MovePosition(this.transform.position + movement);
    }

    //점프
    public void Jump()
    {
        rb.velocity = Vector2.zero;//점프 전 움직임 멈추기 0벡터
        Vector2 jumpVelocity = new Vector2(0, jumpPower);

        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    //애니메이션 관리
    private void AnimationController()
    {
        if(movement.x != 0)
            // 이동 에니메이션 실행 bool
        else
                //이동 에니메이션 중지

        //만약 점프 실행 중일 경우
        //settrigger 실행
    }
}
