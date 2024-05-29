using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion1 : StateMachineBehaviour
{
    [HideInInspector]public Player1 Player1;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    anim=GetComponent<Animator>();
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Player1.Health > 0)
        {

            float Horizontal = Input.GetAxis("Horizontal");


            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Horizontal *= 2;
            }

            if (Horizontal > 0)
            {
                Player1.transform.rotation = Quaternion.identity;
            }
            if (Horizontal < 0)
            {
                Player1.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            Horizontal = Mathf.Abs(Horizontal);
            Player1.transform.Translate(Horizontal * Player1.speed * Time.deltaTime, 0, 0);
            animator.SetFloat("Velocity", Horizontal);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump");
                Player1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Horizontal, Player1.jumpSpeed), ForceMode2D.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                int RandomAttack = Random.Range(1, 4);
                string Attack = "Attack" + RandomAttack.ToString();

                animator.SetTrigger("Attack2");
                animator.SetTrigger(Attack);
            }
        }
        else
        {
            animator.SetBool("Die",true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
