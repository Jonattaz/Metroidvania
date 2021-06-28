using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Variável animator, para trocar as animações
    private Animator anim;

    // Variável que representa o dano
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Método que serve para tocar a animação
    public void PlayAnimation(AnimationClip clip)
    {
        anim.Play(clip.name);
    }

    // Método que adiciona o dano para cada arma
    public void SetWeapon(int damageValue)
    {
        damage = damageValue;
    }

    // Método que retorna o dano máximo que o jogador pode causar
    public int GetDamage()
    {
        return damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage + FindObjectOfType<Player>().strength);
        }


    }








}
