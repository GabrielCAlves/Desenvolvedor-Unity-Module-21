using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public Vector2 pastPosition;
    public float velocity = 1f;

    float minX = -4.5f;
    float maxX = 4.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //mousePosition AGORA - mousePosition PASSADO
            Move(Input.mousePosition.x - pastPosition.x);
        }
        pastPosition = Input.mousePosition;
    }

    public void Move(float speed)
    {
        //transform.position += Vector3.right * Time.deltaTime * speed * velocity;

        Vector3 newPosition = transform.position + Vector3.right * Time.deltaTime * speed * velocity;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        transform.position = newPosition;
    }
}
