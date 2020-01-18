using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFllow : MonoBehaviour
{
    public RectTransform UiCanvas;
    public Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(UiCanvas,
            new Vector2(Input.mousePosition.x, Input.mousePosition.y), mainCamera,
            out mousePos);

        float z;
        //Debug.Log(transform.position);
        if (mousePos.x > transform.position.x)
        {
            z = -Vector3.Angle(Vector3.up, mousePos - transform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, mousePos - transform.position);
        }
        
        transform.localRotation = Quaternion.Euler(0, 0, z);

        
    }
}
