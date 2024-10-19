using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoHorizontal : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private Rigidbody2D rb;       // Referencia al Rigidbody2D del personaje
    private Vector2 movimiento;   // Vector de movimiento
    public Boolean canWalk = true;

    void Start()
    {
        // Obtener el SpriteRenderer del objeto
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public float velocidad = 20;  // Velocidad del movimiento

    void Update()
    {
        // Detectar flecha izquierda
        if(canWalk){
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetBool("Walking",true);
                spriteRenderer.flipX = true;
                movimiento = new Vector2(-1 * velocidad, rb.velocity.y);
            }
            else if(Input.GetKey(KeyCode.RightArrow)){
                animator.SetBool("Walking",true);
                spriteRenderer.flipX = false;
                movimiento = new Vector2(1 * velocidad, rb.velocity.y);
            }
            else{
                movimiento = new Vector2(0, rb.velocity.y);
                animator.SetBool("Walking",false);
            }
        }
    }

   void FixedUpdate()
    {
        // Aplicar el movimiento usando el Rigidbody2D
        rb.velocity = movimiento;
    }


}