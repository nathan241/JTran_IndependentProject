using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishJump : MonoBehaviour
{
    float timeCounter = 0;
    public float speed = 5.0f;
    public float width = 2.0f;
    public float height = 2.0f;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float y = Mathf.Sin(timeCounter) * height;
        float z = 0;
       
        transform.position = new Vector3(x, y, z) + startPosition;
    }
}
