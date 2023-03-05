using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{

    /*
     * if onBecameInvisible doesnt work try using distance between cam and LavaObject condition 
     */
    [SerializeField]
    private float new_X_pos_lava;   
    [SerializeField]
    private float lavaCamDis; 
    [SerializeField]
    public bool isSpike;
    [SerializeField]
    private float min_Y_pos_spike;
    [SerializeField]
    private float max_Y_pos_spike;
    [SerializeField]
    private float new_X_pos_spike;
    [SerializeField]
    private float spikeCamDis;
   // public Transform startCamPoint;
    Camera cam ;
    float width;
    void Start()
    {
        cam = Camera.main;
        width = 2f * cam.orthographicSize * cam.aspect;
        //radmomize y pos at the start
        if (isSpike)
        {
            float random_Y_pos = Random.Range(min_Y_pos_spike, max_Y_pos_spike);
            transform.position = new Vector2(transform.position.x , random_Y_pos);

        }
    }

    void Update()
    {

        //radmomize y pos at the ich time camera pass spikeType
        if (isSpike)
        {
            if ((cam.transform.position.x - width/2) - transform.position.x >= spikeCamDis)
            {
                float random_Y_pos = Random.Range(min_Y_pos_spike, max_Y_pos_spike);

                transform.position = new Vector2(transform.position.x + new_X_pos_spike, random_Y_pos);

                foreach (Transform child in transform)
                {
                   // Do something
                }

            }
        }
        else
        {
            if ((cam.transform.position.x - width / 2) - transform.position.x >= lavaCamDis)
            {
                //transform.position.x += 2lavadis;
                transform.position = new Vector2(transform.position.x + new_X_pos_lava, transform.position.y);
            }
           


        }
    }
   
   
}
