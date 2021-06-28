using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // Referência o painel do menu de pause
    public GameObject pausePanel;

    // Variável que irá mostrar quando o menu está ativo ou não
    private bool pauseMenu = false;

    // Cursor do painel do menu
    public Transform cursor;

    // Opções do menu
    public GameObject[] menuOptions;

    // Index do menuOptions
    private int cursorIndex = 0;

    // Referência à lista de itens
    public GameObject scrollView;

    // Referência ao painel de opções
    public GameObject optionPanel;

    // Referência ao inventário
    private Inventory inventory;


    // Referência o prefab do ItemList
    public GameObject itemListPrefab;

    // Referência o objeto content do painel
    public RectTransform content;

    public List<ItemList> items;

    // Váriavel que irá identificar quando a lista está ativa ou não
    private bool scrollListActive = false;

    // Referência ao texto de descrição dos itens
    public Text descriptionText;

    public Scrollbar scrollVertical;

    // Textos dos status do jogador
    public Text healthText, manaText, strengthText, attackText, defenseText;

    private Player player;

    // UI do jogador
    public Text healthUI, manaUI, soulsUI, potionUI;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.inventory;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu = !pauseMenu;
            cursorIndex = 0;
            scrollListActive = false;
            descriptionText.text = "";
            scrollView.SetActive(false);
            optionPanel.SetActive(true);
            UpdateAtributes();
            UpdateUI();

            // Para não causar o bug do OutOfRange
            if (pauseMenu)
            {
                pausePanel.SetActive(true);
            }
            else
            {
                pausePanel.SetActive(false);
            }
        }

        if (pauseMenu) 
        {

            Vector3 cursorPos = new Vector3();
            if (!scrollListActive)
            {
                cursorPos = menuOptions[cursorIndex].transform.position;
                cursor.position = new Vector3(cursorPos.x - 100, cursorPos.y, cursorPos.z);

            }else if (scrollListActive && items.Count > 0)
            {
                cursorPos = items[cursorIndex].transform.position;
                cursor.position = new Vector3(cursorPos.x - 75, cursorPos.y, cursorPos.z);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                if (!scrollListActive && cursorIndex >= menuOptions.Length - 1)
                {
                    cursorIndex = menuOptions.Length - 1;
                }
                else if(scrollListActive && cursorIndex >= items.Count - 1)
                {
                    if (items.Count == 0)
                    {
                        cursorIndex = 0;
                    }
                    else
                    {
                        cursorIndex = items.Count - 1;
                    }
                }
                else
                { 
                    cursorIndex++;
                }

                if (scrollListActive && items.Count > 0)
                {
                    scrollVertical.value -= (1f / (items.Count - 1));
                    UpdateDescription();
                }

            }else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (cursorIndex == 0)
                {
                    cursorIndex = 0;
                }
                else
                {
                    cursorIndex--;
                }
                if (scrollListActive && items.Count > 0)
                {
                    scrollVertical.value += (1f / (items.Count - 1));
                    UpdateDescription();
                }
            }

            if (Input.GetButtonDown("Submit") && !scrollListActive)
            {
                optionPanel.SetActive(false);
                scrollView.SetActive(true);
                RefreshItemList();
                UpdateItemsList(cursorIndex);
                cursorIndex = 0;
                if (items.Count > 0)
                {
                    UpdateDescription();
                    scrollListActive = true;
                }

            }else if (Input.GetButtonDown("Submit") && scrollListActive)
            {
                if (items.Count > 0)
                { 
                    UseItem();
                }
            }

        }

    }

    // Método responsável pelo texto da descrição estar de acordo com o item
    void UpdateDescription()
    {
        if (items[cursorIndex].weapon != null)
        {
            descriptionText.text = items[cursorIndex].weapon.description;
        } else if (items[cursorIndex].consumableItem != null)
        {
            descriptionText.text = items[cursorIndex].consumableItem.description;
        } else if (items[cursorIndex].key != null)
        {
            descriptionText.text = items[cursorIndex].key.description;

        } else if (items[cursorIndex].armor != null)
        {
            descriptionText.text = items[cursorIndex].armor.description;
        }

    }

    // Método que controla a atualização dos itens no inventário, destruindo os items duplicados
    void RefreshItemList()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Destroy(items[i].gameObject); 
        }

        items.Clear();
    }

    // Método responsável por atualizar a lista de itens no inventário
    void UpdateItemsList(int option)
    {
        if (option == 0)
        {
            for (int i = 0; i < inventory.weapons.Count; i++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().SetUpWeapon(inventory.weapons[i]);
                items.Add(tempItem.GetComponent<ItemList>());
            }

        }else if (option == 1)
        {
            for (int i = 0; i < inventory.armor.Count; i++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().SetUpArmor(inventory.armor[i]);
                items.Add(tempItem.GetComponent<ItemList>());
            }
        }
        else if (option == 2)
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().SetUpItem(inventory.items[i]);
                items.Add(tempItem.GetComponent<ItemList>());
            }
        }
        else if (option == 3)
        {
            for (int i = 0; i < inventory.keys.Count; i++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().SetUpKey(inventory.keys[i]);
                items.Add(tempItem.GetComponent<ItemList>());
            }
        }
    }

     // Método responsável pela atualização dos status do jogador
     void UpdateAtributes()
    {
        healthText.text = "Health: " + player.GetHealth() + "/" + player.maxHealth;
        manaText.text = "Mana: " + player.GetMana() + "/" + player.maxMana;
        strengthText.text = "Strength " + player.strength;
         attackText.text = "Attack: " + (player.strength + player.GetComponentInChildren<Attack>().GetDamage());
        defenseText.text = "Defense: " + player.defense;
    }

    // Método que permite o jogador usar algum item pelo menu
    void UseItem()
    {
        if (items[cursorIndex].weapon != null)
        {
            player.AddWeapon(items[cursorIndex].weapon);
        }else if (items[cursorIndex].consumableItem != null)
        {
            player.UseItem(items[cursorIndex].consumableItem);
            inventory.RemoveItem(items[cursorIndex].consumableItem);
            cursorIndex = 0;
            RefreshItemList();
            UpdateItemsList(2);
            scrollVertical.value = 1;

        }else if (items[cursorIndex].armor != null)
        {
            player.AddArmor(items[cursorIndex].armor);
        }
        UpdateAtributes();
        UpdateDescription();
    }

    // Método que atualiza todos os elementos de UI(vida,mana e souls)
    public void UpdateUI()
    {
        healthUI.text = player.GetHealth() + " / " + player.maxHealth;
        manaUI.text = player.GetMana() + " / " + player.maxMana;
        soulsUI.text = "Souls: " + player.souls;
        potionUI.text = "X" + inventory.CountItems(player.item);
    }


}















