using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

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
        

    }

}
