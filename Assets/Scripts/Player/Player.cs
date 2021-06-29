using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Enumerador para as skills do jogo, novas podem ser adicionadas aqui e validadas no método SetPlayerSkill
public enum PlayerSkill
{
    dash, doubleJump
}

public class Player : MonoBehaviour
{
    // Referência o rigidbody do jogador
    private Rigidbody2D rb;

    // Velocidade máxima possível
    public float maxSpeed;

    // Velocidade atual
    private float speed;

    public int manaCost;

    private bool facingRight = true;

    private bool onGround;
    public Transform groundCheck;

    private bool jump = false;
    private bool doubleJump;
    public float jumpForce;

    // Variável do tipo weapon
    public Weapon weaponEquipped;

    // Referência ao animator do jogador
    private Animator animPlayer;

    public Rigidbody2D projectile;

    private Attack attack;

    public float fireRate;
    private float nextAttack;

    public int souls;

    public ConsumableItem item;
    public int maxHealth;
    public int maxMana;
    public int strength;
    public int defense;

    private int health;
    private int mana;

    public Armor armor;

    // Variável que armazena o sprite renderer do jogador
    private SpriteRenderer spriteRend;
    
    // Variável que armazena o estado de se o jogador pode ou não causar dano
    private bool canDamage = true;

    // Variável que armazena o estado de vida do jogador
    private bool isDead =  false;

    private bool dash = false;

    public bool doubleJumpSkill = false;
    public bool dashSkill = false;

    // Referência ao inventário
    private Inventory inventory;

    // Váriavel que controla a força do dash
    public float dashForce;

    // Variável que representa e da acesso ao GameManager
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
        attack = GetComponentInChildren<Attack>();
        spriteRend = GetComponent<SpriteRenderer>();
        inventory = Inventory.inventory;
        gameManager = GameManager.gameManager;

        SetPlayer(); // ERRO DE REFFERÊNCIA
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // Dispara uma linha da posição do jogador até a posição da variável groundCheck
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            // Faz uma verificação usando o onGround para saber se pode usar o double jump
            if (onGround)
            {
                doubleJump = false;
            }


            // Controla o jump e o double jump
            if (Input.GetButtonDown("Jump") && (onGround || (!doubleJump && doubleJumpSkill)))
            {
                jump = true;
                if (!doubleJump && !onGround)
                {
                    doubleJump = true;
                }
            }

            // Controla o ataque do jogador
            if (Input.GetButtonDown("Fire1") && Time.time > nextAttack && weaponEquipped != null)
            {
                dash = false;
                animPlayer.SetTrigger("Attack");
                attack.PlayAnimation(weaponEquipped.animation);
                nextAttack = Time.time + fireRate;
            }

            if (inventory.CountItems(item) > 0)
            {
                // Faz o jogador usar o item poção
                if (Input.GetButtonDown("Fire2"))
                {
                    UseItem(item);
                    Inventory.inventory.RemoveItem(item);
                    FindObjectOfType<UIManager>().UpdateUI();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q) && onGround && (!dash && dashSkill))
            {
                rb.velocity = Vector2.zero;
                animPlayer.SetTrigger("Dash");
            }else if (Input.GetKeyDown(KeyCode.Q) && mana >= manaCost)
            {
                Rigidbody2D tempProjectile = Instantiate(projectile, transform.position, transform.rotation);
                tempProjectile.GetComponent<Attack>().SetWeapon(50);
                if (facingRight)
                {
                    tempProjectile.AddForce(new Vector2(5,10), ForceMode2D.Impulse);

                }
                else
                {
                    tempProjectile.AddForce(new Vector2(-5, 10), ForceMode2D.Impulse);
                }

                mana -= manaCost;
                FindObjectOfType<UIManager>().UpdateUI();

            }



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
        if (!isDead)
        {
            // Variável que recebe a movimentação no eixo da horizontal
            float h = Input.GetAxisRaw("Horizontal");

            if (canDamage && !dash)
            {
                // Atualiza a velocidade do rigidbody multiplicando h por speed
                rb.velocity = new Vector2(h * speed, rb.velocity.y);
                animPlayer.SetFloat("Speed", Mathf.Abs(h));
            }

            // Controla quando o método Flip é chamado
            if (h > 0 && !facingRight)
            {
                Flip();
            }
            else if (h < 0 && facingRight)
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

            if (dash)
            {
                int hforce = facingRight ? 1 : -1;
                rb.velocity = Vector2.left * dashForce * hforce; 

            }

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
        defense = armor.defense; // ERRO DE REFERÊNCIA NO SAVE
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
    
    // Método responsável pelo dano recebido
    public void TakeDamage(int damage)
    {
        if (canDamage)
        {
            canDamage = false;
            health -= (damage - defense);
            FindObjectOfType<UIManager>().UpdateUI();
            if (health <= 0)
            {
                animPlayer.SetTrigger("Dead");
                Invoke("ReloadScene", 1f);
                isDead = true;
            }
            else
            {
                StartCoroutine(DamageCoroutine());
            }

        }

    }

    // Corrotina que controla a parte estética do dano levado pelo jogador
    IEnumerator DamageCoroutine()
    {
        for (float i = 0; i < 0.6f; i += 0.2f)
        {
            spriteRend.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRend.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        canDamage = true;
    }

    // Método que recarrega a cena caso o jogador morra
    void ReloadScene()
    {
        Souls.instance.gameObject.SetActive(true);
        Souls.instance.souls = souls;
        Souls.instance.transform.position = transform.position;
        /// recarrega a cena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }  

    // Método que passa a variável dash para verdadeiro
    public void DashTrue()
    {
        dash = true;
    }

    // Método que passa a váriável dash para falso
    public void DashFalse()
    {
        dash = false;
    }

    // Método que controla o desbloqueio de novas skills
    public void SetPlayerSkill(PlayerSkill skill)
    {
        if (skill == PlayerSkill.dash)
        {
            dashSkill = true;

        }else if ( skill == PlayerSkill.doubleJump)
        {
            doubleJumpSkill = true;
        }
    }


    // Método que define alguns valores para o player, como vida, mana e etc
    public void SetPlayer()
    {
        Vector3 playerPos = new Vector3(gameManager.playerPosX, gameManager.playerPosY, 0);

        transform.position = playerPos;
        maxHealth = gameManager.health;
        maxMana = gameManager.mana;
        speed = maxSpeed;
        health = maxHealth;
        mana = maxMana;
        strength = gameManager.strength;
        souls = gameManager.souls;
        doubleJumpSkill = gameManager.canDoubleJump;
        dashSkill = gameManager.canBackDash;
        if (gameManager.currentArmorId > 0)
        {
            AddArmor(Inventory.inventory.itemDataBase.GetArmor(gameManager.currentArmorId)); // ERRO DE REFERÊCIA NO SAVE
        }
        if (gameManager.currentWeaponId > 0)
        {
            AddWeapon(Inventory.inventory.itemDataBase.GetWeapon(gameManager.currentWeaponId));
        }



    }


    // Método que verifica se as habilidades estão repetidas
    public bool GetSkill(PlayerSkill skill)
    {
        if (skill == PlayerSkill.dash)
        {
            return dashSkill;
        }else if (skill == PlayerSkill.doubleJump)
        {
            return doubleJumpSkill;
        }else
        {
            return false;
        }
    }


}









