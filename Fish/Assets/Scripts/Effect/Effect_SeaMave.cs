using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SeaMave : MonoBehaviour
{
    private Vector3 dirV;
    // Start is called before the first frame update
    void Start()
    {
        dirV = -transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, dirV, 10 * Time.deltaTime);
    }
}
