using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SetPositionWithInput : MonoBehaviour
{
    [SerializeField] private Vector3 position = Vector3.zero;
    [SerializeField] private string[] buttonNames;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (string buttonName in buttonNames)
        {
            if (Input.GetButton(buttonName))
            {
                transform.localPosition = position;
                return;
            }
        }
    }
}
