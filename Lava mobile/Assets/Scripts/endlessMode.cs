using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;




public class endlessMode : MonoBehaviour
{
    private static endlessMode instance;
    public static endlessMode Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new endlessMode();
            }

            return instance;
        }
    }

    [SerializeField]
    private bool isDrawMode;
    [SerializeField]
    private bool isSwipeMode;
    private GameObject player;
    private GameManager Game_Manager;
    public int HighScore_draw;
    public int HighScore_swipe;
    public int score;
    public int minScoreToAdWin;
    public float TimeForEachPoint;
    private float Timer;
    public float speedToIncr;
    public float maxLineSpeed;
    [SerializeField]
    private float LineSpeed;
    private Image dischargingSprite;
    private GameObject ReviveWindow;
    private GameObject SkipButton;
    private TMP_Text scoreDisplay;
    [SerializeField]
    private float ForwardSpikeDis;
    public GameObject PlayToRevive;
    public GameObject LineCreatorToInstantiate;
    public int AdCounter;

    [SerializeField]
    private float DischargeSpeed;
    private bool ReviveWindowshown;
    private bool skipClicked;
    private GameObject[] lines;

    [HideInInspector]
    public bool hit_lava;
    [HideInInspector]
    public bool hit_destroyer;
    [HideInInspector]
    public bool hit_tight;
    [HideInInspector]
    public bool hit_spike;
    Vector2 respawwnPoint;

    private void Awake()
    {
        if (isDrawMode)
        {
            if (PlayerPrefs.HasKey("HighScore_draw"))
            {
                HighScore_draw = PlayerPrefs.GetInt("HighScore_draw");
            }
        }
        else if (isSwipeMode)
        {
            if (PlayerPrefs.HasKey("HighScore_swipe"))
            {
                HighScore_swipe = PlayerPrefs.GetInt("HighScore_swipe");
            }
        }
       
    }
    private void Start()
    {


        //get UI references
        scoreDisplay = GameObject.Find("Canvas").transform.Find("Score_text").GetComponent<TextMeshProUGUI>();
        //UI Revive => Endlesss
        ReviveWindow = GameObject.Find("Canvas").transform.Find("OtherChance").gameObject;
        SkipButton = GameObject.Find("Canvas").transform.Find("OtherChance/PassButton").gameObject;
        dischargingSprite = GameObject.Find("Canvas").transform.Find("OtherChance/chargingIMG (1)").GetComponent<Image>();

        SkipButton.GetComponent<Button>().onClick.AddListener(OnSkipPressed);

        ReviveWindowshown = false;
        skipClicked = false;
        player = GameObject.FindGameObjectWithTag("Player");
        Game_Manager = FindObjectOfType<GameManager>();
        Timer = TimeForEachPoint;
    }

    private void OnSkipPressed()
    {
        skipClicked = true;
    }

    
    void ShowLoseScreens(bool whatToHit, string void_toCall)
    {
        if (whatToHit)
        {
           
            //see if you can show Ad window
            if (Game_Manager.isEndless &&
            score >= minScoreToAdWin &&
            !ReviveWindowshown)
            {
                //show Ad window
                ReviveWindow.SetActive(true);
                if (ReviveWindow.activeSelf)
                {
                    dischargingSprite.fillAmount -= DischargeSpeed * Time.deltaTime;
                    if (dischargingSprite.fillAmount <= .6f)
                    {
                        SkipButton.SetActive(true);
                        
                        
                    }
                    if (dischargingSprite.fillAmount <= 0f || skipClicked)
                    {
                        //hide Revive window
                        ReviveWindow.SetActive(false);
                        Game_Manager.Invoke(void_toCall, Game_Manager.timeToShowScreensSkip);
                        scoreDisplay.enabled = false;
                    }
                }
            }
              else
                {
                    Game_Manager.Invoke(void_toCall, Game_Manager.timeToShowScreens);
                scoreDisplay.enabled = false;
            }
        }
    }
    public void RevivePlayer()
    {
            ReviveWindow.SetActive(false);

            GameObject[] spikeTypes = gameObject.GetComponent<GameManager>().FindInActiveObjectsByName("Spike_type");
            foreach (GameObject item in spikeTypes)
            {
                item.transform.position += new Vector3(ForwardSpikeDis, 0f, 0f);
            }
        if (isSwipeMode)
        {
             respawwnPoint = new Vector2(Camera.main.transform.position.x - 5f, 0f);

        } else if (isDrawMode)
        {
             respawwnPoint = new Vector2(Camera.main.transform.position.x - 5f, 2.7f);

        }
            GameObject _playerToRevive = Instantiate(PlayToRevive, respawwnPoint, Quaternion.identity) as GameObject;
            _playerToRevive.name = "_player";
        if (isSwipeMode)
        {
          GameObject newLineCreator =  Instantiate(LineCreatorToInstantiate) as GameObject;
            newLineCreator.GetComponent<LineCreator>().SwipeControl = true;
            newLineCreator.GetComponent<LineCreator>().DrawControl = false;
            newLineCreator.GetComponent<LineCreator>().LineFollow = false;
            newLineCreator.GetComponent<LineCreator>().LineFree = true;
            
        }
        else if (isDrawMode)
        {
            GameObject newLineCreator = Instantiate(LineCreatorToInstantiate) as GameObject;
            newLineCreator.GetComponent<LineCreator>().SwipeControl = false;
            newLineCreator.GetComponent<LineCreator>().DrawControl = true;
            newLineCreator.GetComponent<LineCreator>().LineFollow = false;
            newLineCreator.GetComponent<LineCreator>().LineFree = false;
        }

        lines = GameObject.FindGameObjectsWithTag("line");
            foreach (GameObject line in lines)
            {
                Destroy(line.gameObject);
            }

        Camera.main.GetComponent<FollowPlayer>().shakeDuration = 1f;
        Camera.main.GetComponent<FollowPlayer>().shakeMagnitude = 1f;
            hit_lava = false;
            hit_destroyer = false;
            hit_tight = false;
            hit_spike = false;
            //shoud be after the above true statements
            ReviveWindowshown = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        AdCounter = PlayerPrefs.GetInt("AdCounter");
        ShowLoseScreens(hit_lava, "onLose");
        ShowLoseScreens(hit_spike, "onLose_spikes");
        ShowLoseScreens(hit_tight, "onLose_tight");
        ShowLoseScreens(hit_destroyer, "onLose_noText");

      
        if (isSwipeMode)
        {
            //Update Upper SCORE text
            if (scoreDisplay.enabled == true)
            {
                scoreDisplay.text = score.ToString();

            }
            //save the high score if hitted
            if (score > HighScore_swipe)
            {
                scoreDisplay.color = new Color32(100, 255, 83, 230);
                HighScore_swipe = score;
                PlayerPrefs.SetInt("HighScore_swipe", HighScore_swipe);
            }
        }  
        else if (isDrawMode)
        {
            //Update Upper SCORE text
            if (scoreDisplay.enabled == true)
            {
                scoreDisplay.text = score.ToString();

            }
            //save the high score if hitted
            if (score > HighScore_draw)
            {
                scoreDisplay.color = new Color32(100, 255, 83, 230);
                HighScore_draw = score;
                PlayerPrefs.SetInt("HighScore_draw", HighScore_draw);
            }
        }
      
        
        
        // increase score & line speed every time
        //when player is alive
        if (player != null)
        {

            if (Timer < 0f)
            {
                score++;
                Timer = TimeForEachPoint;
                if (GameObject.FindGameObjectWithTag("line"))
                {

                    //get reference to lines
                    lines = GameObject.FindGameObjectsWithTag("line");
                    //Reverse the speed for each line  
                    foreach (GameObject line in lines)
                    {

                        if (LineSpeed <= maxLineSpeed)
                        {
                            LineSpeed += speedToIncr;
                        }

                        line.GetComponent<SurfaceEffector2D>().speed = LineSpeed;

                    }
                }
                else
                {
                }
            }
            else
            {
                Timer -= Time.deltaTime;
            }
        }
        //when player dies
        else
        {
            if (lines != null)
            {
                foreach (GameObject line in lines)
                {
                    //restore line speed when player dies
                    LineSpeed = line.GetComponent<SurfaceEffector2D>().speed;
                }
            }

            var _scoreDisplays = GameObject.FindGameObjectsWithTag("Score");
            foreach (var item in _scoreDisplays)
            {
                item.GetComponent<TextMeshProUGUI>().text = score.ToString();
            }
            if (isSwipeMode)
            {
                var _HighScoreDisplays = GameObject.FindGameObjectsWithTag("HighScore");
                foreach (var item in _HighScoreDisplays)
                {
                    item.GetComponent<TextMeshProUGUI>().text = HighScore_swipe.ToString();
                }
            }
            else if (isDrawMode)
            {
                var _HighScoreDisplays = GameObject.FindGameObjectsWithTag("HighScore");
                foreach (var item in _HighScoreDisplays)
                {
                    item.GetComponent<TextMeshProUGUI>().text = HighScore_draw.ToString();
                }
            }
           
        }
        /*
            //update score  text
            if (GameObject.FindGameObjectWithTag("Score") != null)
            {
                scoreDisplay = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
                scoreDisplay.text = score.ToString();

            }
            scoreDisplay.text = score.ToString();
            //update Highscore  text
            if (GameObject.FindGameObjectWithTag("HighScore") != null)
            {
                HighScoreDisplay = GameObject.FindGameObjectWithTag("HighScore").GetComponent<TextMeshProUGUI>();
                HighScoreDisplay.text = HighScore.ToString();

            }
        */
        }

    }




