using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

   // [HideInInspector]
    public GameManager Game_Manager;

   // [HideInInspector]
    public endlessMode Endless_Mode;
    // [HideInInspector]
    public playerBugDestroyer playerBugDestroyer;
    private Rigidbody2D rb;
    public GameObject[] deathParticles;
    public GameObject completeLevelPs;
    [HideInInspector]
    public bool LevelComplete;
    public List<Sprite> Skins;
    public List<GameObject> Trails;
    //private bool isReverser;
    private GameObject[] lines;
    GameObject latestLineCreated;
   public AudioSource ballSoundSource;
     AudioSource DeathSoundSource;
    AudioSource starSoundSource;
   // public Transform detector;
    public float maxDisToPlayRoll;
   public float speed;
   public float maxSpeed;
   public float minPitch;
   public float maxPitch;
    //#######################################################################################################"
     void Start()
    {
        //set _player z dimension to 0
        transform.parent.transform.position =new Vector3 (transform.parent.transform.position.x, transform.parent.transform.position.y,0f);
        LevelComplete = false;
        //set the skin to the player at the start
        GetComponent<SpriteRenderer>().sprite = Skins[PlayerPrefs.GetInt("current_skin") - 1];
        //set the trail to the player at the start
        Instantiate(Trails[PlayerPrefs.GetInt("current_trail") - 1], GameObject.Find("_player").transform);
 
        //get reference to rigidBody
        rb = GetComponent<Rigidbody2D>();
        //get reference to game managers
        Game_Manager = FindObjectOfType<GameManager>();
        //get reference to endlessMode
        Endless_Mode = FindObjectOfType<GameManager>().GetComponent<endlessMode>() ;
        //get reference to playerBugDestroyer
        playerBugDestroyer = transform.Find("playerBugDestroyer").GetComponent<playerBugDestroyer>();
        AudioSource[] AudioSources = GetComponents<AudioSource>();
        //get reference to audiosources
       
           
                ballSoundSource = AudioSources[0];
            
          
                DeathSoundSource = AudioSources[1];



    }
    
    private void FixedUpdate()
    {
        speed = rb.velocity.magnitude;

       float pitchModifier = Mathf.Abs(maxPitch - minPitch);
       // ballSoundSource.pitch = minPitch + (speed / maxSpeed) * pitchModifier;
        ballSoundSource.pitch = maxPitch - (speed / maxSpeed) * pitchModifier;
        if (ballSoundSource.pitch<= minPitch)
        {
            ballSoundSource.pitch = minPitch;
        }
        else if(ballSoundSource.pitch >= maxPitch)
        {
            ballSoundSource.pitch = maxPitch;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("line"))
        {
            latestLineCreated = collision.gameObject;
        }
    }
    void Update()
    {
        if (latestLineCreated != null)
        {
       Vector3 closestPoint = latestLineCreated.GetComponent<EdgeCollider2D>().ClosestPoint(transform.position);
            float _distance = Vector2.Distance(transform.position, closestPoint);

        //If something was hit.

        if (_distance <= maxDisToPlayRoll)
        {
            if (ballSoundSource.isPlaying == false && speed >= 1.5f)
            {
                ballSoundSource.Play();
            }
            else if (ballSoundSource.isPlaying == true && speed < 1.5f)
            {
                ballSoundSource.Pause();
            }
        }
        else
        {
            if (ballSoundSource.isPlaying == true)
            {
                ballSoundSource.Pause();
            }
        }
        }
        //Raycast2D tests
        /*
       //Cast a ray in the direction specified in the inspector.
       RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down);

       //If something was hit.
       if (hit.collider != null)
       {
           Gizmos.DrawWireSphere(hit.collider.transform.position, 1f);

           //If the object hit is less than or equal to 6 units away from this object.
           if (hit.distance <= 6.0f)
           {
               return;     
                   }
       }
       
        //  closestPoint = GameObject.FindGameObjectWithTag("line").GetComponent<EdgeCollider2D>().ClosestPoint(transform.position);

        if (hit.collider != null)
        {
            // Draws a line from the normal of the object that you clicked
            Debug.DrawLine(transform.position, hit.collider.transform.position, Color.yellow, 10.0f);
        }
        */
        /*
        //enable gravity
        if (true || Input.touchCount > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) ==1)
        {

            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        */
        //fix bug
        if (playerBugDestroyer.bug == true)
        {
            DeathSoundSource.Play();
            Endless_Mode.hit_tight = true;
            if (!Game_Manager.isEndless)
            {
                if (!LevelComplete)
                {
                    Game_Manager.Invoke("onLose_tight", Game_Manager.timeToShowScreens);
                }
            }

            //Game_Manager.onLose_tight();
            Destroy(gameObject.transform.parent.gameObject);
            //vibrate device
            Handheld.Vibrate();
            Instantiate(deathParticles[2], transform.position, Quaternion.identity);

        }
        if(transform.position.x > 20000 || transform.position.x < -20000 || transform.position.y > 20000 || transform.position.y < -20000 )
        {
            Destroy(gameObject);
        }
      
    }

 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("reverser"))
        {
            ///if you not gonna draw lines after cilliding with reverser then put it in here
            //get reference to lines
            lines = GameObject.FindGameObjectsWithTag("line");
            //Reverse the speed for each line  
            foreach (GameObject line in lines)
            {
                line.GetComponent<SurfaceEffector2D>().speed *= -1;
            }
            ///
            //isReverser = !isReverser;
            Destroy(collision.gameObject);


        }
        if (collision.gameObject.CompareTag("lava"))
        {
            DeathSoundSource.Play();
            Endless_Mode.hit_lava = true;
            if (!Game_Manager.isEndless)
            {
                if (!LevelComplete)
                {
                    Game_Manager.Invoke("onLose", Game_Manager.timeToShowScreens);
                }
            }
            //vibrate device
            Handheld.Vibrate();
            Destroy(gameObject.transform.parent.gameObject);
            Vector2 dPpos = new Vector2(transform.position.x, transform.position.y);
            foreach (GameObject dP in deathParticles)
            {
                Instantiate(dP, dPpos, Quaternion.identity);
            }
            //play losing sound effect
        }
        // when level complete
        else if (collision.gameObject.CompareTag("star"))
        {
            //vibrate device
            Handheld.Vibrate();
            Destroy(collision.gameObject);
            Game_Manager.Invoke("OnLevelComplete", Game_Manager.timeToShowScreens);



            //particle system for the star
            Instantiate(completeLevelPs, collision.transform.position, Quaternion.identity);
            LevelComplete = true;
        }
        else if (collision.gameObject.CompareTag("spike"))
        {
            DeathSoundSource.Play();
            Endless_Mode.hit_spike = true;
            if (!Game_Manager.isEndless)
            {
                if (!LevelComplete)
                {
                    Game_Manager.Invoke("onLose_spikes", Game_Manager.timeToShowScreens);
                }
            }
            //Game_Manager.onLose_spikes();
            Destroy(gameObject.transform.parent.gameObject);
            //vibrate device
            Handheld.Vibrate();
            Instantiate(deathParticles[2], transform.position, Quaternion.identity);

            //play losing sound effect
        }
        else if (collision.gameObject.CompareTag("destroyer"))
        {
            DeathSoundSource.Play();
            Endless_Mode.hit_destroyer = true;
            if (!Game_Manager.isEndless)
            {
                if (!LevelComplete)
                {
                    Game_Manager.Invoke("onLose_noText", Game_Manager.timeToShowScreens);
                }
            }

            Destroy(gameObject.transform.parent.gameObject);
            Instantiate(deathParticles[2], transform.position, Quaternion.identity);

        }
    }

}
