using UnityEngine;
using System.Collections;

public class Driving : MonoBehaviour {
    public WheelCollider wheelFRColider;
    public WheelCollider wheelFLColider;
    public WheelCollider wheelRRColider;
    public WheelCollider wheelRLColider;

    public Transform wheelFRModel;
    public Transform wheelFLModel;
    public Transform wheelRRModel;
    public Transform wheelRLModel;

    public Transform FLDiscBrake;
    public Transform FRDiscBrake;

    public float motorTorque = 50;
    public float steerAngle = 10;

    public Transform centerOfMass;//重心点
    public float maxSpeed = 140;
    public float minSpeed = 30;

    private bool isBreaking = false;//刹车
    public float brakeTorque = 400;//刹车的力

    private float currentSpeed;
    public AudioSource engineSource;
    public AudioSource skidSource;
    public int[] speedArray;

    public ParticleEmitter leftEmitter;
    public ParticleEmitter rightEmitter;

    //灯光
    public GameObject  Light;
   
//车子的划痕
    public GameObject skidmark;
    //车碰撞到栏杆的声音
    public AudioSource crashSource;
	// Use this for initialization
	void Start () {
        rigidbody.centerOfMass = centerOfMass.localPosition;
	}
	
	// Update is called once per frame
	void Update () {


        float s = wheelFLColider.rpm * (wheelFLColider.radius * 2 * Mathf.PI) * 60 / 1000;//千米每小时
        currentSpeed = s;
        if ((s <0 && Input.GetAxis("Vertical") > 0) || (s >0 && Input.GetAxis("Vertical") < 0))
        {
            //刹车  前进的时候按后退  或者后退的时候按前进
            isBreaking = true;
        }
        else {
            isBreaking = false;
        }

        if ((s > maxSpeed && Input.GetAxis("Vertical") > 0) || (s < -minSpeed && Input.GetAxis("Vertical") < 0))
        {
            wheelFRColider.motorTorque = 0;
            wheelFLColider.motorTorque = 0;
        }
        else
        {
            //motorTorque向前的力
            wheelFRColider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelFLColider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        }

        if (isBreaking)
        {
            //刹车
            wheelFRColider.motorTorque = 0;
            wheelFLColider.motorTorque = 0;

            wheelFRColider.brakeTorque =brakeTorque;
            wheelFLColider.brakeTorque = brakeTorque;
        }
        else {
            wheelFRColider.brakeTorque = 0;
            wheelFLColider.brakeTorque = 0;
        }
        //steerAngle角度控制
        wheelFRColider.steerAngle = Input.GetAxis("Horizontal") * steerAngle;
        wheelFLColider.steerAngle = Input.GetAxis("Horizontal") * steerAngle;
        RotateWheel(); SteerWheel(); EngineSound(); ControlRight();
	}
    void FixedUpdate()
    {
        SkidSmoke();
    }
    //轮子的旋转
    void RotateWheel()
    {
        
        FRDiscBrake.Rotate((wheelFRColider.rpm * (360 / 60) * Time.deltaTime) * Vector3.right);
        FLDiscBrake.Rotate((wheelFLColider.rpm * (360 / 60) * Time.deltaTime) * Vector3.right);

        wheelRRModel.Rotate((wheelRRColider.rpm * (360 / 60) * Time.deltaTime) * Vector3.right);
        wheelRLModel.Rotate((wheelRLColider.rpm * (360 / 60) * Time.deltaTime) * Vector3.right);

    }
    //轮子的左右转动

    void SteerWheel()
    {
        Vector3 localEulerAngles = wheelFLModel.localEulerAngles;
        localEulerAngles.y = wheelFLColider.steerAngle;
        wheelFRModel.localEulerAngles = localEulerAngles;
        wheelFLModel.localEulerAngles = localEulerAngles;
    }
    //引擎的声音
    void EngineSound()
    {
        int index = 0;
        for (int i = 0; i < speedArray.Length-2; i++)
        {
            if (currentSpeed >= speedArray[index])
            {
                index = i;
            }
        }
        int minSpeed = speedArray[index];
        int maxSpeed = speedArray[index + 1];
        engineSource.pitch = 0.1f + (currentSpeed-minSpeed) / (maxSpeed-minSpeed);
        //engineSource.pitch = 0.1f + currentSpeed / maxSpeed;
    }
    Vector3 lastWheelFLColider = Vector3.zero;
    Vector3 lastwheelFRColider = Vector3.zero;
    void SkidSmoke()
    {
        if (currentSpeed > 40 && Mathf.Abs(wheelFLColider.steerAngle) > 5)
        {
            //保存上一帧的车轮位置
        
            bool isGround = false;
            WheelHit hit;
            if (wheelFLColider.GetGroundHit(out hit))
            {
                isGround = true;
                leftEmitter.emit = true;
                //产生划痕
                if (lastWheelFLColider.x != 0 && lastWheelFLColider.y != 0 && lastWheelFLColider.z != 0)
                {
                    Vector3 p = hit.point;
                    p.y += 0.5f;
                    //得到一个旋转，当前位置减去上一次的位置得到方向
                    Quaternion rotation = Quaternion.LookRotation(hit.point-lastWheelFLColider);
                    GameObject.Instantiate(skidmark, hit.point, rotation);
                }
                lastWheelFLColider = hit.point;
            }
            else {
                lastWheelFLColider = Vector3.zero;
                leftEmitter.emit = false;
            }

            if (wheelFRColider.GetGroundHit(out hit))
            {
                isGround = true;
                rightEmitter.emit = true;
                //产生划痕
                if (lastwheelFRColider != Vector3.zero)
                {
                    Vector3 p = hit.point;
                    p.y += 0.05f;
                    //得到一个旋转，当前位置减去上一次的位置得到方向
                    Quaternion rotation = Quaternion.LookRotation(hit.point - lastwheelFRColider);
                    GameObject.Instantiate(skidmark, hit.point, rotation);
                }
                lastwheelFRColider = hit.point;
            }
            else
            {
                lastwheelFRColider = Vector3.zero;
                rightEmitter.emit = false;
            }
            if (skidSource.isPlaying == false&&isGround)
            {
                skidSource.Play();
            }
            else if (skidSource.isPlaying && isGround == false)
            {
                skidSource.Stop();
            }
           

        }
        else {
            if (skidSource.isPlaying)
            {
                skidSource.Stop();
            }
            leftEmitter.emit = false;
            rightEmitter.emit = false;
        }
    }
    //控制灯
    void ControlRight()
    {
        if (currentSpeed < -3)
        {
            Light.SetActive(true);
          
        }
        else {
            Light.SetActive(false);
            
        }
    }

    //碰撞
    void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Wall")
        {
            crashSource.Play();
        }
    }
}
