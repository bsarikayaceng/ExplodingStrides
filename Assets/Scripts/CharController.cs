using DG.Tweening;
using UnityEngine;

public class CharController : MonoBehaviour
{
    RabbitAnimationController RabbitAnimationController;
    //public Vector3  targetPosition = new Vector3;
    public float moveSpeed;
    public GameObject rabbit;


    public void TouchMove(Vector3 newPos)
    {

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if (mousePos.x > 1)
        //{
        //    //move right
        //    transform.Translate(moveSpeed, 0, 0);
        //}
        //else if (mousePos.x < -1)
        //{
        //    //move left
        //    transform.Translate(-moveSpeed, 0, 0);
        //}

        transform.SetPositionAndRotation(newPos,Quaternion.identity);
        RabbitAnimationController.PlayRunAnimation();

        //rabbit.transform.DOMove(targetPosition.position,moveSpeed);

    }

}
