using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    private Rigidbody2D _body;

    public Transform playerTransform;

    public float BaseSpeed { get; set; } = 5f;
    public float SmoothTime { get; set; } = 0.04f;


    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        playerTransform.position = getPlayerTransform().position;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        playerTransform.position = getPlayerTransform().position;

        Vector3 newPos = Vector3.MoveTowards(transform.position, playerTransform.position, BaseSpeed * Time.deltaTime);
        _body.MovePosition(newPos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            AudioManager.Instance.PlayAudioOneShot((AudioClip)Resources.Load("Audios/KillSound"), 0.1f);
    }

    private Transform getPlayerTransform()
    {
        return GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }
}
