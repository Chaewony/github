using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackTime; //공격 에니메이션 실행 시간

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Attack()//리턴값있어야함
    {
        //Animation.setbool(Attack.true);
        //yield return new WaitForSeconds(망치 찍기 전까지 걸리는 시간);

        //여기 실행
        yield return new WaitForSeconds(attackTime);//공격 애니메이션 진행시간동안 중지
        
        //Animation.setbool(Attack.false);
    }
}
