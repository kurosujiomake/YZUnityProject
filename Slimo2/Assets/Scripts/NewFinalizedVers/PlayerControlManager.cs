using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerControlManager : MonoBehaviour
{
    private Gamepad pad;
    public enum Direction
    {
        neutral,
        up,
        down,
        left,
        right
    }
    [Header("Analog sticks")]
    public Direction curLStickDir;
    public Direction curRStickDir;
    public Vector2 leftStickRead;
    public Vector2 rightStickRead;
    private float horiL = 0;
    private float vertL = 0;
    private float horiR = 0;
    private float vertR = 0;
    public float[] stickDeadzoneL;
    public float[] stickDeadzoneR;
    [Header("Right side face buttons")]
    public bool a_button_pressed;
    public bool a_button_held;
    public bool a_button_released;
    public bool b_button_pressed;
    public bool b_button_held;
    public bool b_button_released;
    public bool x_button_pressed;
    public bool x_button_held;
    public bool x_button_released;
    public bool y_button_pressed;
    public bool y_button_held;
    public bool y_button_released;
    [Header("Left side face buttons (Dpad)")]
    public bool up_pressed;
    public bool up_held;
    public bool up_released;
    // Start is called before the first frame update
    void Start()
    {
        pad = Gamepad.current;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        leftStickRead = pad.leftStick.ReadValue();
        rightStickRead = pad.rightStick.ReadValue();
        
        DebugTesting();
        ButtonReads();
        curLStickDir = ReturnDir(0);
        curRStickDir = ReturnDir(1);
    }
    void DebugTesting()
    {
        if(a_button_pressed)
        {
            print("a button was pressed");
        }
    }
    void ButtonReads()
    {
        KeyHeld();
        a_button_pressed = pad.aButton.wasPressedThisFrame;
        a_button_released = pad.aButton.wasReleasedThisFrame;
        b_button_pressed = pad.bButton.wasPressedThisFrame;
        b_button_released = pad.bButton.wasReleasedThisFrame;
        x_button_pressed = pad.xButton.wasPressedThisFrame;
        x_button_released = pad.xButton.wasReleasedThisFrame;
        y_button_pressed = pad.yButton.wasPressedThisFrame;
        y_button_released = pad.yButton.wasReleasedThisFrame;
        up_pressed = pad.dpad.up.wasPressedThisFrame;
        up_released = pad.dpad.up.wasReleasedThisFrame;
        
    }
    void KeyHeld()
    {
        if(a_button_pressed)
        {
            a_button_held = true;
        }
        if(a_button_released)
        {
            a_button_held = false;
        }
        if (b_button_pressed)
        {
            b_button_held = true;
        }
        if (b_button_released)
        {
            b_button_held = false;
        }
        if (x_button_pressed)
        {
            x_button_held = true;
        }
        if (x_button_released)
        {
            x_button_held = false;
        }
        if (y_button_pressed)
        {
            y_button_held = true;
        }
        if (y_button_released)
        {
            y_button_held = false;
        }
        if (up_pressed)
        {
            up_held = true;
        }
        if (up_released)
        {
            up_held = false;
        }
        
    }
    Direction ReturnDir(int stick)
    {
        horiL = leftStickRead.x;
        vertL = leftStickRead.y;
        horiR = rightStickRead.x;
        vertR = rightStickRead.y;
        Direction d = Direction.neutral;
        switch(stick)
        {
            case 0: //left stick direction
                if(Mathf.Abs(horiL) > Mathf.Abs(vertL)) //more horizontal than vertical
                {
                    if(horiL < -stickDeadzoneL[0])
                    {
                        d = Direction.left;
                    }
                    if(horiL > stickDeadzoneL[0])
                    {
                        d = Direction.right;
                    }
                }
                if (Mathf.Abs(horiL) < Mathf.Abs(vertL)) // more vertical than horizontal
                {
                    if(vertL < -stickDeadzoneL[1])
                    {
                        d = Direction.down;
                    }
                    if(vertL > stickDeadzoneL[1])
                    {
                        d = Direction.up;
                    }
                }
                    break;
            case 1: //right stick direction
                if (Mathf.Abs(horiR) > Mathf.Abs(vertR)) //more horizontal than vertical
                {
                    if (horiR < -stickDeadzoneR[0])
                    {
                        d = Direction.left;
                    }
                    if (horiR > stickDeadzoneR[0])
                    {
                        d = Direction.right;
                    }
                }
                if (Mathf.Abs(horiR) < Mathf.Abs(vertR)) // more vertical than horizontal
                {
                    if (vertR < -stickDeadzoneR[1])
                    {
                        d = Direction.down;
                    }
                    if (vertR > stickDeadzoneR[1])
                    {
                        d = Direction.up;
                    }
                }
                break;
        }
        return d;
    }
}
