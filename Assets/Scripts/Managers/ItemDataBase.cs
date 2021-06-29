using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Weapon> weapons;
    public List<ConsumableItem> consumableItems;
    public List<Armor> armors;
    public List<Key> keys;

    // Método que retorna as armas para o jogador
    public Weapon GetWeapon(int itemId)
    {
        foreach (var item in weapons)
        {
            if (item.itemID == itemId)
            {
                return item;
            }
        }
        return null; 
    }


    // Método que retorna os itens cosumiveis para o jogador
    public ConsumableItem GetConsumableItem(int itemId)
    {
        foreach (var item in consumableItems)
        {
            if (item.itemID == itemId)
            {
                return item;
            }
        }
        return null;

    }

    // Método que retorna as armaduras para o jogador
    public Armor GetArmor(int itemId)
    {
        foreach (var item in armors)
        {
            if (item.itemID == itemId)
            {
                return item;
            }
        }

        return null;
    }

    // Método que retorna as chaves para o jogador
    public Key GetKey(int itemId)
    {
        foreach (var item in keys)
        {
            if (item.itemID == itemId)
            {
                return item;
            }
        }

        return null;
    }



}












