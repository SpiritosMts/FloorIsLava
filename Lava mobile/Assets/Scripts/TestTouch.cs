using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour
{
    public Texture areaTexture;
    public Texture touchTexture;
    public Vector2 joystickPosition = new Vector2(135f, 135f);
    public Vector2 speed = new Vector2(2, 100);
    public float zoneRadius = 100f;
    public float touchSize = 30;
    public float deadZone = 20;
    public float touchSizeCoef = 0;
    protected Vector2 joystickAxis;
    protected Vector2 joystickValue;
    public Vector2 joyTouch;
    private Vector2 _joystickCenter;
    [SerializeField]
    private Vector2 _smoothing = new Vector2(20f, 20f);
    public Vector2 Smoothing
    {
        get
        {
            return this._smoothing;
        }
        set
        {
            _smoothing = value;
            if (_smoothing.x < 0.1f)
            {
                _smoothing.x = 0.1f;
            }
            if (_smoothing.y < 0.1)
            {
                _smoothing.y = 0.1f;
            }
        }
    }
    private int _joystickIndex = -1;
    private bool _enaReset;
    private bool _enaZoom;

    void Start()
    {
        _joystickCenter = joystickPosition;
        _enaReset = false;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    if (_joystickIndex == touch.fingerId)
                    {
                        _joystickIndex = -1;
                        _enaReset = true;
                    }
                }

                if (_joystickIndex == touch.fingerId)
                {
                    OnTouchDown(touch.position);
                }
                if (touch.phase == TouchPhase.Began)
                {
                    if (((Vector2)touch.position - _joystickCenter).sqrMagnitude < Mathf.Pow((zoneRadius + touchSizeCoef / 2), 2))
                    {
                        _joystickIndex = touch.fingerId;
                    }
                }
            }

            UpdateJoystick();
            if (_enaReset)
            {
                ResetJoystick();
            }
        }
        else
        {
            if (Input.GetButtonUp("Fire1"))
            {
                _joystickIndex = -1;
                _enaReset = true;
            }
            if (_joystickIndex == 1)
            {
                OnTouchDown(Input.mousePosition);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (((Vector2)Input.mousePosition - _joystickCenter).sqrMagnitude < Mathf.Pow((zoneRadius + touchSizeCoef / 2), 2))
                {
                    _joystickIndex = 1;

                }

            }
            if (_enaReset)
            {
                ResetJoystick();
            }

            UpdateJoystick();

        }

    }




    private void UpdateJoystick()
    {
        if (joyTouch.sqrMagnitude > deadZone * deadZone)
        {

            joystickAxis = Vector2.zero;
            if (Mathf.Abs(joyTouch.x) > deadZone)
            {
                joystickAxis = new Vector2((joyTouch.x - (deadZone * Mathf.Sign(joyTouch.x))) / (zoneRadius - touchSizeCoef - deadZone), joystickAxis.y);

            }
            else
            {
                joystickAxis = new Vector2(joyTouch.x / (zoneRadius - touchSizeCoef), joystickAxis.y);

            }
            if (Mathf.Abs(joyTouch.y) > deadZone)
            {
                joystickAxis = new Vector2(joystickAxis.x, (joyTouch.y - (deadZone * Mathf.Sign(joyTouch.y))) / (zoneRadius - touchSizeCoef - deadZone));
            }
            else
            {
                joystickAxis = new Vector2(joystickAxis.x, joyTouch.y / (zoneRadius - touchSizeCoef));
            }

        }
        else
        {
            joystickAxis = new Vector2(0, 0);
        }
        Vector2 realvalue = new Vector2(speed.x * joystickAxis.x, speed.y * joystickAxis.y);
        joystickValue = realvalue;
        print(realvalue);

    }

    void OnTouchDown(Vector2 position)
    {
        joyTouch = new Vector2(position.x, position.y) - _joystickCenter;
        if ((joyTouch / (zoneRadius - touchSizeCoef)).sqrMagnitude > 1)
        {
            joyTouch.Normalize();
            joyTouch *= zoneRadius - touchSizeCoef;
        }
        //print(joyTouch);
    }


    private void ResetJoystick()
    {
        if (joyTouch.sqrMagnitude > 0.1)
        {
            joyTouch = new Vector2(joyTouch.x - joyTouch.x * _smoothing.x * Time.deltaTime, joyTouch.y - joyTouch.y * _smoothing.y * Time.deltaTime);
        }
        else
        {
            joyTouch = Vector2.zero;
            _enaReset = false;
        }
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(_joystickCenter.x - zoneRadius, Screen.height - _joystickCenter.y - zoneRadius, zoneRadius * 2, zoneRadius * 2), areaTexture, ScaleMode.ScaleToFit, true);
        GUI.DrawTexture(new Rect(_joystickCenter.x + (joyTouch.x - touchSize), Screen.height - _joystickCenter.y - (joyTouch.y + touchSize), touchSize * 2, touchSize * 2), touchTexture, ScaleMode.ScaleToFit, true);
    }
}
