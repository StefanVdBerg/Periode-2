﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Vector3 v;
    public Vector3 v2;
    public GameObject body;
    public GameObject head;
    public GameObject player;
    public Vector3 mov;
    public float speed;
    public float startSpeed;
    public float rotY;
    public float maxRot;
    public Camera cam;
    public GameObject hpHolder;
    public bool inMenu = false;
    public GameObject shooter;
    public GameObject rotator;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Screen.fullScreen = true;
        startSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        rotY = transform.eulerAngles.x;
        rotY = Mathf.Clamp(rotY, -maxRot, maxRot);

        v.y = Input.GetAxis("Mouse X");
        v2.x = Input.GetAxis("Mouse Y");

        mov.x = -Input.GetAxis("Horizontal");
        mov.z = -Input.GetAxis("Vertical");

        transform.position = new Vector3(gameObject.transform.position.x, 16, gameObject.transform.position.z);

        if (inMenu == false)
        {
            transform.Translate(mov * Time.deltaTime * speed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

            body.transform.Rotate(v * Time.deltaTime * 60, Space.Self);
            head.transform.Rotate(v2 * Time.deltaTime * 50, Space.Self);
        }

        if (head.transform.eulerAngles.x <= 20.99f)
        {
            head.transform.eulerAngles = new Vector3(21, head.transform.eulerAngles.y, head.transform.eulerAngles.z);
        }

        if (Input.GetButtonDown("Run"))
        {
            speed = startSpeed + 12;
        }
        else
        {
            if (Input.GetButtonUp("Run"))
            {
                speed = startSpeed;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            speed = speed - 9;
        }
        else
        {
            if (Input.GetButtonUp("Fire1"))
            {
                speed = speed + 9;
            }
        }

        if (Input.GetButtonDown("Lock"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                shooter.gameObject.GetComponent<LaserFiring>().enabled = false;
                rotator.gameObject.GetComponent<WeaponRotate>().enabled = false;
                inMenu = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                shooter.gameObject.GetComponent<LaserFiring>().enabled = true;
                rotator.gameObject.GetComponent<WeaponRotate>().enabled = true;
                inMenu = false;
            }
        }

        body.GetComponent<Rigidbody>().velocity.Set(0, 0, 0);

        if (Input.GetButtonDown("Cancel"))
        {
            print("ded");
            Application.Quit();
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            hpHolder.gameObject.GetComponent<HP>().Damage(0.05f);
        }
    }
}
