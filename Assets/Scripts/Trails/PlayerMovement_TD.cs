using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_TD : MonoBehaviour
{
    //Variabler
    [SerializeField] float moveSpeed = 0.01f;
    [SerializeField] Camera cam;

    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 mousePos;
    Vector2 smoothedMoveInput;
    Vector2 moveInputSmooth;

    float angle = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        //H�r h�mtar vi muspekarens Screenposition och omvandlar den till worldposition f�r att enklare veta vart musen �r i f�rh�llande till spelaren
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position; //Vector matte f�r att ta reda p� riktningen spelaren ska rotera mot
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; //H�r anv�nder vi Atan2 f�r att f� fram riktningens radian och d�refter omvandla den till grader
    }

    void OnMove(InputValue value) //Om spelaren trycker p� WASD
    {
        moveInput = value.Get<Vector2>(); //Lagra v�rdet i en vector2 f�r att veta vilket h�ll spelaren ska g� �t
    }
    void FixedUpdate()
    {
        //Smoothed movement
        smoothedMoveInput = Vector2.SmoothDamp(smoothedMoveInput, moveInput, ref moveInputSmooth, 0.01f);
        rb.velocity = smoothedMoveInput * moveSpeed; //F�rflytta spelaren med hj�lp av dess rigidbody

        //Roteration
        Vector2 lookDir = mousePos - rb.position; //Vector matte f�r att ta reda p� riktningen spelaren ska rotera mot
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; //H�r anv�nder vi Atan2 f�r att f� fram riktningens radian och d�refter omvandla den till grader
        rb.rotation = angle; //Rotera spelarens rigidbody med hj�lp av graderna vi f�tt fram
    }
}
