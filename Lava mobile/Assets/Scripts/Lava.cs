using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D collision)
    {
        //when fall in lava
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            //play particle system
            //play losing sound effect
        }
    }

    void Update()
    {
        
    }
}
