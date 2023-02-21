using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IBaseEntity
{
    public BaseData.PlayerDataManager playerData;

    private Rigidbody2D _body;

    public Transform playerTransform;
    Animator animator;

    public float BaseSpeed { get; set; } = 8;
    public float SmoothTime { get; set; } = 0.04f;

    private Vector3 velocitySmoothing;

    public FloatingJoystick joystick;
    private State PlayerState { get; set; } = State.IDLE;

    [SerializeField]
    public float SafeDistance { get; set; } = 3f;

    [SerializeField]
    public float HP;

    [SerializeField]
    public float regen;

    public bool canRegen = true;
    public float timeToHealth = 5f;

    private Vector3 moveDir;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        joystick = GameObject.FindGameObjectWithTag("InputControl").GetComponent<FloatingJoystick>();
        animator = gameObject.GetComponent<Animator>();

        gameObject.GetComponent<HealthSystem>().CurrentHealth = HP;
        gameObject.GetComponent<HealthSystem>().MaximumHealth = HP;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        switch (PlayerState)
        {
            case State.MOVEMENT:
                break;
            case State.IDLE:
                Idle();
                break;
        }
        if(gameObject.GetComponent<HealthSystem>().MaximumHealth < HP)
        {
            gameObject.GetComponent<HealthSystem>().MaximumHealth = HP;
        }

        TimeRegen();
    }

    void TimeRegen()
    {
        if (!canRegen)
        {
            timeToHealth -= Time.deltaTime;
            if(timeToHealth < 0)
            {
                canRegen = true;
            }
        }
        else
        {
            timeToHealth -= Time.deltaTime;
            if (timeToHealth < 0)
            {
                Regen(regen);
                timeToHealth = 3f;
            }
        }
    }

    void Regen(float regen)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<HealthSystem>().GotHealedFor(regen);
    }

    public void Movement()
    {
        bool isMoving = false;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isMoving = true;
            _body.velocity = Vector3.SmoothDamp(_body.velocity, Vector3.left * BaseSpeed, ref velocitySmoothing, SmoothTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
            _body.velocity = Vector3.SmoothDamp(_body.velocity, Vector3.right * BaseSpeed, ref velocitySmoothing, SmoothTime);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            isMoving = true;
            _body.velocity = Vector3.SmoothDamp(_body.velocity, Vector3.up * BaseSpeed, ref velocitySmoothing, SmoothTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            isMoving = true;
            _body.velocity = Vector3.SmoothDamp(_body.velocity, Vector3.down * BaseSpeed, ref velocitySmoothing, SmoothTime);
        }

        if (joystick && joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            isMoving = true;
            moveDir = new Vector2(joystick.Horizontal, joystick.Vertical);
            _body.velocity = Vector3.SmoothDamp(_body.velocity, moveDir * BaseSpeed, ref velocitySmoothing, SmoothTime);
        }
        if (isMoving)
        {
            animator.SetBool("Run", true);
            PlayerState = State.MOVEMENT;

        } else
        {
            animator.SetBool("Run", false);
            PlayerState = State.IDLE;
        }
        ClampInScreen();
    }

    private void ClampInScreen()
    {
        //GameObject cam = GameObject.FindWithTag("Cam");
        Collider2D collider = GameObject.Find("Inner Boundary").GetComponent<PolygonCollider2D>();  
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(transform.position.x, collider.bounds.min.x, collider.bounds.max.x);
        position.y = Mathf.Clamp(transform.position.y, collider.bounds.min.y, collider.bounds.max.y);
        transform.position = position;
    }

    public void Idle()
    {
        _body.velocity = Vector3.zero;
    }
    enum State
    {
        IDLE = 0,
        MOVEMENT = 1,
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.PlayAudioOneShot((AudioClip) Resources.Load("Audios/KillSound"), 0.1f);
    }

    public void IsAlive(bool isAlive)
    {
        if (!isAlive)
        {
            gameObject.GetComponent<HealthSystem>().CurrentHealth = 0;
            AudioManager.Instance.PlayAudioOneShot((AudioClip)Resources.Load("Audios/GameOver"), 1f);
            SceneManager.LoadSceneAsync("GameOver");
        }
    }
}
