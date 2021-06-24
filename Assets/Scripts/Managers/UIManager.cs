using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            cursorIndex = 0;
            pauseMenu = !pauseMenu;
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
            Vector3 cursorPos = menuOptions[cursorIndex].transform.position;
            cursor.position = new Vector3(cursorPos.x - 100, cursorPos.y, cursorPos.z);
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                cursorIndex++;

            }else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                cursorIndex--;
            }
        }

    }
}
