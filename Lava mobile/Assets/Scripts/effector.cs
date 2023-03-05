using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effector : MonoBehaviour
{
    public float rotationSpeed=90f;
    public bool Rotate;
   
    private void Start()
    {
    }
    void Update()
    {
        if (Rotate)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.World);
        }

      
    }
  
   
}
