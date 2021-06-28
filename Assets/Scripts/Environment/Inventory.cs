using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Lista que armazena as chaves, funciona como um array
    public List<Key> keys;

    // Lista que armazena as armas
    public List<Weapon> weapons;

    public List<ConsumableItem> items;

    public List<Armor> armor; 

    public static Inventory inventory;

    private void Awake()
    {
        if (inventory == null)
        {
            inventory = this;
        }else if (inventory != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Método que contabiliza a quantidade de itens que o jogador possui
    public int CountItems(ConsumableItem item)
    {
        int numberOfItems = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (item == items[i])
            {
                numberOfItems++;
            }
        }

        return numberOfItems;
    }


    // Método que adiciona armas ao inventário
    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    // Método que adiciona armas ao inventário
    public void AddKey(Key key)
    {
        keys.Add(key);
    }

    // Método que adiciona itens ao inventário
    public void AddItem(ConsumableItem item)
    {
        items.Add(item);
     
    }

    // Método que remove itens do inventário
    public void RemoveItem(ConsumableItem item)
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == item)
            {
                items.RemoveAt(i);
                break;
            }
        }
         

    }



    // Método responsável por verificar se o jogador possui a chave correta
    public bool CheckKey(Key key)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            // verifica se a chave do index é igual a que foi passada como parâmetro
            if (keys[i] == key)
            {
                return true;
            }
        }
        return false;

    }


}















