using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ShotForceSlider : MonoBehaviour
{
    private Slider slider;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag(Player.TAG).GetComponent<Player>();

        slider.minValue = player.ShootForceMin;
        slider.maxValue = player.ShootForceMax;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.ShootForceCurrent;
    }
}
