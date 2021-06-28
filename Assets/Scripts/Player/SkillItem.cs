using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public PlayerSkill skill;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.SetPlayerSkill(skill);
            Destroy(gameObject);
        }
    }


}
