using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class charging : MonoBehaviour
{
    public Image ChargingSprite;
    public GameObject ReviveWindow;
    [SerializeField]
    private float speed;
    public bool shown;


     void Start()
    {
        ChargingSprite = GameObject.Find("chargingIMG (1)").GetComponent<Image>();
        ReviveWindow = this.gameObject;
    }
    void Update()
    {
        ChargingSprite.fillAmount -= speed * Time.deltaTime;
        if(ChargingSprite.fillAmount <= 0f)
        {
            //hide Revive window
            ReviveWindow.SetActive(false);
        }
    }


  
}
