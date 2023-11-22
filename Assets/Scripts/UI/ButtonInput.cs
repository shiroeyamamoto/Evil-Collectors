using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    public Image leftMouseImage;
    public Image rightMouseImage;
    public Image middleMouseImage;

    private void Update()
    {
        InputKey();

        MouseColorChanger();
    }

    private void InputKey()
    {
        // Lấy tất cả các giá trị trong enum KeyCode
        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        {
            // Kiểm tra xem nếu phím đó đang được nhấn
            if (Input.GetKey(keyCode) || Input.GetKeyUp(keyCode))
            {
                // Lấy tên của phím đang được nhấn
                string keyName = keyCode.ToString();

                // Tìm GameObject trong button có tên giống với tên của phím
                Transform keyTransform = transform.Find(keyName);

                // Nếu tìm thấy, đổi màu
                if (keyTransform != null)
                {
                    Image keyImage = keyTransform.gameObject.GetComponent<Image>();

                    if (Input.GetKey(keyCode))
                    {
                        // Nếu đang giữ phím, đổi màu thành màu xanh
                        keyImage.color = Color.green;
                    }
                    else
                    {
                        // Nếu đã nhả phím, đổi màu thành màu trắng
                        keyImage.color = Color.white;
                    }
                }
            }
        }
    }

    private void MouseColorChanger()
    {
        // Check chuột trái
        if (Input.GetMouseButton(0))
        {
            leftMouseImage.color = Color.green;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftMouseImage.color = Color.white;
        }

        // Check chuột phải
        if (Input.GetMouseButton(1))
        {
            rightMouseImage.color = Color.green;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightMouseImage.color = Color.white;
        }

        // Check chuột giữa
        if (Input.GetMouseButton(2) || Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            middleMouseImage.color = Color.green;
        }
        else if (Input.GetMouseButtonUp(2) || Input.GetAxis("Mouse ScrollWheel") == 0)
        {
            middleMouseImage.color = Color.white;
        }
    }
}
