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

        for (int i = 0; i < Inventory.inventory.weapons.Count; i++)
        {
            if (Inventory.inventory.weapons[i]  == weapon)
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.AddWeapon(weapon);
            Inventory.inventory.AddWeapon(weapon);
            FindObjectOfType<UIManager>().SetMessage(weapon.message);
            Destroy(gameObject);
        }
    }


     

}
