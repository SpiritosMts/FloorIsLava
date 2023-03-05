using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/*
 int1 = ++int2 =======>> if(int1=0,int2=1)=> int1=2 , int2=2
 GameObject.Find("") can work on underPath object
may active shop buttons for all UIs
 */
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
           


            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }
    /*
    private static bool applicationIsQuitting = false;
    
    public void OnDestroy()
    {
        Debug.Log("Gets destroyed");
        applicationIsQuitting = true;
    }
    */
    public bool isEndless;
    public bool isLevels;
    [SerializeField]
    public int GemsToAddAfterAd;


    public GameObject adsPanel;
    public GameObject pause_backGround;
    public GameObject complete_backGround;
    public GameObject fail_backGround;
    public GameObject fail_backGround_tight;
    public GameObject fail_backGround_spikes;
    public GameObject fail_backGround_noText;
    

    public float timeToShowScreens;
    public float timeToShowScreensSkip;
    public TextMeshProUGUI playerGemsText;
    public TextMeshProUGUI addGemsText;
    AudioSource _AudioSource;
    AdManager AdManager;
   public GameObject FindInActiveObjectByName(string name)
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                   
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
   public GameObject[] FindInActiveObjectsByName(string name)
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    goList.Add(objs[i].gameObject);                    
                }
            }
        }
        return goList.ToArray();
        ;
    }

    public void DisplayImage()
    {
        adsPanel.SetActive(true);

        Time.timeScale = 0f;

    }
    public void HideImage()
    {
        adsPanel.SetActive(false);
        Time.timeScale = 1f;

        if(SceneManager.GetActiveScene().name == "p_Endless_mode_swipe" || SceneManager.GetActiveScene().name == "p_Endless_mode_draw")
        {

            GameObject.Find("GameManager").GetComponent<endlessMode>().RevivePlayer();

        }




    }
    void Start()
    {
        if (GameObject.Find("AudioManager"))
        {
            _AudioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();

        }
        AdManager = GetComponent<AdManager>();
        if (isEndless || isLevels)
        {
            //pause Button function
            GameObject pauseButton = FindInActiveObjectByName("Pause_Button");
            pauseButton.GetComponent<Button>().onClick.AddListener(OnPause);
            //next Button function
            GameObject nextButton = FindInActiveObjectByName("next_Button");
            nextButton.GetComponent<Button>().onClick.AddListener(OnNextLevel);
            //resume Buttons functions
            GameObject[] resumeButton = FindInActiveObjectsByName("resume_Button");
            foreach (GameObject item in resumeButton)
            {
                item.GetComponent<Button>().onClick.AddListener(OnUnpause);
            }
            //skipLevel Buttons functions
            GameObject[] skipLevelButton = FindInActiveObjectsByName("SkipLevelAd (1)");
            foreach (GameObject item in skipLevelButton)
            {
               // item.GetComponent<Button>().onClick.AddListener(AdManager.OnClickShowrewardedAd);
                item.GetComponent<Button>().onClick.AddListener(DisplayImage);


            }
            //restart Buttons functions
            GameObject[] restartButtons = FindInActiveObjectsByName("Restart_Button");
            foreach (GameObject item in restartButtons)
            {
                item.GetComponent<Button>().onClick.AddListener(OnRestartMayBeAd);
            }
            //levels Buttons functions
            GameObject[] levelsButtons = FindInActiveObjectsByName("levels_Button");
            foreach (GameObject item in levelsButtons)
            {
                item.GetComponent<Button>().onClick.AddListener(OnLevels);
            }
            //suit Buttons functions
            GameObject[] quitButtons = FindInActiveObjectsByName("quit_Button");
            foreach (GameObject item in quitButtons)
            {
                item.GetComponent<Button>().onClick.AddListener(OnQuit);
            }


            //get references to UI Screens
            pause_backGround = GameObject.Find("Canvas").transform.Find("pause_backGround").gameObject; 
            complete_backGround = GameObject.Find("Canvas").transform.Find("complete_backGround").gameObject; 
            fail_backGround = GameObject.Find("Canvas").transform.Find("fail_backGround").gameObject; 
            fail_backGround_tight = GameObject.Find("Canvas").transform.Find("fail_backGround_tight").gameObject; ;
            fail_backGround_spikes = GameObject.Find("Canvas").transform.Find("fail_backGround_spikes").gameObject; 
            fail_backGround_noText = GameObject.Find("Canvas").transform.Find("fail_backGround_noText").gameObject; 


            /*
            //disable all backgrounds
            pause_backGround.SetActive(false);
            complete_backGround.SetActive(false);
            fail_backGround.SetActive(false);         
            fail_backGround_tight.SetActive(false);
            fail_backGround_spikes.SetActive(false);
            fail_backGround_noText.SetActive(false);
            */
        }
        if (isLevels)
        {
            GameObject.Find("levelIndexText").GetComponent<TextMeshProUGUI>().text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
        }
    }


    void Update()
    {
        // __checkPointLevel = PlayerPrefs.GetInt("_checkPointLevel");
        if (SceneManager.GetActiveScene().name == "Skins" || SceneManager.GetActiveScene().name == "Trails")
        {
            playerGemsText = GameObject.Find("Canvas/PlayerGems (1)/Text (TMP)").GetComponent<TextMeshProUGUI>();
                playerGemsText.text = PlayerPrefs.GetInt("player_gems").ToString();
            addGemsText = GameObject.Find("Canvas/PlayerGems (1)/Text (TMP) (1)").GetComponent<TextMeshProUGUI>();
        }
       
    }

    public void PlayClickSound()
    {
        //play click sound
        if (_AudioSource !=null)
        {

        _AudioSource.Play();
        }
    }
    IEnumerator ShowMessage(string message, float delay)
    {
        Vector3 startYpos = addGemsText.GetComponent<RectTransform>().position;
        addGemsText.text = message;
        addGemsText.enabled = true;
        InvokeRepeating("TranslateMsg", 0f, 0.03f);

        yield return new WaitForSeconds(delay);
        CancelInvoke();

        addGemsText.enabled = false;
        addGemsText.GetComponent<RectTransform>().position = startYpos;

    }
    void TranslateMsg()
    {
        addGemsText.GetComponent<RectTransform>().position += new Vector3(0f, 2f, 0f);

    }



    public void RateUs()
    {
        Application.OpenURL("market://details?id=" + Application.productName);
    }
    //ADVERTISEMENTS
    public void AfterWatchAdReward()
    {
        PlayerPrefs.SetInt("player_gems", PlayerPrefs.GetInt("player_gems") + GemsToAddAfterAd);
        StartCoroutine(ShowMessage("+" + GemsToAddAfterAd, 1.5f));
    }
    /// ####################
 
    public void AfterWatchingSkipLevelAd()
    {
        //for levels mode
        //if ad loaded and shown succesfully =====>>> revive player & desactive Ad window 
        //30 sec Ad
        // increase checkPointLevel by one
        fail_backGround.SetActive(false);
        fail_backGround_tight.SetActive(false);
        fail_backGround_spikes.SetActive(false);
        fail_backGround_noText.SetActive(false);
        pause_backGround.SetActive(false);
        Time.timeScale = 1f;
        OnLevelComplete();
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().LevelComplete = true;
        }



    }


    void OnRestartMayBeAd()
    {
        PlayClickSound();
        // make counter ad increase
        PlayerPrefs.SetInt("AdCounter", PlayerPrefs.GetInt("AdCounter") + 1);
        //if ad loaded and shown succesfully =====>>> revive player & desactive Ad window 
        if (PlayerPrefs.GetInt("AdCounter") >= 5)
        {
            Time.timeScale = 1f;

            PlayerPrefs.SetInt("AdCounter", 0);
            //show some ad
            ///AdManager.OnClickShowinterstitialAd();
        }
        else
        {
            JustRestart();
        }

    }

    //OPEN SCENES :

    //Levels
    public void OnLevels()
    {
        PlayClickSound();
        SceneManager.LoadScene("Levels_List_1");
        Time.timeScale = 1f;
    }
    public void Onlevel14()
    {
        PlayerPrefs.SetInt("_checkPointLevel", 14);
        Debug.Log("ddfsd");

    }
    public void OnLevelsSecond()
    {
        PlayClickSound();
        SceneManager.LoadScene("Levels_List_2");
        Time.timeScale = 1f;
    }  
    public void OnLevelsThird()
    {
        PlayClickSound();
        SceneManager.LoadScene("Levels_List_3");
        Time.timeScale = 1f;
    } 

//Shop
    public void OpenTrails()
    {
        PlayClickSound();
        SceneManager.LoadScene("Trails");

    }
    public void OpenBalls()
    {
        PlayClickSound();
        SceneManager.LoadScene("Skins");
    }
    public void OpenShop()
    {
        PlayClickSound();
        SceneManager.LoadScene("Skins & Trails");
        Time.timeScale = 1f;
    }
    public void OpenInAppPurchase()
    {
        PlayClickSound();
        SceneManager.LoadScene("s_inAppPurchase");
        Time.timeScale = 1f;
    }
    public void OpenScoreBoard()
    {
        PlayClickSound();
        SceneManager.LoadScene("Scores");
        Time.timeScale = 1f;
    }
    public void add50GemsPurchase()
    {
        PlayClickSound();
        PlayerPrefs.SetInt("player_gems", PlayerPrefs.GetInt("player_gems") + 50);

    }
    public void add200GemsPurchase()
    {
        PlayClickSound();
        PlayerPrefs.SetInt("player_gems", PlayerPrefs.GetInt("player_gems") + 200);

    }
    public void add1000GemsPurchase()
    {
        PlayClickSound();
        PlayerPrefs.SetInt("player_gems", PlayerPrefs.GetInt("player_gems") + 1000);

    }
    public void add500GemsPurchase()
    {
        PlayClickSound();
        PlayerPrefs.SetInt("player_gems", PlayerPrefs.GetInt("player_gems") + 500);

    }
    public void unlock14()
    {
        PlayClickSound();
        PlayerPrefs.SetInt("player_gems", PlayerPrefs.GetInt("player_gems") + 500);

    }

    //Endless
    public void OpenEndless()
    {
        PlayClickSound();
        SceneManager.LoadScene("swipe & draw");
    }
    public void OpenEndlessSwipe()
    {
        PlayClickSound();
        SceneManager.LoadScene("p_Endless_mode_swipe");
    }
    public void OpenEndlessDraw()
    {
        PlayClickSound();
        SceneManager.LoadScene("p_Endless_mode_draw");
    }

//Other
    public void OnMenuPlay()
    {
        PlayClickSound();
        SceneManager.LoadScene(PlayerPrefs.GetInt("_checkPointLevel"));
    }
     void OnNextLevel()
    {
        PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
      
    public void JustRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   public  void OnQuit()
    {
        PlayClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("_Main_Menu");
       
    }


    //UI FUNCTIONS
     void OnPause()
    {

        if (GameObject.Find("Player"))
        {
           

                GameObject.Find("Player").GetComponent<Player>().ballSoundSource.volume =0f;
            
        }
        if (fail_backGround.activeSelf == false
            && fail_backGround_tight.activeSelf == false
            && fail_backGround_spikes.activeSelf == false
            && fail_backGround_noText.activeSelf == false
            && complete_backGround.activeSelf == false
            )
        {
            PlayClickSound();
            Time.timeScale = 0f;
            pause_backGround.SetActive(true);
            
        }




       


    }
    public void onLose()
    {
        fail_backGround.SetActive(true);
     
    }
    public void onLose_tight()
    {
        fail_backGround_tight.SetActive(true);
   
    }
    public void onLose_spikes()
    {
        fail_backGround_spikes.SetActive(true);


    }
    public void onLose_noText()
    {
        fail_backGround_noText.SetActive(true);

     
    }
    public void OnLevelComplete()
    {
        if (SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt("_checkPointLevel"))
        {
            // increase checkPointLevel by one
            int m_checkPointLevel = PlayerPrefs.GetInt("_checkPointLevel") + 13;
            PlayerPrefs.SetInt("_checkPointLevel", m_checkPointLevel);
        }
        
        //show level complete display
        complete_backGround.SetActive(true);
      

    }
    //##############
     void OnUnpause()
    {
        PlayClickSound();
        if (GameObject.Find("Player"))
        {
           
                GameObject.Find("Player").GetComponent<Player>().ballSoundSource.volume = 0.4f;
            
        }
        Time.timeScale = 1f;
        pause_backGround.SetActive(false);
    }
}
