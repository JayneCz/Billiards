using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private const string SHOOT_BUTTON_NAME = "Shoot";
    private const string OBJECTIVE_TAG = "Objective";

    public event Action<int> OnPointGain;
    public event Action<int> OnShoot;
    public float ShootForceCurrent { get; private set; } = 0f;


    [field: SerializeField] public float ShootForceMin { get; private set; } = 0f;
    [field: SerializeField] public float ShootForceMax { get; private set; } = 25;

    [SerializeField] private float shootForceSpeed = 100f;
    [SerializeField] private Transform cueStick;
    [SerializeField] private int pointsToWin = 3;

    private int shots = 0;
    private int points = 0;

    private bool readyToShoot = true;

    private readonly Dictionary<GameObject, bool> objectivesHit = new Dictionary<GameObject, bool>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var objective in GameObject.FindGameObjectsWithTag(OBJECTIVE_TAG))
        {
            objectivesHit.Add(objective, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        readyToShoot = IsStationary() && AllObjectivesAreStationary();

        if (readyToShoot)
        {
            ChargeShotWithInput();
            ShootWithInput();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag(OBJECTIVE_TAG) && !objectivesHit[other])
        {
            objectivesHit[other] = true;

            if (!objectivesHit.ContainsValue(false))
            {
                points++;
                OnPointGain?.Invoke(points);
                if (points >= pointsToWin)
                {
                    Debug.Log("You win!");
                }
            }
        }
    }

    private bool IsStationary()
    {
        return GetComponent<Rigidbody>().velocity == Vector3.zero;
    }

    private bool AllObjectivesAreStationary()
    {
        foreach (var objective in objectivesHit.Keys)
        {
            if (objective.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                return false;
            }
        }
        return true;
    }

    private void ChargeShotWithInput()
    {
        if (Input.GetButton(SHOOT_BUTTON_NAME))
        {
            ShootForceCurrent += shootForceSpeed * Time.deltaTime;
            ShootForceCurrent = Mathf.Clamp(ShootForceCurrent, ShootForceMin, ShootForceMax);
        }
    }

    private void ShootWithInput()
    {
        if (Input.GetButtonUp(SHOOT_BUTTON_NAME))
        {
            readyToShoot = false;

            ResetObjectivesHit();

            OnShoot?.Invoke(++shots);

            Vector3 force = cueStick.forward;
            force *= ShootForceCurrent;
            gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            ShootForceCurrent = ShootForceMin;
        }
    }
    private void ResetObjectivesHit()
    {
        foreach (var objective in objectivesHit.Keys.ToList())
        {
            objectivesHit[objective] = false;
        }
    }

}
