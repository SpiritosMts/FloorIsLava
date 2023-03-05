using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSound : MonoBehaviour
{
   public AudioSource _ballSoundSource;
    public float speed;
        void Start()
    {
        _ballSoundSource = transform.parent.GetComponent<Player>().ballSoundSource;
    }

    private void FixedUpdate()
    {
        speed = transform.parent.GetComponent<Rigidbody2D>().velocity.magnitude;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_ballSoundSource.isPlaying == false
         && speed >= 0.1f
         && collision.gameObject.CompareTag("line"))
        {
            _ballSoundSource.Play();
        }
        else if (_ballSoundSource.isPlaying == true
                 && speed < 0.1f
                 && collision.gameObject.CompareTag("line"))
        {
            _ballSoundSource.Pause();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        float coolDown =1f;
        if (coolDown <= 0f)
        {
            if (_ballSoundSource.isPlaying == true
           && collision.gameObject.CompareTag("line"))
            {
                _ballSoundSource.Pause();
            }
        }
        else
        {
            coolDown -= Time.deltaTime;
        }

        
    }
  
}
