﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed;

    float horizontalMove = 0f;
    bool jump = false;
    public CharacterController2D controller;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
   
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) &&
            (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))){
            controller.Dash(true);
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            controller.Dash();
        }
    }

    private void FixedUpdate()
    {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
        
    }
}
