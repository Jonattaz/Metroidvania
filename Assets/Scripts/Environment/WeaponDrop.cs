using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{

    // Referência ao Scriptable Object Weapon
    public Weapon weapon;

    // Sprite Renderer da arma
    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = weapon.image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.AddWeapon(weapon);
            Inventory.inventory.AddWeapon(weapon);
            Destroy(gameObject);
        }
    }


     

}
