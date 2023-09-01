using UnityEngine;

public class RabbitAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;


    public void PlayRunAnimation()
    {
        animator.SetTrigger("Run");
    }

}
