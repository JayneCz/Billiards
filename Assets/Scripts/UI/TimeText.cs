using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TimeText : ValueText
{
    private readonly System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        SetValue($"{stopwatch.Elapsed:mm\\:ss}");
    }
}
