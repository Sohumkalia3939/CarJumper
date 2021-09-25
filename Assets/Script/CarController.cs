using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick; //line added
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;
    [SerializeField] private Joystick joystick; //line added
    public float m_Thrust;
    public Rigidbody m_Rigidbody;
  public bool isGround;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;
    public Text Score;
    public int scorenum=0;
    public Transform Player;
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
        scorenum = 0;
    }
    private void FixedUpdate()
    {
         TouchInput.ProcessTouches();

         if (TouchInput.Tap())
         {
             Jump();
         }
        if (Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(1);


            Jump();
        }
     
            GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = joystick.Horizontal();
        verticalInput = joystick.Vertical();
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        brakeForce = isBreaking ? 3000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }
    public void Jump()
    {
        if (isGround == true)
        {


            m_Rigidbody.AddForce(transform.up * m_Thrust);
            isGround = false;
        }
      

    }
    void Update()
    {

              Score.text = "Score:" + Player.transform.position.z.ToString("0");
       // Score.text = "Score:" + scorenum;
        Debug.Log(Player.transform.position.y);
        if (Player.transform.position.y<=-15)
        {
            FindObjectOfType<ShowGameOver>().gameover();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag== "Ground")
        {
            isGround = true;
            scorenum ++;
            // Destroy(collision.collider.gameObject);
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(7);

                Destroy(collision.collider.gameObject);

            }
        }
        else
        {
            isGround = false;
        }
    }
   
}
