using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class CueBallBehaviour : MonoBehaviour
{
    public event Action<int> OnHitObjective;
    public event Action<int> OnShoot;

    private const string SHOOT_BUTTON_NAME = "Shoot";
    private const string OBJECTIVE_TAG = "Objective";

    [Header("Shooting")]
    [SerializeField] private Transform cueStick;
    [SerializeField] private float shootForceMin = 0f;
    [SerializeField] private float shootForceMax = 100f;
    [SerializeField] private float shootForceSpeed = 1000f;

    [Header("Objectives")]
    [SerializeField] private int objectiveAmount = 2;

    private int shotCount = 0;
    private float shootForceCurrent = 0f;
    private readonly ISet<GameObject> hitObjectives = new HashSet<GameObject>();

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
    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag(OBJECTIVE_TAG))
        {
            hitObjectives.Add(other);
            OnHitObjective?.Invoke(hitObjectives.Count);

            if (hitObjectives.Count >= objectiveAmount)
            {
                Debug.Log("You win!");
            }
        }
    }

    private void Shoot()
    {
        OnShoot?.Invoke(++shotCount);

        Vector3 force = cueStick.forward;
        force *= shootForceCurrent;
        gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        shootForceCurrent = shootForceMin;
    }

}
