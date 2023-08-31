using UnityEngine;

public class RabbitAnimationController : MonoBehaviour
{
    [SerializeField] TestTouch testTouch;
    [SerializeField] Animator animator;


    public void PlayRunAnimation()
    {
        animator.SetTrigger("Run");
    }



    

}
