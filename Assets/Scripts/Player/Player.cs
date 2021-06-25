using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Referência o rigidbody do jogador
    private Rigidbody2D rb;

    // Velocidade máxima possível
    public float maxSpeed;

    // Velocidade atual
    private float speed;

    private bool facingRight = true;

    private bool onGround;
    public Transform groundCheck;

    private bool jump = false;
    private bool doubleJump;
    public float jumpForce;

    // Variável do tipo weapon
    private Weapon weaponEquipped;

    // Referência ao animator do jogador
    private Animator animPlayer;

    private Attack attack;

    public float fireRate;
    private float nextAttack;

    public ConsumableItem item;
    public int maxHealth;
    public int maxMana;
    public int strength;
    public int defense;

    private int health;
    private int mana;

    private Armor armor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        animPlayer = GetComponent<Animator>();
        attack = GetComponentInChildren<Attack>();
        mana = maxMana;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Dispara uma linha da posição do jogador até a posição da variável groundCheck
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        // Faz uma verificação usando o onGround para saber se pode usar o double jump
        if (onGround)
        {
            doubleJump = false;
        }


        // Controla o jump e o double jump
        if (Input.GetButtonDown("Jump") && (onGround || !doubleJump))
        {
            jump = true;
            if (!doubleJump && !onGround)
            {
                doubleJump = true;
            }
        }

        // Controla o ataque do jogador
        if (Input.GetButtonDown("Fire1") && Time.time> nextAttack && weaponEquipped != null)
        {
            animPlayer.SetTrigger("Attack");
            attack.PlayAnimation(weaponEquipped.animation);
            nextAttack = Time.time + fireRate;
        }

        // Faz o jogador usar algum item
        if (Input.GetButtonDown("Fire2"))
        {
            UseItem(item);
            Inventory.inventory.RemoveItem(item);
        }

    }

    // Métod que causa o efeito do item usado pelo jogador
    public void UseItem(ConsumableItem item)
    {
        health += item.healthGain;
        if (health >= maxHealth) 
        {
            health = maxHealth;
        }

        mana += item.manaGain;
        if (mana >= maxMana)
        {
            mana = maxMana;
        }

    }

    // É chamada a cada tempo fixo, normalmente usado quando se precisa trabalhar com a física do jogo
    private void FixedUpdate()
    {
        // Variável que recebe a movimentação no eixo da horizontal
        float h = Input.GetAxisRaw("Horizontal");

        // Atualiza a velocidade do rigidbody multiplicando h por speed
        rb.velocity = new Vector2(h * speed, rb.velocity.y) ;

        // Controla quando o método Flip é chamado
        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if(h < 0 && facingRight)
        {
            Flip();
        }

        // Faz o jogador pular
        if (jump)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            jump = false;
        }

    }


    // Método que faz o sprite do jogador seguir o lado do qual se movimenta
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }

    // Método que controla a adição de novas armas para o personagem
    public void AddWeapon(Weapon weapon)
    {
        weaponEquipped = weapon;
        attack.SetWeapon(weaponEquipped.damage);
    }

    // Método que controla a adição de armaduras
    public void AddArmor(Armor item)
    {
        armor = item;
        defense = armor.defense;
    }


    // Método que retorna a vida do jogador
    public int GetHealth()
    {
        return health;
    }

    // Método que retorna a mana do jogador
    public int GetMana()
    {
        return mana;
    }


}
