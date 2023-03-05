using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;



public class Skins : MonoBehaviour
{
    public int skin_Index;
    //public GameObject skin_Icon;
    public Image bg_image;
    public int _price;
    public GameObject Lock_Sprite;
        void Start()
    {
        _price = 2;
        //make TaskOnClick work when button clicked
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
      
        //get reference to the skin_Icon
        //skin_Icon = transform.Find("Level_bg").transform.Find("Skin_Icon").gameObject;
        //get reference to the bg_image
        bg_image = transform.Find("Level_bg").GetComponent<Image>();
        //get reference to the lock sprite
        
            Lock_Sprite = transform.Find("Lock_img").gameObject;

        
        if (Lock_Sprite.activeSelf)
        {
            _price = transform.Find("Lock_img").transform.Find("price").GetComponent<price>()._price;

        }

        //make each available skin visible at the start
        if (PlayerPrefs.HasKey("skin_Index_" + skin_Index))
        {
            //Disable the lock Sprite for < EACH > skin you got
            Lock_Sprite.SetActive(false);
        }
        else
        {
            Lock_Sprite.SetActive(true);

        }


        //set the current skin to green at the start       
        if (PlayerPrefs.GetInt("current_skin") == skin_Index )
        {
            //set to green at the start
            bg_image.color = new Color32(65, 164, 32, 150);
        }
        
       
    }

 
    //assosiate a function to the button
    void TaskOnClick()
    {
        if (GameObject.Find("AudioManager"))
        {
            GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
        }
        //condition to take skin(canBuySkin) ==> comlete levels , gather coins , watch ads ....
        if (PlayerPrefs.HasKey("skin_Index_" + skin_Index) || PlayerPrefs.GetInt("player_gems") >= _price )
        {

            if (PlayerPrefs.HasKey("skin_Index_" + skin_Index))
            {
                Debug.Log("ITS A SELECT");

            }
            if (PlayerPrefs.GetInt("player_gems") >= _price)
            {
                Debug.Log("ITS A BUY");
            }
            //Disable the lock Sprite for < EACH > skin you got
            Lock_Sprite.SetActive(false);


            //change all other skins bg to color gray
            foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (gameObj.name == "Level_bg")
                {
                    gameObj.GetComponent<Image>().color = new Color32(101,101,101,255);
                }
            }           
            //change selected one color to green
            bg_image.color = new Color32(65, 164, 32, 150);


            //chnage the current skin 
            PlayerPrefs.SetInt("current_skin", skin_Index);
            //add this skin to skinsAvailable
            if (!PlayerPrefs.HasKey("skin_Index_" + skin_Index))
            {
                //substract the element price from gems player have
                PlayerPrefs.SetInt("player_gems", PlayerPrefs.GetInt("player_gems") - _price);
                //save skin_Index in skinsAvailable for later
                PlayerPrefs.SetInt("skin_Index_" + skin_Index, skin_Index);

            }
            
        }

    }
}
