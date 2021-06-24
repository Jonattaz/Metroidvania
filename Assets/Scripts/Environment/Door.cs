using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Key key;

    // Sprite da porta aberta
    public Sprite doorOpen;

    // Sprite renderer do objeto
    private SpriteRenderer spriteRend;

    // Referência ao box collider do objeto
    private BoxCollider2D boxCol;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Inventory.inventory.CheckKey(key))
            {
                spriteRend.sprite = doorOpen;
                boxCol.enabled = false;
            }
        }
    }


}
