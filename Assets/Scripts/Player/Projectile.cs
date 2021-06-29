using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Variável que controla o tempo levado para destruir este objeto
    public float destroyTime;

    // Variável que representa a velocidade do objeto
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime); 
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.forward * speed * Time.deltaTime);

    }
}
