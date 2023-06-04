using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Playercontroller : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;
    public float jumpForce;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick joystick;

    private AudioSource audioSource;

    internal bool jump=false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0)
        {
            Move();
        }
        else
        {
            joystickMove();
        }
        if (Input.GetButtonDown("Jump") || jump) 
        {
            tryJump();
            jump = false;
        }
    }

    void joystickMove()
    {
        _rigidbody.velocity = new Vector3(joystick.Horizontal * moveSpeed, _rigidbody.velocity.y, joystick.Vertical * moveSpeed);

        Vector3 facingDir = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (facingDir.magnitude > 0)
        {
            transform.forward = facingDir;
        }
        /*if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }*/
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(xInput, 0, zInput)*moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;

        //Face direction
        Vector3 facingDir = new Vector3(xInput, 0, zInput);
        if(facingDir.magnitude>0)
        {
            transform.forward = facingDir;
        }

    }

    void tryJump()
    {
        Ray ray1 = new Ray(transform.position + new Vector3(0.5f, 0.0f, 0.5f), Vector3.down);
        Ray ray2 = new Ray(transform.position + new Vector3(-0.5f, 0.0f, 0.5f), Vector3.down);
        Ray ray3 = new Ray(transform.position + new Vector3(0.5f, 0.0f, -0.5f), Vector3.down);
        Ray ray4 = new Ray(transform.position + new Vector3(-0.5f, 0.0f, -0.5f), Vector3.down);

        bool cast1 = Physics.Raycast(ray1, 0.7f);
        bool cast2 = Physics.Raycast(ray2, 0.7f);
        bool cast3 = Physics.Raycast(ray3, 0.7f);
        bool cast4 = Physics.Raycast(ray4, 0.7f);

        if(cast1 || cast2 || cast3 || cast4)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("trigger");
            GameManager.instance.GameOver();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(other.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(1);
            Destroy(other.gameObject);
            audioSource.Play();
        }
        else if(other.CompareTag("Goal"))
        {
            Debug.Log("goal");
            GameManager.instance.LevelEnd();
        }

    }


}
