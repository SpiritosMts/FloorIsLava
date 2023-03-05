using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;



public class Trails : MonoBehaviour
{
    public int trail_Index;
    //public GameObject trail_Icon;
    public Image bg_image;
    public int _price;
    public GameObject Lock_Sprite;

    //public List<int> skinsAvailable ;
    void Start()
    {
        _price = 2;

        //make TaskOnClick work when button clicked
        GetComponent<Button>().onClick.AddListener(TaskOnClick);

        
        //get reference to the skin_Icon
        //trail_Icon = transform.Find("Level_bg").transform.Find("Trail_Icon").gameObject;
        //get reference to the bg_image
        bg_image = transform.Find("Level_bg").GetComponent<Image>();
        //get reference to the lock sprite
    
            Lock_Sprite = transform.Find("Lock_img").gameObject;

        
        if (Lock_Sprite.activeSelf)
        {
            _price = transform.Find("Lock_img").transform.Find("price").GetComponent<price>()._price;

        }
        //make each available skin visible at the start
        // if (skinsAvailable.Contains(skin_Index))==> the old condition
        if (PlayerPrefs.HasKey("trail_Index_" + trail_Index))
        {
            //Disable the lock Sprite for < EACH > skin you got
            Lock_Sprite.SetActive(false);
        }
        else
        {
            Lock_Sprite.SetActive(true);

        }


        //set the current skin to green at the start       
        if (PlayerPrefs.GetInt("current_trail") == trail_Index)
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
        if (PlayerPrefs.HasKey("trail_Index_" + trail_Index) || PlayerPrefs.GetInt("player_gems") >= _price)
        {
            if (PlayerPrefs.HasKey("trail_Index_" + trail_Index))
            {
                Debug.Log("ITS A SELECT");

            }
            if (PlayerPrefs.GetInt("player_gems") >= _price)
            {
                Debug.Log("ITS A BUY");
            }
            //Disable the lock Sprite for < EACH > skin you got
            Lock_Sprite.SetActive(false);

            /*
            //enable the skin icon
            skin_Icon.SetActive(true);
            */

            //change all other skins bg to color gray
            foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (gameObj.name == "Level_bg")
                {
                    gameObj.GetComponent<Image>().color = new Color32(101, 101, 101, 255);
                }
            }
            //change selected one color to green
            bg_image.color = new Color32(65, 164, 32, 150);


            //chnage the current skin 
            PlayerPrefs.SetInt("current_trail", trail_Index);
            //add this skin to skinsAvailable
            if (!PlayerPrefs.HasKey("trail_Index_" + trail_Index))
            {
                //substract the element price from gems player have
                PlayerPrefs.SetInt("player_gems", PlayerPrefs.GetInt("player_gems") - _price);
                //save skin_Index in skinsAvailable for later
                PlayerPrefs.SetInt("trail_Index_" + trail_Index, trail_Index);

            }


        }

    }
}
