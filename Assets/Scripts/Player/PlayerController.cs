using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    //  Input Controller
    private PlayerInput inputs;
    private bool run;
    private bool attack;

    //  Player Config
    private Rigidbody rb;
    private Vector2 movimento;
    private Vector2 look;
    private float rotate;
    
    [SerializeField] private float speed = 10;
    [SerializeField] private float speedWalking;
    [SerializeField] private float speedRun;
    [SerializeField] private float rotateSpeed = 30f;
    
    //  Animations Controller
    private Animator anim;
    private bool isWalking;
    private bool isRun;    

    
   

#region Inputs
    public void SetMovimento(InputAction.CallbackContext value)
    {
        movimento = value.ReadValue<Vector2>();
    }

    public void SetLook(InputAction.CallbackContext value)
    {
        rotate = value.ReadValue<Vector2>().x;
    }
    public void SetRun(InputAction.CallbackContext value)
    {
        if(value.started)
            run = true;
        if(value.performed)
            run = true;
        if(value.canceled)
            run = false;
    }

    public void SetAttack(InputAction.CallbackContext value)
    {
        if(value.started)
            attack = true;
        if(value.performed)
            attack = true;
        if(value.canceled)
            attack = false;
    }
    
#endregion

#region Unity General
    private void Update()
    {
        Walking();
        LookView();
    }

     private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else{Destroy(instance);}

        inputs = new PlayerInput();

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

#endregion

    public void Walking()
    {
        // Vector3 move = new Vector3(movimento.x, 0, movimento.y);
        // rb.AddForce( move * speed * Time.fixedDeltaTime * 300);

        Vector3 move = new Vector3(movimento.x, 0, movimento.y);

        move = transform.forward * move.z + transform.right * move.x;
        rb.AddForce(move * speed * Time.fixedDeltaTime * 300);

        if(run)
        {
            speed = speedRun;
            anim.SetBool("isRun", true);
            anim.SetBool("isWalking", false);
        }
        if(!run)
        {
            speed = speedWalking;
            anim.SetBool("isRun", false);
        }

        if(speed == speedWalking)
        {
            anim.SetBool("isWalking", true);
        }
        if(movimento.magnitude == 0)
        {
            anim.SetBool("isWalking", false);
        }
    }

    public void Attack()
    {
        
    }

    public void LookView()
    {
        transform.Rotate(Vector3.up * rotate * rotateSpeed * Time.deltaTime);
        // Vector3 lookAt = new Vector3(look.x, 0, look.y);

        // transform.rotation = Quaternion.Euler(
        //     lookAt.y * Time.deltaTime,
        //     lookAt.x * Time.deltaTime,
        //     0
        // );
    }
    
}
