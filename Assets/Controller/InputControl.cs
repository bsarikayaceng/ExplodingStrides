using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControl : MonoBehaviour
{

    private static InputControl instance;

    public static InputControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputControl>();
            }
            return instance;
        }
    }


    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnEndTouch;



    private Rigidbody Rabbitchar;
    public Vector2 moveVal;
    public float moveSpeed=5;

    private MyInputs touchControls;

    private void Awake()
    {
        Rabbitchar = GetComponent<Rigidbody>();
        touchControls = new MyInputs();
    }


    public void Moving(InputAction.CallbackContext value)
    {
        Debug.Log("Pressed"+value);
    }


    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    //private void Start()
    //{
    //    touchControls.Player.TouchPress.started += ctx => StartTouch(ctx);
    //    touchControls.Player.TouchPress.canceled += ctx => EndTouch(ctx);
    //}


    //private void StartTouch(InputAction.CallbackContext context)
    //{
    //    Debug.Log("<color=cyan>Touch Started</color>" + touchControls.Player.TouchPosition.ReadValue<Vector2>() );
    //    if (OnStartTouch != null) OnStartTouch(touchControls.Player.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);


    //}
    
    //private void EndTouch(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Touch ended");
    //    if (OnEndTouch != null) OnEndTouch(touchControls.Player.TouchPosition.ReadValue<Vector2>(), (float)context.time);

    //}
}
