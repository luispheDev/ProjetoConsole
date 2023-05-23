using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    [SerializeField]    private AnimationsController animController;

    //  Input Controller
    private PlayerInput inputs;
    private bool run;
    private bool attackPeformed;

    //  Player Config
    private Rigidbody rb;
    private Vector2 movimento;
    private Vector2 look;
    private float rotate;
    
    [SerializeField] private float speed = 10;
    [SerializeField] private float speedWalking;
    [SerializeField] private float speedRun;
    [SerializeField] private float rotateSpeed = 30f;
    

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
        if(value.performed)
            attackPeformed = true;
        if(value.canceled)
            attackPeformed = false;
    }
    
#endregion

#region Unity General
     private void Awake() {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else{Destroy(instance);}

        inputs = new PlayerInput();

        rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        InputController();
        LookView();
    }

#endregion

    public void InputController()
    {
        Walking();

        if(attackPeformed){
            Debug.Log("ta atacando");
            Attack();
        }
    }

    public void Walking()
    {
        Vector3 move = new Vector3(movimento.x, 0, movimento.y);

        move = transform.forward * move.z + transform.right * move.x;
        rb.AddForce(move * speed * Time.fixedDeltaTime * 300);

        if(run)
        {
            speed = speedRun;
            animController.RunOn();
            animController.MoveOff();
        }
        if(!run)
        {
            speed = speedWalking;
            animController.RunOff();
        }

        if(speed == speedWalking)
        {
            animController.MoveOn();
        }
        if(movimento.magnitude == 0)
        {
            animController.MoveOff();
        }
    }

    public void Attack()
    {
        animController.AttackOn();
    }

    public void LookView()
    {
        transform.Rotate(Vector3.up * rotate * rotateSpeed * Time.deltaTime);
    }
    
}
