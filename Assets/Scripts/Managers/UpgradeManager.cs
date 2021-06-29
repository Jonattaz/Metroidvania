using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Text upgradeCostText;
    public Text[] attributesText;

    public GameObject upgradePanel;
    private bool upgradeActive = false;

    private int cursorIndex;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradeActive)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                UpdateText();
                cursorIndex++;
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                UpdateText();
                cursorIndex--;
            }

            if (cursorIndex == 0)
            {
                attributesText[0].text = "Life: " + player.maxHealth + ">" + Mathf.RoundToInt(player.maxHealth * 0.1f);
                attributesText[0].color = Color.green;
            }else if (cursorIndex == 1)
            {
                attributesText[1].text = "Mana: " + player.maxMana + ">" + Mathf.RoundToInt(player.maxMana * 0.1f);
                attributesText[1].color = Color.green;
            }else if (cursorIndex == 2)
            {
                attributesText[2].text = "Strength: " + player.strength + ">" + Mathf.RoundToInt(player.strength * 0.1f);
                attributesText[2].color = Color.green;

            }

            if (Input.GetButtonDown("Submit") && player.souls >= GameManager.gameManager.upgradeCost)
            {
                player.souls -= GameManager.gameManager.upgradeCost;
                GameManager.gameManager.upgradeCost += (GameManager.gameManager.upgradeCost / 2);
                if (cursorIndex == 0)
                {
                    player.maxHealth += (int)(player.maxHealth * 0.1f);
                }else if (cursorIndex == 1)
                {
                    player.maxMana += (int)(player.maxMana * 0.1f);
                }else if (cursorIndex == 2)
                {
                    player.strength += (int)(player.strength * 0.1f);
                }

                UpdateText();
                FindObjectOfType<UIManager>().UpdateUI();
            }            
        }
        
    }


    // Método que atualiza a cor dos textos
    public void UpdateText()
    {
        upgradeCostText.text = "Souls cost: " + GameManager.gameManager.upgradeCost + " Souls:" + player.souls;
        attributesText[0].text = "Life: " + player.maxHealth;
        attributesText[1].text = "Mana: " + player.maxMana;
        attributesText[2].text = "Força: " + player.strength;
        for (int i = 0; i < attributesText.Length; i++)
        {
            attributesText[i].color = Color.white;
        }

    }


    // Método responsável pela tela e o save
    public void CallUpgradeManager()
    {
        upgradeActive = !upgradeActive;
        cursorIndex = 0;
        UpdateText();
        if (upgradeActive)
        {
            upgradePanel.SetActive(true);
        }
        else
        {
            upgradePanel.SetActive(false);
        }
    }



}


















