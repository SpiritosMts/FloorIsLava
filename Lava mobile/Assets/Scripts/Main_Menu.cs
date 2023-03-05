using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * Menu should be opened first time playing the game
 * it fix player bug when opened
 */
public class Main_Menu : MonoBehaviour
{
    public int __checkPointLevel;

    void Start()
    {
        if (PlayerPrefs.GetInt("Begin") == 0)
        {
            //the once compilation code here
            PlayerPrefs.SetInt("_checkPointLevel", 1);
            //set default skin
            PlayerPrefs.SetInt("skin_Index_"+1, 1);
            PlayerPrefs.SetInt("current_skin", 1);
            //set default trail
            PlayerPrefs.SetInt("trail_Index_" + 1, 1);
            PlayerPrefs.SetInt("current_trail", 1);
            //give player 0 gems at the start of the game
            PlayerPrefs.SetInt("player_gems", 300);
            //Set initial adCounter
            PlayerPrefs.SetInt("AdCounter", 0);

            // Compile the upper code just once
            PlayerPrefs.SetInt("Begin", 1);
        }
      
    }

    void Update()
    {
        __checkPointLevel = PlayerPrefs.GetInt("_checkPointLevel");
    }
    /*
    void OnPlayPress()
    {
        //load last opened level
        int _checkPtLvl = PlayerPrefs.GetInt("_checkPointLevel");
        SceneManager.LoadScene("Level_" + _checkPtLvl.ToString());
    }
    */
}
