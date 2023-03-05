using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Automatic line creator was made to auto move the player in mainManu 
 * 
 */
public class LineCreator : MonoBehaviour {

	public GameObject linePrefab;

	Transform player;
	Line activeLine;

	public bool LineFree;
	public bool LineFollow;
	public bool SwipeControl;
	public bool DrawControl;
	private bool once;
	[SerializeField]
	private float x_start_offset;
	[SerializeField]
	private float y_start_offset;
	[SerializeField]
	private float max_Y_pos;
	[SerializeField]
	private float min_Y_pos;	
	[Header("Line Settings")]
	[SerializeField]
	private float x_offset;
	[SerializeField]
	private float y_offset;
	[SerializeField]
	private float Y_speed_setter_mobile;
	[SerializeField]
	private float Y_speed_setter_mob_line_free;
	[SerializeField]
	private float Y_speed_setter_win;
	//public float delta;
	[SerializeField]
	public float threshold;
	private Transform XY_Pos;
	private GameObject XY_Pos_Ps;
	//[Header("Touch Settings")]
	//[SerializeField]
	//private float minDisToDrawOn_Y;
	//[SerializeField]
	//private float maxDisOnXaxis;
	//private float AxisRawVertical;
	//public float touch_deltaPosition_x ;
	//public float touch_deltaPosition_y ;
	private Touch touch;
	//private Vector3 fp;   //First touch position
	//private Vector3 lp;   //Last touch position
	//#######################################################################################################"
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		XY_Pos = GameObject.Find("_player/XY_Pos").transform;
		XY_Pos_Ps = GameObject.Find("_player/XY_Pos/CFX_Magical_Source");
	}
	
	void FixedUpdate()
	{



		//draw line manually on y axis
		if (player != null)
		{
			if (SwipeControl)
			{
				if (once == false)
				{
					//update child y_pos & x_pos at the start
					//XY_Pos.localPosition = new Vector2(x_offset, y_offset);
					once = true;
					GameObject _lineGO = Instantiate(linePrefab);
					activeLine = _lineGO.GetComponent<Line>();

					y_offset = y_start_offset;
					Vector2 Autopos = new Vector2(player.position.x + x_start_offset, player.position.y + y_start_offset);
					activeLine.UpdateLine(Autopos);

				}

				//if distance btwn player and XY_Pos <= x_offset On Horizontal then update the line and control XY_Pos.position.y else stay freezed
				// ==============>>>>> IF X_OFFSET CHANGED YOU MUST ALSO CHANGE XY_POS 
				if ((Mathf.Abs(player.position.x - XY_Pos.position.x) <= x_offset))
				{
					//To test game on Pc : remove later
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
					//AxisRawVertical = Input.GetAxisRaw("Vertical");
					y_offset += Input.GetAxisRaw("Vertical") *Time.deltaTime* Y_speed_setter_win;

#endif

#if UNITY_ANDROID

					if (Input.touchCount > 0) // user is touching the screen with a single touch
					{
						//Touch touch = Input.GetTouch(Input.touchCount-1); // get the touch
						touch = Input.GetTouch(0); // get the touch
												   //y_offset
						if (LineFollow)
						{

							y_offset += touch.deltaPosition.y * Time.deltaTime * Y_speed_setter_mobile;
						}
						else if (LineFree)
						{
							/*
							if (touch.phase == TouchPhase.Began) //check for the first touch
							{
								fp = touch.position;
								lp = touch.position;

							}
							else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
							{


								lp = touch.position;

								if (lp.y < fp.y
									&& Mathf.Abs(lp.y - fp.y) >= minDisToDrawOn_Y )
								{

								}
								//move finger up
								else if (lp.y > fp.y
										 && Mathf.Abs(lp.y - fp.y) >= minDisToDrawOn_Y)								
								{

								}
							}
							else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
							{
								lp = touch.position;

							}
							*/
							if (touch.deltaPosition.y > threshold)
							{
								Y_speed_setter_mob_line_free = 1f;

							}
							else if (touch.deltaPosition.y < -threshold)
							{
								Y_speed_setter_mob_line_free = -1f;

							}
							
							y_offset += 5f * Time.deltaTime * Y_speed_setter_mob_line_free;
						}
						/*
						
						if (touch.phase == TouchPhase.Began) //check for the first touch
						{
							fp = touch.position;
							lp = touch.position;

						}
						else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
						{

						
							lp = touch.position;
							//move finger down
							if (lp.y < fp.y 
								&& Mathf.Abs(lp.y - fp.y) >= minDisToDrawOn_Y 
								&& Mathf.Abs(lp.x - fp.x) <= maxDisOnXaxis)
							{
								//AxisRawVertical = -1f;
								y_offset =  Mathf.Abs( touch.deltaPosition.y) * Y_speed_setter;
							}
							//move finger up
							else if (lp.y > fp.y 
									 && Mathf.Abs(lp.y - fp.y) >= minDisToDrawOn_Y 
									 && Mathf.Abs(lp.x - fp.x) <= maxDisOnXaxis)
							{
								//AxisRawVertical = 1f;
								y_offset =   Mathf.Abs(touch.deltaPosition.y) * Y_speed_setter;

							}
						}
					    	else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
						{
							lp = touch.position;
							//to make control harder dont write this 
							AxisRawVertical = 0f;
						}

						*/
					}
							else
							{
								Y_speed_setter_mob_line_free = 0f;

							}
				
#endif
					/*
					//y_offset limits
					if (y_offset >= max_Y_pos)
					{
						y_offset = max_Y_pos;
					}
					else if (y_offset <= min_Y_pos)
					{
						y_offset = min_Y_pos;
					}
					*/


					//play XY_Pos_Particle_system when change y_offset
					/*
                    if (Mathf.Abs(AxisRawVertical) == 1)
                    {
						XY_Pos_Ps.SetActive(true);
					}
					else
                    {
						XY_Pos_Ps.SetActive(false);
					}
					*/
					//update global XY_Pos : x_pos & y_pos
					XY_Pos.position = new Vector2(player.position.x + x_offset, player.position.y + y_offset);
				}


				//UpdateLine with player position reference
				if (activeLine != null)
				{
                    if (LineFree)
                    {
						Vector2 Autopos = new Vector2(XY_Pos.position.x, y_offset);
						activeLine.UpdateLine(Autopos);
					} 
					if (LineFollow)
                    {
						Vector2 Autopos = new Vector2(XY_Pos.position.x, XY_Pos.position.y);
						activeLine.UpdateLine(Autopos);
					}
				
				}
			}
		//draw line manually
		 if (DrawControl)
			{
				/*
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

				if (Input.GetMouseButtonDown(0))
				{

					GameObject lineGO = Instantiate(linePrefab);
					activeLine = lineGO.GetComponent<Line>();
				}
				else if (Input.GetMouseButtonUp(0))
				{

					activeLine = null;
				}
				if (activeLine != null)
				{

					Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					activeLine.UpdateLine(touchPos);
				}
#endif
				*/
#if UNITY_ANDROID
				if (Input.touchCount > 0) // user is touching the screen with a single touch
				{
					 touch = Input.GetTouch(0);
					if (touch.phase == TouchPhase.Began)
					{
						GameObject lineGO = Instantiate(linePrefab);
						activeLine = lineGO.GetComponent<Line>();
					}
					else if (touch.phase == TouchPhase.Ended)
					{
						activeLine = null;
					}
					if (activeLine != null)
					{

						Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
						activeLine.UpdateLine(touchPos);
					}
				}
#endif

			}

		}
		}
	}


