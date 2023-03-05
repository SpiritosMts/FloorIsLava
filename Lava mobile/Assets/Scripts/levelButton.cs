using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/*
 * you can you can get level_button_text as a text then load scene with scene name (in this case levels must be as [Level_INDEX])
 * or you can convert level_button_text to int then load scene with scene index
 * this script is associated in every level parralel to Level_button
 * Level_bg and Level_Text should have same name for all levels
 * level text should have TextMeshPro 
*/

public class levelButton : MonoBehaviour
{
    //public int __checkPointLevel;
    private TextMeshProUGUI textField;
    private Transform Texttrans;
     
    public int scene_Index;
     void Start()
    {
        //make TaskOnClick work when button clicked
        Button btn = GetComponent<Button>();
       btn.onClick.AddListener(TaskOnClick);

        //get reference to the level_button_text
        Texttrans = transform.Find("Level_bg").transform.Find("Level_Text ");
        textField = Texttrans.gameObject.GetComponent<TextMeshProUGUI>();

        //convert text to int 
        scene_Index = int.Parse(textField.text);

        //Disable the lock Sprite for < EACH > completed levels 
        if (scene_Index <= PlayerPrefs.GetInt("_checkPointLevel"))
        {
             GameObject Lock_Sprite = transform.Find("Lock_img").gameObject;
            Lock_Sprite.SetActive(false);
        }
       
    }
    // show __checkPointLevel in inspector
    void Update()
    {
        //__checkPointLevel = PlayerPrefs.GetInt("_checkPointLevel");
    }
    //assosiate a function to the button
    void TaskOnClick()
    {
        if (GameObject.Find("AudioManager"))
        {
            GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
        }
        //load level related to pressed button
        if (scene_Index <= PlayerPrefs.GetInt("_checkPointLevel"))
        {
            SceneManager.LoadScene("Level_" +scene_Index.ToString());
        }
        Debug.Log("Level_" + scene_Index.ToString() +" opened");

    }
}
