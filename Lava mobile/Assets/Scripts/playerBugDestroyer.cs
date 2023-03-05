using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBugDestroyer : MonoBehaviour
{
    [HideInInspector]
   public bool bug;
    void Start()
    {
        bug = false;
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("line"))
        {
            bug = true;
        }
    }
}
