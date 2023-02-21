using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public bool IsRight { get; set; } = true;
    Vector3 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = gameObject.transform.position;
        if (gameObject.tag == "Player" && ((position.x > previousPosition.x && !IsRight) || (position.x < previousPosition.x && IsRight)))
        {
            Collider2D collider = GameObject.Find("Inner Boundary").GetComponent<PolygonCollider2D>();
            if (previousPosition.x > collider.bounds.min.x && previousPosition.x < collider.bounds.max.x)
            {
                ChangeDirection();
            }
        }

        if (gameObject.tag == "Enemy" && ((position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x && !IsRight)
            || (position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x && IsRight)))
        {
            ChangeDirection();
        }
        previousPosition = position;

    }


    public void ChangeDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        IsRight = !IsRight;
    }

}
