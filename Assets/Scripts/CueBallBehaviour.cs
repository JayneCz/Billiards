using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CueBallBehaviour : MonoBehaviour
{
    private const string SHOOT_BUTTON_NAME = "Shoot";

    [SerializeField] private Transform cueStick;

    [SerializeField] private float shootForceMin = 0f;
    [SerializeField] private float shootForceMax = 100f;
    [SerializeField] private float shootForceSpeed = 1000f;

    private float shootForceCurrent = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(SHOOT_BUTTON_NAME))
        {
            shootForceCurrent += shootForceSpeed * Time.deltaTime;
            shootForceCurrent = Mathf.Clamp(shootForceCurrent, shootForceMin, shootForceMax);
        }
        if (Input.GetButtonUp(SHOOT_BUTTON_NAME))
        {
            Shoot();
            return;
        }
    }

    private void Shoot()
    {
        Vector3 force = cueStick.forward;
        force *= shootForceCurrent;
        gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        shootForceCurrent = shootForceMin;
    }
}
