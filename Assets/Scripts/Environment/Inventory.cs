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

    public List<Armor> armors; 

    public static Inventory inventory;

    public ItemDataBase itemDataBase;


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
        LoadInventory();
    }

    private void Start()
    {
       
        FindObjectOfType<UIManager>().UpdateUI();
    }


    // Método que carrega os itens que estiverem salvos no game manager
    void LoadInventory()
    {
        for (int i = 0; i < GameManager.gameManager.weaponId.Length; i++)
        {
            AddWeapon(itemDataBase.GetWeapon(GameManager.gameManager.weaponId[i]));
        }


        for (int i = 0; i < GameManager.gameManager.itemId.Length; i++)
        {
            AddItem(itemDataBase.GetConsumableItem(GameManager.gameManager.itemId[i]));
        }

        for (int i = 0; i < GameManager.gameManager.armorId.Length; i++)
        {
            AddArmor(itemDataBase.GetArmor(GameManager.gameManager.armorId[i]));
        }


        for (int i = 0; i < GameManager.gameManager.keyId.Length; i++)
        {
            AddKey(itemDataBase.GetKey(GameManager.gameManager.keyId[i]));
        }

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


    // Método que adiciona armaduras ao inventário
    public void AddArmor(Armor armor)
    {
        this.armors.Add(armor);
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















