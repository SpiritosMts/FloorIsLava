using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    public float rotationSpeed;
    public float traslationSpeed;
    public bool Translate;
    public bool Rotate;
    private float x_pingPong;
    private float y_pingPong;
    public float x_distance;
    public float y_distance;
    public bool x;
    public bool y;
    private Vector3 startPos;
    private float Ycounter;
    private float Xcounter;
    public float start_X_counter;
    public float start_Y_counter;
   
  
    private void Start()
    {
        startPos = transform.position;
        Xcounter = start_X_counter;
        Ycounter = start_Y_counter;
       

    }
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (FindObjectOfType<GameManager>().isEndless)
        {
            if (collision.gameObject.CompareTag("lava"))
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    */
    void Update()
    {
       
        if (Rotate)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.World);
        } 
        if (Translate)
        {
            if (x && !y)
            {
                Xcounter += Time.deltaTime;
                x_pingPong = Mathf.PingPong(Xcounter, x_distance);

                Vector3 pos = new Vector3(startPos.x + x_pingPong * traslationSpeed, startPos.y  , 0f);
                transform.position = pos;
            } 
            else if  (!x && y)
            {

                Ycounter += Time.deltaTime;
                y_pingPong = Mathf.PingPong(Ycounter, y_distance);
              
                Vector3 pos = new Vector3(startPos.x , startPos.y + y_pingPong * traslationSpeed, 0f);
                transform.position = pos;
                
            }
            else if (x && y)
            {
                Xcounter += Time.deltaTime;
                x_pingPong = Mathf.PingPong(Xcounter, x_distance);
                Ycounter += Time.deltaTime;
                y_pingPong = Mathf.PingPong(Ycounter, y_distance);

                Vector3 pos = new Vector3(startPos.x + x_pingPong * traslationSpeed, startPos.y + y_pingPong * traslationSpeed, 0f);
                transform.position = pos;
            }
            

        }
    }
}
