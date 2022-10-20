using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShotsText : ValueText
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().OnShoot += SetShots;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetShots(int shots)
    {
        SetValue(shots);
    }
}
