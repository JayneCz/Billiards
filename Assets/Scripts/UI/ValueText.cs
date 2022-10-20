using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public abstract class ValueText : MonoBehaviour
{
    [SerializeField] protected string textFormat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void SetValue(object value)
    {
        gameObject.GetComponent<TMP_Text>().text = string.Format(textFormat, value);
    }
}
