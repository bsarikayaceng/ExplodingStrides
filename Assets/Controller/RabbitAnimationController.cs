using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimationController : MonoBehaviour
{
    [SerializeField] TestTouch testTouch;
    [SerializeField] Animator animator;


    public void PlayRunAnination()
    {
        animator.SetTrigger("Run");
    }



    

}
