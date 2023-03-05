using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	private Transform player;
	//public float minYpos;
	//public float maxYpos;
	public float smoothTime;
	private Vector3 velocity = Vector3.zero;
	// Desired duration of the shake effect
	public float shakeDuration = 0f;
	// A measure of magnitude for the shake. Tweak based on your preference
	public float shakeMagnitude = 0.7f;
	// A measure of how quickly the shake effect should evaporate
	private float dampingSpeed = 1.0f;
	// The initial position of the GameObject
	Vector3 CamPos;


	 void Start()
	{
	}
	void Update()
	{
        if (GameObject.FindGameObjectWithTag("Player"))
        {
			player = GameObject.FindGameObjectWithTag("Player").transform;

        }
        

		if (player != null)
		{
			Vector3 desired_CamPos = new Vector3(player.position.x+5f, player.position.y, -10f);
			//follow player smoothly
			//CamPos = Vector3.SmoothDamp(transform.position, desired_CamPos, ref velocity, smoothTime);
			CamPos = Vector3.Lerp(transform.position, desired_CamPos, smoothTime);
			transform.position = CamPos;
		}
		//when player died
		else
		{
			//shake the camera
			Shake();
		}
	}

	void Shake()
	{
		if (shakeDuration > 0)
		{
			transform.localPosition = CamPos + Random.insideUnitSphere * shakeMagnitude;
			if (shakeMagnitude > 0f)
			{
				shakeMagnitude -= Time.deltaTime;
			}
			shakeDuration -= Time.deltaTime * dampingSpeed;
		}
		else
		{
			shakeDuration = 0f;
			transform.localPosition = CamPos;
		}
	}
}
