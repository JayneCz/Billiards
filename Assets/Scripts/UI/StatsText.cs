using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class StatsText : MonoBehaviour
{
    [SerializeField] [Multiline] private string textFormat = "Stats:\n{0}";

    private Stats stats;
    private TMP_Text tmpText;
    private bool updateContinuously = false;

    // Start is called before the first frame update
    void Start()
    {
        tmpText = gameObject.GetComponent<TMP_Text>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            updateContinuously = true;
            stats = player.GetComponent<Player>().stats;
        }
        else
        {
            stats = Stats.Load();
            if (stats == null)
            {
                tmpText.text = "";
            }
            else
            {
                SetText();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (updateContinuously)
        {
            SetText();
        }
    }

    private void SetText()
    {
        tmpText.text = string.Format(textFormat, stats.ToString());
    }
}
