using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KarakterHaraket : MonoBehaviour
{
    public GameObject replayBtn;
    public GameObject gameOverTxt;
    public GameObject finalPointObj;
    public TMP_Text finalPointText;
    Rigidbody2D rb;
    public TMP_Text scoreValueText;
    public TMP_Text gamePointText;
    public int gamePoint;
    private float yatayHaraket;
    public float haraketHizi;
    float screenWidth = Screen.width;
    public UnityEngine.Rendering.Universal.Light2D light2D;
    public float pointDecreasePerSecond = 1f;
    Touch _touch;
    float lastTapTime = 0;
    float doubleTabThreshold = 0.3f;
    public float jumpSpeed = 8f;
    public float scoreValue;
    private float decimalPoint;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void jumpToDoubleClick()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTapTime <= doubleTabThreshold)
                {
                    lastTapTime = 0;
                    Debug.Log("cift tiklandi");
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                }
                else
                {
                    lastTapTime = Time.time;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreValue > 0)
        {
            scoreValueText.text = ((int)scoreValue).ToString();
            scoreValue -= pointDecreasePerSecond * Time.deltaTime;
            
            gamePointText.text= ((int)gamePoint).ToString();

            decimalPoint+=  (Time.deltaTime);
            gamePoint=Mathf.RoundToInt(decimalPoint)*5;
            LightControl(scoreValue);
        }
        else{

            finalPointText.text=gamePointText.text;
       
            replayBtn.SetActive(true);
            finalPointObj.SetActive(true);
            gameOverTxt.SetActive(true);
           

        }
        //yatayHaraket = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(yatayHaraket * haraketHizi * Time.deltaTime, rb.velocity.y);
        //Debug.Log("yatayHaraket --- "+yatayHaraket);

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            //Debug.Log("touch --- " + _touch);

            if (_touch.position.x > (screenWidth / 2))
            {
                //The User has touched on the right side of the screen
                rb.velocity = new Vector2(rb.velocity.x + 1 * haraketHizi * Time.deltaTime, rb.velocity.y);

            }
            else
            {
                //The user hase touched the left side of the screen 

                rb.velocity = new Vector2(rb.velocity.x + (-1 * haraketHizi * Time.deltaTime), rb.velocity.y);
            }
        }

        jumpToDoubleClick();



    }

    private void LightControl(float _outerRadius)
    {
        //  light2D = karakter.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2D.pointLightOuterRadius = _outerRadius / 2;
        //  light2D.outerRadius=_outerRadius;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {





        if (other.gameObject.tag == "Fener")
        {
            if (scoreValue + 11 <= 31)
            {
                scoreValue = int.Parse(scoreValueText.text);
                scoreValue += 11;
                scoreValueText.text = scoreValue.ToString();
            }
            else
            {
                scoreValue = 31;
                scoreValueText.text = scoreValue.ToString();
            }

            Destroy(other.gameObject);
        }
    }
}
