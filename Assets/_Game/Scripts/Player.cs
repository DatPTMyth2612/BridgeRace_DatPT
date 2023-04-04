using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class Player : Character
{
    public CharacterController controller;
    private Vector3 direction;
    private Vector3 velocity;
    private float gravity = -10f;
    private FloatingJoystick joystick;
    public int randomColorInt;

    private void Awake()
    {
        ChangeColor();
    }
    private void Start()
    {
        joystick = FindObjectOfType<FloatingJoystick>();
        //ChangeColor();
    }
    private void Update()
    {
        direction = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y).normalized;
        RaycastHit stair;
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, out stair, Mathf.Infinity, stairLayer))
        {
            Stair stairScript = stair.transform.gameObject.GetComponent<Stair>();
            Material stairMaterial = stair.transform.gameObject.GetComponent<MeshRenderer>().material;
            if (stairScript.stairColor != characterColor || stairMaterial == stairScript.defaultMaterial)
            {
                if (direction.z >= 0f) direction.z = 0;
            }
        }
        if (Vector3.Distance(Vector3.zero, direction) > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void ChangeColor()
    {
        randomColorInt = UnityEngine.Random.Range(0, 4);
        ColorData.ColorType color = (ColorData.ColorType)randomColorInt;
        GetComponent<MeshRenderer>().material = colorData.GetColor(color);
        characterColor = color;
    }
}
