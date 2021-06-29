using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[Serializable]
// Classe que contém todos os dados que precisarão ser salvos
class PlayerData
{
    public int health;
    public int mana;
    public int strength;
    public float playerPosX, playerPosY;
    public float minCamX, maxCamX, minCamY, maxCamY;
    public int souls;
    public int[] itemId;
    public int[] weaponId;
    public int[] armorId;
    public int[] keyId;
    public int upgradeCost;
    public int currentWeaponId;
    public int currentArmorId;
    public bool canDoubleJump;
    public bool canBackDash;
}

public class GameManager : MonoBehaviour
{
    // Dados que precisam ser salvos
    public int health = 100;
    public int mana = 50;
    public int strength = 10;
    public float playerPosX, playerPosY;
    public float minCamX, maxCamX, minCamY, maxCamY;
    public int souls;
    public int[] itemId;
    public int[] weaponId;
    public int[] armorId;
    public int[] keyId;
    public int upgradeCost;
    public int currentWeaponId;
    public int currentArmorId;
    public bool canDoubleJump = false;
    public bool canBackDash = false;


    // Variável que armazena local em que irá ficar o save
    private string filePath;


    // Instância do game manager
    public static GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if(gameManager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath + "/playerInfo.dat";

        Load();
    }

    // Método resposável pelo sistema de save do jogo
    public void Save()
    {
        Player player = FindObjectOfType<Player>();
        CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();


        itemId = new int[Inventory.inventory.items.Count];
        weaponId = new int[Inventory.inventory.items.Count];
        armorId = new int[Inventory.inventory.armors.Count];
        keyId = new int[Inventory.inventory.keys.Count];

        // Loop para os items
        for (int i = 0; i < itemId.Length; i++)
        {
            itemId[i] = Inventory.inventory.items[i].itemID;
        }
        
        // Loop para as armas
        for (int i = 0; i < weaponId.Length; i++)
        {
            weaponId[i] = Inventory.inventory.weapons[i].itemID;
        }
        
        // Loop para as armaduras
        for (int i = 0; i < armorId.Length; i++)
        {
           armorId[i] = Inventory.inventory.armors[i].itemID;
        }
        
        
        // Loop para as chaves
        for (int i = 0; i < keyId.Length; i++)
        {
            keyId[i] = Inventory.inventory.keys[i].itemID;
        }


        BinaryFormatter binary = new BinaryFormatter();

        FileStream file = File.Create(filePath);


        PlayerData data = new PlayerData();

        data.health = player.maxHealth;
        data.mana = player.maxMana;
        data.playerPosX = player.transform.position.x;
        data.playerPosY = player.transform.position.y;
        data.souls = player.souls;
        data.strength = player.strength;
        data.upgradeCost = upgradeCost;
        data.maxCamX = cameraFollow.maxXAndY.x;
        data.minCamX = cameraFollow.minXAndY.x;
        data.maxCamY = cameraFollow.maxXAndY.y;
        data.minCamY = cameraFollow.minXAndY.y;

        if (player.weaponEquipped != null)
        {
            data.currentArmorId = player.weaponEquipped.itemID;
        }

        if (player.armor != null)
        {
            data.currentArmorId = player.armor.itemID;
        }
        data.canDoubleJump = player.doubleJumpSkill;
        data.canBackDash = player.dashSkill;

        data.itemId = itemId;
        data.weaponId = weaponId;
        data.armorId = armorId;
        data.keyId = keyId;

        binary.Serialize(file, data);
        file.Close();

        Debug.Log("Salvou");

        FindObjectOfType<UIManager>().SetMessage("Jogo salvo");


    }


    // Método que carrega os dados do jogador
    public void Load()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);

            PlayerData data = (PlayerData)binary.Deserialize(file);
            file.Close();

            health = data.health;
            mana = data.mana;
            strength = data.strength;
            playerPosX = data.playerPosX;
            playerPosY = data.playerPosY;
            maxCamX = data.maxCamX;
            maxCamY = data.maxCamY;
            minCamX = data.minCamX;
            minCamY = data.minCamY;
            souls = data.souls;
            upgradeCost = data.upgradeCost;
            currentArmorId = data.currentArmorId;
            currentWeaponId = data.currentWeaponId;
            canDoubleJump = data.canDoubleJump;
            canBackDash = data.canBackDash;
            itemId = data.itemId;
            weaponId = data.weaponId;
            armorId = data.armorId;
            keyId = data.keyId;

        }
    }







}
















