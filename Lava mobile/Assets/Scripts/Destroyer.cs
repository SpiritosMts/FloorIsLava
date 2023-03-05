using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float Distance_playerNDdestroyer;
    private float abs_dis;
    
    private Transform player;

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player != null)
        {
            abs_dis = Mathf.Abs(player.position.x - transform.position.x);
            if (abs_dis <= Distance_playerNDdestroyer)
            {
                //camera should decrease speed or stop
                Camera.main.GetComponent<FollowPlayer>().smoothTime = 0.0001f;
            }
            else
            {
                //camera should follow player
                Camera.main.GetComponent<FollowPlayer>().smoothTime = 4f;
            }
        }
    }


}
