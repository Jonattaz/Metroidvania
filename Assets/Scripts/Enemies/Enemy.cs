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

    // Váriavel que armazena o drop do inimigo
    public GameObject itemDrop;

    public ConsumableItem item;

    // Váriavel que representa a vida do inimigo
    public int health;

    // Referência ao animator do objeto(enemy)
    private Animator anim;

    // Variável que irá armazenar a distância entre o enemy e o player
    private Vector3 playerDistance;

    // Variável que representa a velocidade do inimigo
    public float speed;

    public Vector2 damageForce;


    private bool facingRight = false;

    private bool isDead;

    private SpriteRenderer spriteRend;

    // Variável que representa o dano que o inimigo pode receber
    public int damage;

    public int souls;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            playerDistance = player.transform.position - transform.position;
            if (Mathf.Abs(playerDistance.x) < 12 && Mathf.Abs(playerDistance.y) < 3)
            {
                rb.velocity = new Vector2(speed * (playerDistance.x / Mathf.Abs(playerDistance.x)), rb.velocity.y);
            }

            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            if (rb.velocity.x > 0.5f && !facingRight)
            {
                Flip();
            }
            else if (rb.velocity.x < 0.5f && facingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; 
    }


    // Método responsável pelo dano que o inimigo irá levar
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Dead");
            FindObjectOfType<Player>().souls += souls;
            FindObjectOfType<UIManager>().UpdateUI();            


            if (item != null)
            {
                GameObject tempItem = Instantiate(itemDrop, transform.position, transform.rotation);
                tempItem.GetComponent<ItemDrop>().item = item;
            }

        }
        else
        {

            StartCoroutine(DamageCoroutine());
        }
    }


    // Método que controla a parte estética do dano do inimigo
    IEnumerator DamageCoroutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            spriteRend.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Método que destroy o inimigo 
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
            // DamageForce e newDamageforce possuem por objetivo impedir o jogador de ficar em cima dos inimigos,
            // ação não desejada
            Vector2 newDamageForce = new Vector2(damageForce.x * (playerDistance.x / Mathf.Abs(playerDistance.x)),damageForce.y);
            player.GetComponent<Rigidbody2D>().AddForce(newDamageForce,ForceMode2D.Impulse);
        }

    }







}

















