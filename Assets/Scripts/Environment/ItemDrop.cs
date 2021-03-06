using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public ConsumableItem item;

    private SpriteRenderer spriteRend;


    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = item.image;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.inventory.AddItem(item);
            FindObjectOfType<UIManager>().UpdateUI();
            FindObjectOfType<UIManager>().SetMessage(item.message);
            Destroy(gameObject);
        }

    }

}
