using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private HealthBar _healthBar;
    [SerializeField] private TakeCoin coins;
    [SerializeField] private Enemy enemy;
    private static PlayerController instance;
    [SerializeField]    private AnimationsController animController;

    //  Input Controller
    private PlayerInput inputs;
    private bool run;
    private bool attackPeformed;
    private bool defendPerfomed;

    //  Player Config
    private Rigidbody rb;
    private Vector2 movimento;
    private Vector2 look;
    private float rotate;
    public float maxLife = 5;
    public float currentLife;
    public float damage = 2;
    public bool emCombate;
    public GameObject PainelDeath;
    public GameObject PainelBloqueio;
    public GameObject Venceu;
    public bool tocou;

    


    
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
    public void SetDefend(InputAction.CallbackContext value)
    {
        if(value.performed) 
            defendPerfomed = true;

        if(value.canceled)  
            defendPerfomed = false;
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
        currentLife = maxLife;

        _healthBar.RemoveHealthBar(maxLife, currentLife);
    }

    private void Update()
    {
        InputController();
        LookView();

        if(currentLife < maxLife && emCombate == false) 
        {
            currentLife += 1;
            _healthBar.AddHealthBar(currentLife);
        }
    }

#endregion

    public void InputController()
    {
        Walking();

        if(attackPeformed){
            Attack();
        }
        if(defendPerfomed) 
            Deffend();
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

    public void TakeDamage()
    {
        currentLife -= 1;
        _healthBar.RemoveHealthBar(maxLife, currentLife);
        if(currentLife <= 0) PainelDeath.SetActive(true);
    }

    public void Attack()
    {
        animController.AttackOn();
    }
    public void Deffend()
    {
        animController.DefendOn();
    }
    public void LookView()
    {
        transform.Rotate(Vector3.up * rotate * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            if(defendPerfomed)
            {
                PainelBloqueio.SetActive(true);
            }else{
                TakeDamage();   
            }
                     
        }
    }
    private void OnTriggerExit(Collider col)
    {
        PainelBloqueio.SetActive(false);
    }

    
}
