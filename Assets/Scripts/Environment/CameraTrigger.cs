using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{

    // Armazena os valores máximos e mínimos de X e Y da câmera
    public Vector2 maxXandY, minXandY;

    // Referência o script da câmera
    private CameraFollow cameraFollow;


    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraFollow.maxXAndY = maxXandY;
            cameraFollow.minXAndY = minXandY;
        }
    }





}
