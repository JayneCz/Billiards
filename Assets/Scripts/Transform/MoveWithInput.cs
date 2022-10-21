using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MoveWithInput : MonoBehaviour
{
    [SerializeField] private float speedMax = 10f;

    [Header("X")]
    [SerializeField] private bool useAxisX = true;
    [SerializeField] private string axisNameX = "Horizontal";

    [Header("Y")]
    [SerializeField] private bool useAxisY = false;
    [SerializeField] private string axisNameY;

    [Header("Z")]
    [SerializeField] private bool useAxisZ = true;
    [SerializeField] private string axisNameZ = "Vertical";

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float x = useAxisX ? Input.GetAxis(axisNameX) : 0f;
        float y = useAxisY ? Input.GetAxis(axisNameY) : 0f;
        float z = useAxisZ ? Input.GetAxis(axisNameZ) : 0f;

        direction = Vector3.ClampMagnitude(new Vector3(x, y, z), 1f);

        transform.Translate(direction * speedMax * Time.deltaTime);
    }
}
