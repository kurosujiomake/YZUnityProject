using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerControlManager : MonoBehaviour
{
    private Gamepad pad;
    public KeyBinding kb;
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
    public bool down_pressed;
    public bool down_held;
    public bool down_released;
    public bool left_pressed;
    public bool left_held;
    public bool left_released;
    public bool right_pressed;
    public bool right_held;
    public bool right_released;
    [Header("Shoulder Buttons")]
    public bool Lbumper_pressed;
    public bool Lbumper_held;
    public bool Lbumper_released;
    public bool LTrigger_pressed;
    public bool LTrigger_held;
    public bool LTrigger_released;
    public bool Rbumper_pressed;
    public bool Rbumper_held;
    public bool Rbumper_released;
    public bool RTrigger_pressed;
    public bool RTrigger_held;
    public bool RTrigger_released;


    // Start is called before the first frame update
    void Awake()
    {
        pad = Gamepad.current;
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
        down_pressed = pad.dpad.down.wasPressedThisFrame;
        down_released = pad.dpad.down.wasReleasedThisFrame;
        left_pressed = pad.dpad.left.wasPressedThisFrame;
        left_released = pad.dpad.left.wasReleasedThisFrame;
        right_pressed = pad.dpad.right.wasPressedThisFrame;
        right_released = pad.dpad.right.wasReleasedThisFrame;
        Lbumper_pressed = pad.leftShoulder.wasPressedThisFrame;
        Lbumper_released = pad.leftShoulder.wasReleasedThisFrame;
        LTrigger_pressed = pad.leftTrigger.wasPressedThisFrame;
        LTrigger_released = pad.leftTrigger.wasReleasedThisFrame;
        Rbumper_pressed = pad.rightShoulder.wasPressedThisFrame;
        Rbumper_released = pad.rightShoulder.wasReleasedThisFrame;
        RTrigger_pressed = pad.rightTrigger.wasPressedThisFrame;
        RTrigger_released = pad.rightTrigger.wasReleasedThisFrame;
    }
    void KeyHeld()
    {
        ButtonReads();
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
        if (down_pressed)
        {
            down_held = true;
        }
        if (down_released)
        {
            down_held = false;
        }
        if (left_pressed)
        {
            left_held = true;
        }
        if (left_released)
        {
            left_held = false;
        }
        if (right_pressed)
        {
            right_held = true;
        }
        if (right_released)
        {
            right_held = false;
        }
        if(Lbumper_pressed)
        {
            Lbumper_held = true;
        }
        if(Lbumper_released)
        {
            Lbumper_held = false;
        }
        if(LTrigger_pressed)
        {
            LTrigger_held = true;
        }
        if(LTrigger_released)
        {
            LTrigger_held = false;
        }
        if(Rbumper_pressed)
        {
            Rbumper_held = true;
        }
        if(Rbumper_released)
        {
            Rbumper_held = false;
        }
        if(RTrigger_pressed)
        {
            RTrigger_held = true;
        }
        if(RTrigger_released)
        {
            RTrigger_held = false;
        }
    }
    Direction ReturnDir(int stick)
    {
        StickReads();
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
    void StickReads()
    {
        if (pad != null)
        {
            leftStickRead = pad.leftStick.ReadValue();
            rightStickRead = pad.rightStick.ReadValue();
        }
        horiL = leftStickRead.x;
        vertL = leftStickRead.y;
        horiR = rightStickRead.x;
        vertR = rightStickRead.y;
    }
    public bool GetButtonDown(string which)
    {
        KeyHeld();
        bool b = false;
        string btn = kb.ReturnBtn(which);
        switch(btn)
        {
            case "a":
                b = a_button_pressed;
                break;
            case "b":
                b = b_button_pressed;
                break;
            case "x":
                b = x_button_pressed;
                break;
            case "y":
                b = y_button_pressed;
                break;
            case "up":
                b = up_pressed;
                break;
            case "down":
                b = down_pressed;
                break;
            case "left":
                b = left_pressed;
                break;
            case "right":
                b = right_pressed;
                break;
            case "LBumper":
                b = Lbumper_pressed;
                break;
            case "LTrigger":
                b = LTrigger_pressed;
                break;
            case "RBumper":
                b = Rbumper_pressed;
                break;
            case "RTrigger":
                b = RTrigger_pressed;
                break;
        }
        return b;
    }
    public bool GetButtonUp(string which)
    {
        KeyHeld();
        bool b = false;
        string btn = kb.ReturnBtn(which);
        switch (btn)
        {
            case "a":
                b = a_button_released;
                break;
            case "b":
                b = b_button_released;
                break;
            case "x":
                b = x_button_released;
                break;
            case "y":
                b = y_button_released;
                break;
            case "up":
                b = up_released;
                break;
            case "down":
                b = down_released;
                break;
            case "left":
                b = left_released;
                break;
            case "right":
                b = right_released;
                break;
            case "LBumper":
                b = Lbumper_released;
                break;
            case "LTrigger":
                b = LTrigger_released;
                break;
            case "RBumper":
                b = Rbumper_released;
                break;
            case "RTrigger":
                b = RTrigger_released;
                break;
        }
        return b;
    }
    public bool GetButtonHeld(string which)
    {
        KeyHeld();
        bool b = false;
        string btn = kb.ReturnBtn(which);
        switch (btn)
        {
            case "a":
                b = a_button_held;
                break;
            case "b":
                b = b_button_held;
                break;
            case "x":
                b = x_button_held;
                break;
            case "y":
                b = y_button_held;
                break;
            case "up":
                b = up_held;
                break;
            case "down":
                b = down_held;
                break;
            case "left":
                b = left_held;
                break;
            case "right":
                b = right_held;
                break;
            case "LBumper":
                b = Lbumper_held;
                break;
            case "LTrigger":
                b = LTrigger_held;
                break;
            case "RBumper":
                b = Rbumper_held;
                break;
            case "RTrigger":
                b = RTrigger_held;
                break;
        }
        return b;
    }
    public string GetDirectionL()
    {
        curLStickDir = ReturnDir(0);
        string s = "n"; 
        switch(curLStickDir)
        {
            case Direction.neutral:
                s = "n";
                break;
            case Direction.up:
                s = "u";
                break;
            case Direction.down:
                s = "d";
                break;
            case Direction.left:
                s = "l";
                break;
            case Direction.right:
                s = "r";
                break;
        }
        return s;
    } // returns string u, d, l, r, n for up, down, left, right, neutral
    public string GetdirectionR()
    {
        curRStickDir = ReturnDir(1);
        string s = "n";
        switch (curRStickDir)
        {
            case Direction.neutral:
                s = "n";
                break;
            case Direction.up:
                s = "u";
                break;
            case Direction.down:
                s = "d";
                break;
            case Direction.left:
                s = "l";
                break;
            case Direction.right:
                s = "r";
                break;
        }
        return s;
    }  // returns right stick in u, d, l ,r, n
    public float ReturnAxis(string which, string axis)
    {
        StickReads();
        float f = 0;
        switch(which)
        {
            case "left":
                switch(axis)
                {
                    case "hori":
                        f = horiL;
                        break;
                    case "vert":
                        f = vertL;
                        break;
                }
                break;
            case "right":
                switch (axis)
                {
                    case "hori":
                        f = horiR;
                        break;
                    case "vert":
                        f = vertR;
                        break;
                }
                break;
        }
        return f;
    }
}

[System.Serializable]
public class KeyBinding
{
    [Header("Set your key bindings here")]
    public string Atk1_btn;
    public string Atk2_btn;
    public string Atk3_btn;
    public string Jump_btn;
    public string Dash_btn;
    public string SwapWep_btn;
    public string Inv_btn;

    public string ReturnBtn(string which)
    {
        string s = "";
        switch(which)
        {
            case "Atk1":
                s = Atk1_btn;
                break;
            case "Atk2":
                s = Atk2_btn;
                break;
            case "Atk3":
                s = Atk3_btn;
                break;
            case "Jump":
                s = Jump_btn;
                break;
            case "Dash":
                s = Dash_btn;
                break;
            case "SwapWep":
                s = SwapWep_btn;
                break;
            case "Inv":
                s = Inv_btn;
                break;
        }
        return s;
    }
}
