using DG.Tweening;
using UnityEngine;

public class CharController : MonoBehaviour
{
    RabbitAnimationController RabbitAnimationControlle;
    public Vector3 targetPosition;
    public float moveSpeed=0.01f;
    public Transform rabbitTransform;
    private void Start()
    {
        Invoke("TouchMove",15);
        rabbitTransform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
    }


    public void TouchMove(Vector3 newPos)
    {
        transform.LookAt(newPos);
        transform.DOComplete();
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

        targetPosition = new Vector3(newPos.x, -2, newPos.z);

       // Vector3 newPos = new Vector3(x, y, z); // Yeni pozisyonunuzu belirleyin
        Quaternion newRotation = Quaternion.Euler(0, 270, 0); // Yeni rotasyonu oluþturun

        // Pozisyonu ve rotasyonu ayarlayýn
        // transform.SetPositionAndRotation(newPos, newRotation);

        //RabbitAnimationControlle.PlayRunAnimation();
        transform.DOMoveY(2,moveSpeed*3/4).OnComplete(()=>  transform.DOMoveY(-2, moveSpeed / 4));
        transform.DOMoveX(targetPosition.x,moveSpeed);
        transform.DOMoveZ(targetPosition.z,moveSpeed);
        //RabbitAnimationController.PlayRunAnimation();

    }

}
