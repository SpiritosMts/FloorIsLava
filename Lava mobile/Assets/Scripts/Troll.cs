using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour
{
    public bool troll;
    private bool moveStar;
    public float starSpeed;
    public float star_x_offset;
    public float star_y_offset;
    private GameObject star;
    private Vector2 target;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("star"))
        {
            star = GameObject.FindGameObjectWithTag("star").gameObject;
            target = new Vector2(star.transform.position.x + star_x_offset, star.transform.position.y + star_y_offset);

        }

    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("star"))
        {
            //Debug.Log("star found");
            if (moveStar)
            {
                star.transform.position = Vector3.MoveTowards(star.transform.position, target, Time.deltaTime * starSpeed);
            }
        }
        else
        {
          //  Debug.Log("star not found");

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (troll)
        {
            moveStar = true;
        }
    }
}
