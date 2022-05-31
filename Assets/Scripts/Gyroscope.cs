using UnityEngine;

public class Gyroscope : MonoBehaviour
{

    public float AngleMax = 0.15F;
    public float IncliPercent = 0.05F;
    public float smoothSpeed = 0.01F;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion desiredPosition = GyroToUnity(Input.gyro.attitude);
        Quaternion smoothedPosition = Quaternion.Lerp(transform.rotation, desiredPosition, smoothSpeed);
        transform.rotation = smoothedPosition;
    }


    private Quaternion GyroToUnity(Quaternion q)
    {
        float q1 = IncliPercent * q.x;
        float q2 = IncliPercent * q.y;

        if (q1 > AngleMax)
        {
            q1 = AngleMax;
        }
        if (q2 > AngleMax)
        {
            q2 = AngleMax;
        }
        if (q1 < -AngleMax)
        {
            q1 = -AngleMax;
        }
        if (q2 < -AngleMax)
        {
            q2 = -AngleMax;
        }

        return new Quaternion(q1, 0.0F, q2, 1.0F);
        //return new Quaternion(q2, 0.0F, -q1, 1.0F);
    }
}
