using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    private Button button;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = player.CanReplay;
    }
}
