using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PointsText : ValueText
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().OnPointGain += SetPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetPoints(int points)
    {
        SetValue(points);
    }
}
