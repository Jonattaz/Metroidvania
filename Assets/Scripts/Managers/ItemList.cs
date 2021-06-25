using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    // ITENS CONSUMÍVEIS
    public ConsumableItem consumableItem;
    
    // IMAGENS E DESCRIÇÕES DOS ITENS
    public Text text;
    public Image image;

    // ARMAS
    public Weapon weapon;

    // CHAVES
    public Key key;

    // AMADURAS
    public Armor armor;


    // Método que configura os itens do tipo consumível
    public void SetUpItem(ConsumableItem item)
    {
        consumableItem = item;
        image.sprite = consumableItem.image;
        text.text = consumableItem.itemName;

    }


    // Método que configura as chaves 
    public void SetUpKey(Key keyItem)
    {
        key = keyItem;
        image.sprite = key.image;
        text.text = key.keyName;
    }

    // Método que configura as armas
    public void SetUpWeapon(Weapon weaponItem)
    {
        weapon = weaponItem;
        image.sprite = weapon.image;
        text.text = weapon.weaponName;
    }


    // Método que configura a armadura
    public void SetUpArmor(Armor armorItem)
    {
        armor = armorItem;
        image.sprite = armor.image;
        text.text = armor.armorName;
    }


}
