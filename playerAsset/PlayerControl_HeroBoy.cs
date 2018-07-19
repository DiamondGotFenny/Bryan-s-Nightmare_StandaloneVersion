using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_HeroBoy : MonoBehaviour {

    Animator animat;
    WeaponEquip weaponEquip;

    public float moveSpeed = 6f;
    Vector3 movement;
    int floorMask;
    float camRaylength = 100f;
    Rigidbody playerrigidbody;
    [SerializeField] TrailRenderer TR;

    AudioSource swordSound;

    public AudioClip attackSound;

    bool walking;
    bool running;

    private void Awake()
    {
        animat = GetComponent<Animator>();
        weaponEquip = GetComponent<WeaponEquip>();
        floorMask = LayerMask.GetMask("Floor");
        playerrigidbody = GetComponent<Rigidbody>();
        TR = GetComponentInChildren<TrailRenderer>();
        swordSound = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        move(h, v);
        Turning();
        AnimationControl(h, v);
    }

    void Update () {
        TR.time = animat.GetFloat("Blade");
    }
	

    void move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        playerrigidbody.MovePosition(transform.position + movement);
    }

    void AnimationControl(float h,float v)
    {
        if (weaponEquip.AKrifle == true && weaponEquip.Lightsaber == false)
        {
            animat.SetBool("SwordIdle", false);
        }

        if (weaponEquip.AKrifle == false && weaponEquip.Lightsaber == true)
        {
            animat.SetBool("SwordIdle", true);
        }

        if (weaponEquip.AKrifle==true)
        {
            walking = h != 0f || v != 0f;
            animat.SetBool("IsMove", walking);

            if (Input.GetButton("Fire1") && walking == false)
            {
                animat.SetBool("IsShoot", true);
            }
            else
            {
                animat.SetBool("IsShoot", false);
            }
        }

        if (weaponEquip.Lightsaber==true)
        {
            running = h != 0f || v != 0f;
            animat.SetBool("IsMove", false);
            animat.SetBool("IsRun", running);

            if (Input.GetButtonDown("Fire1")&&running==false)
            {
                animat.SetBool("IsAttack", true);
            }
            else
            {
                animat.SetBool("IsAttack", false);
            }

            if (Input.GetButtonDown("Fire2") && running == false)
            {
                OnSkill();
            }
            else
            {
                animat.SetBool("IsSkill", false);
            }
        }                
    }

    public void OnSkill()
    {
        animat.SetBool("IsSkill", true);
    }

    void Turning()
    {
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camray,out floorHit, camRaylength, floorMask))
        {
            Vector3 playertoMouse = floorHit.point - transform.position;
            playertoMouse.y = 0;
            Quaternion newQuaternion = Quaternion.LookRotation(playertoMouse);
            playerrigidbody.MoveRotation(newQuaternion);
        }
    }
}
