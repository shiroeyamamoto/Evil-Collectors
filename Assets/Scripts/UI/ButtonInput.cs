using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Find("W").gameObject.GetComponent<Image>().color = Color.green;
        }
        else transform.Find("W").gameObject.GetComponent<Image>().color = Color.white;
    }
}
