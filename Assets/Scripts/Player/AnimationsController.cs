using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private Animator anim;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
#region  PLayer
    //Movimentação
    public void MoveOn() => anim.SetBool("isWalking", true);
    public void MoveOff() => anim.SetBool("isWalking", false);
    public void RunOn() => anim.SetBool("isRun", true);
    public void RunOff() => anim.SetBool("isRun", false);

    //Ataques
    public void AttackOn() => anim.SetTrigger("Attack01");

    //Defesa
    public void DefendOn() => anim.SetTrigger("Defend");

#endregion

#region Enemy
    public void AttackEnemyOn() => anim.SetBool("isAttack", true);

#endregion
}
