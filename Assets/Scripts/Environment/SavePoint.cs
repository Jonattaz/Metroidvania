using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    private bool enterSave = false;

    public string message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enterSave)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameManager.gameManager.Save();
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                FindObjectOfType<UpgradeManager>().CallUpgradeManager(); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterSave = true;
            FindObjectOfType<UIManager>().SetMessage(message);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterSave = false;
        }
    }







}

