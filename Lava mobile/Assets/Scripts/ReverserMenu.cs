using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverserMenu : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("lava").GetComponent<SurfaceEffector2D>().speed *= -1f;
        }
    }
}
