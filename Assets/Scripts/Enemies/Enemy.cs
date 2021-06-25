using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Referência ao jogador
    private Transform player;

    // Rigidbody do enemy
    private Rigidbody2D rb;

    // Referência ao animator do objeto(enemy)
    private Animator anim;

    // Variável que irá armazenar a distância entre o enemy e o player
    private Vector3 playerDistance;

    // Variável que representa a velocidade do inimigo
    public float speed;

    private bool facingRight = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = player.transform.position - transform.position;
        if (Mathf.Abs(playerDistance.x) < 12 && Mathf.Abs(playerDistance.y) < 3)
        {
            rb.velocity = new Vector2(speed * (playerDistance.x / Mathf.Abs(playerDistance.x)), rb.velocity.y);
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; 
    }
}

















