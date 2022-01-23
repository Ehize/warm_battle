using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Wormy : Photon.MonoBehaviour
{
    protected Joystick joystick;
    protected Joybutton joybutton;

    public PhotonView photonView;

    public Rigidbody2D bulletPrefab;
    public Transform currentGun;

    public float wormySpeed = 1;
    public float maxRelativeVelocity;
    public float misileForce = 5; 

    //public bool IsTurn { get { return WormyManager.singleton.IsMyTurn(wormId); } }

    public int wormId;
    WormyHealth wormyHealth;
    SpriteRenderer ren;

    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();

        wormyHealth = GetComponent<WormyHealth>();
        ren = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (photonView.isMine) 
        {
         //Mobile touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
             Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        }

        var hor = CrossPlatformInputManager.GetAxis("Horizontal");
        if (hor == 0)
        {
            currentGun.gameObject.SetActive(true);

            ren.flipX = currentGun.eulerAngles.z < 180;

            if (Input.GetKeyDown(KeyCode.Q))
            // if (joybutton.Pressed)
            {
                var p = Instantiate(bulletPrefab,
                                   currentGun.position - currentGun.right,
                                   currentGun.rotation);

                p.AddForce(-currentGun.right * misileForce, ForceMode2D.Impulse);

               /* if (IsTurn)
                    WormyManager.singleton.NextWorm();
                    */
            }
        }
        else
        {
            currentGun.gameObject.SetActive(false);
            transform.position += Vector3.right *
                                hor *
                                Time.deltaTime *
                                wormySpeed;            
            // ren.flipX = Input.GetAxis("Horizontal") > 0;
            ren.flipX = CrossPlatformInputManager.GetAxis("Horizontal") > 0;
        }


            RotateGun();
        }
       

        /*if (!IsTurn)
              return; */

        // RotateGun();

        // var hor = Input.GetAxis("Horizontal");
       
    }

    void RotateGun()
    {
        var diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        currentGun.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > maxRelativeVelocity)
        {
            wormyHealth.ChangeHealth(-3);
           /* if (IsTurn)
                WormyManager.singleton.NextWorm(); */
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            wormyHealth.ChangeHealth(-10);
           /* if (IsTurn)
                WormyManager.singleton.NextWorm(); */
        }
            
    }
}
