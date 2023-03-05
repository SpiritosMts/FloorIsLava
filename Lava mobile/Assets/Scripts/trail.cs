using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trail : MonoBehaviour
{
    [SerializeField]
    private float x_offset;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector2( player.transform.position.x+ x_offset, player.transform.position.y);
        }
    }
}
