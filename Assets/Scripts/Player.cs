using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private const string SHOOT_BUTTON_NAME = "Shoot";
    private const string REPLAY_BUTTON_NAME = "Replay";
    private const string OBJECTIVE_TAG = "Objective";
    private const string VICTORY_SCENE_NAME = "Victory";

    public readonly Stats stats = new Stats();
    public float ShootForceCurrent { get; private set; } = 0f;

    [field: SerializeField] public float ShootForceMin { get; private set; } = 0f;
    [field: SerializeField] public float ShootForceMax { get; private set; } = 25;

    [SerializeField] private float shootForceSpeed = 100f;
    [SerializeField] private Transform cueStick;
    [SerializeField] private int pointsToWin = 3;

    private readonly Dictionary<GameObject, bool> objectivesHit = new Dictionary<GameObject, bool>();

    private bool readyToShoot = true;

    private bool replaying = false;
    private Dictionary<GameObject, Vector3> replayObjectPositions;
    private Vector3 replayForce;

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
        stats.time += Time.deltaTime;

        readyToShoot = IsStationary() && AllObjectivesAreStationary();

        if (readyToShoot)
        {
            replaying = false;
            if (Input.GetButtonDown(REPLAY_BUTTON_NAME) && stats.shots >= 1 && ShootForceCurrent == ShootForceMin)
            {
                StartReplay();
                return;
            }
            ChargeShotWithInput();
            ShootWithInput();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (replaying)
        {
            return;
        }
        GameObject other = collision.gameObject;
        if (other.CompareTag(OBJECTIVE_TAG) && !objectivesHit[other])
        {
            objectivesHit[other] = true;

            if (!objectivesHit.ContainsValue(false))
            {
                stats.points++;
                if (stats.points >= pointsToWin)
                {
                    stats.Save();
                    SceneSwitcher.LoadScene(VICTORY_SCENE_NAME);
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

            stats.shots++;

            Vector3 force = cueStick.forward;
            force *= ShootForceCurrent;
            RecordReplay(force);
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

    private void RecordReplay(Vector3 force)
    {
        replayForce = force;
        replayObjectPositions = new Dictionary<GameObject, Vector3>();
        replayObjectPositions.Add(gameObject, transform.position);
        foreach (var objective in objectivesHit.Keys)
        {
            replayObjectPositions.Add(objective, objective.transform.position);
        }
    }

    private void StartReplay()
    {
        replaying = true;
        foreach (var objectPositionPair in replayObjectPositions)
        {
            objectPositionPair.Key.transform.position = objectPositionPair.Value;
        }
        gameObject.GetComponent<Rigidbody>().AddForce(replayForce, ForceMode.Impulse);
    }

}
