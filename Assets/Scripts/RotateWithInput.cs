using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RotateWithInput : MonoBehaviour
{
    [SerializeField] private float speedMax = 500f;
    [SerializeField] private string inputAxisName;
    [SerializeField] private Axis rotationAxis;
    [SerializeField] private bool inverted;
    [SerializeField] private bool clampAngle;
    [SerializeField] private float angleMin;
    [SerializeField] private float angleMax;

    private float angleToAdd;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        angleToAdd = Input.GetAxis(inputAxisName) * speedMax * Time.deltaTime;
        if (inverted)
        {
            angleToAdd = -angleToAdd;
        }

        Vector3 eulerAngles = transform.localEulerAngles;
        switch (rotationAxis)
        {
            case Axis.X:
                eulerAngles.x = getNewAngle(eulerAngles.x);
                break;
            case Axis.Y:
                eulerAngles.y = getNewAngle(eulerAngles.y);
                break;
            case Axis.Z:
                eulerAngles.z = getNewAngle(eulerAngles.z);
                break;
        }

        transform.localEulerAngles = eulerAngles;
    }

    private float getNewAngle(float oldAngle)
    {
        float newAngle = oldAngle + angleToAdd;
        if (clampAngle)
        {
            newAngle = Mathf.Clamp(newAngle, angleMin, angleMax);
        }
        return newAngle;
    }
}
