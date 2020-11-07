﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;

public class PlayerControlManager : MonoBehaviour
{
    private Event e;
    private Gamepad pad;
    private Keyboard board;
    public enum ControlType
    {
        controller,
        keyboard,
        both
    }
    [Header("Note both doesnt currently work, please dont select it")]
    public ControlType curControlType;
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

    [Header("ButtonPressCorolations")]
    public bool Atk1_pressed;
    public bool Atk1_held;
    public bool Atk1_released;
    public bool Atk2_pressed;
    public bool Atk2_held;
    public bool Atk2_released;
    public bool Atk3_pressed;
    public bool Atk3_held;
    public bool Atk3_released;
    public bool Jump_pressed;
    public bool Jump_held;
    public bool Jump_released;
    public bool Dash_pressed;
    public bool Dash_held;
    public bool Dash_released;
    public bool SwapWep_pressed;
    public bool SwapWep_held;
    public bool SwapWep_released;
    public bool Inv_pressed;
    public bool Inv_held;
    public bool Inv_released;
    [Header("Analogue stick equvilants for keyboard")]
    public bool LeftKey_Pressed;
    public bool LeftKey_Held;
    public bool LeftKey_Released;
    public bool RightKey_Pressed;
    public bool RightKey_Held;
    public bool RightKey_Released;
    public bool UpKey_Pressed;
    public bool UpKey_Held;
    public bool UpKey_Released;
    public bool DownKey_Pressed;
    public bool DownKey_Held;
    public bool DownKey_Released;
    // Start is called before the first frame update
    void Awake()
    {
        ControlSetup();
    }
    public void ControlSetup()
    {
        if (Gamepad.current != null)
            pad = Gamepad.current;
        if (Keyboard.current != null)
            board = Keyboard.current;
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
        KeyHelds();
        bool b = false;
        switch(which)
        {
            case "Atk1":
                b = Atk1_pressed;
                break;
            case "Atk2":
                b = Atk2_pressed;
                break;
            case "Atk3":
                b = Atk3_pressed;
                break;
            case "Jump":
                b = Jump_pressed;
                break;
            case "Dash":
                b = Dash_pressed;
                break;
            case "SwapWep":
                b = SwapWep_pressed;
                break;
            case "Inv":
                b = Inv_pressed;
                break;
        }
        return b;
    }
    public bool GetButtonUp(string which)
    {
        KeyHelds();
        bool b = false;
        switch (which)
        {
            case "Atk1":
                b = Atk1_released;
                break;
            case "Atk2":
                b = Atk2_released;
                break;
            case "Atk3":
                b = Atk3_released;
                break;
            case "Jump":
                b = Jump_released;
                break;
            case "Dash":
                b = Dash_released;
                break;
            case "SwapWep":
                b = SwapWep_released;
                break;
            case "Inv":
                b = Inv_released;
                break;
        }
        return b;
    }
    public bool GetButtonHeld(string which)
    {
        KeyHelds();
        bool b = false;
        switch (which)
        {
            case "Atk1":
                b = Atk1_held;
                break;
            case "Atk2":
                b = Atk2_held;
                break;
            case "Atk3":
                b = Atk3_held;
                break;
            case "Jump":
                b = Jump_held;
                break;
            case "Dash":
                b = Dash_held;
                break;
            case "SwapWep":
                b = SwapWep_held;
                break;
            case "Inv":
                b = Inv_held;
                break;
        }
        return b;
    }
    string KeyboardDir()
    {
        string d = "n";
        if(LeftKey_Held)
            d = "l";
        if (RightKey_Held)
            d = "r";
        if (UpKey_Held)
            d = "u";
        if (DownKey_Held)
            d = "d";
        return d;
    }
    public string GetDirectionL()
    {
        string s = "n";
        if(curControlType == ControlType.controller || curControlType == ControlType.both)
        {
            curLStickDir = ReturnDir(0);
            switch (curLStickDir)
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
        }
        if (curControlType == ControlType.keyboard || curControlType == ControlType.both)
        {
            s = KeyboardDir();
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
    }  // returns right stick in u, d, l ,r, n || does not work for keyboard only controls
    float KeyboardAxis(string dirAxis)
    {
        KeyHelds();
        float k = 0;
        switch(dirAxis)
        {
            case "hori":
                if(LeftKey_Held)
                    k = -0.999999998f;
                if (RightKey_Held)
                    k = 0.999999998f;
                if (LeftKey_Held && RightKey_Held)
                    k = 0;
                break;
            case "vert":
                if (UpKey_Held)
                    k = 0.999999998f;
                if (DownKey_Held)
                    k = -0.999999998f;
                if (DownKey_Held && UpKey_Held)
                    k = 0;
                break;
        }
        return k;
    }
    public float ReturnAxis(string which, string axis)
    {
        float f = 0;
        if(curControlType == ControlType.controller || curControlType == ControlType.both)
        {
            StickReads();
            switch (which)
            {
                case "left":
                    switch (axis)
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
        }
        if(curControlType == ControlType.keyboard || curControlType == ControlType.both)
        {
            f = KeyboardAxis(axis);
        }
        return f;
    }
    void KeyPresses()
    {
        Held();
        Atk1_pressed = ((KeyControl)board[kb.Atk1_Key]).wasPressedThisFrame;
        Atk2_pressed = ((KeyControl)board[kb.Atk2_Key]).wasPressedThisFrame;
        Atk3_pressed = ((KeyControl)board[kb.Atk3_Key]).wasPressedThisFrame;
        Jump_pressed = ((KeyControl)board[kb.Jump_Key]).wasPressedThisFrame;
        Dash_pressed = ((KeyControl)board[kb.Dash_Key]).wasPressedThisFrame;
        SwapWep_pressed = ((KeyControl)board[kb.SwapWep_Key]).wasPressedThisFrame;
        Inv_pressed = ((KeyControl)board[kb.Inv_Key]).wasPressedThisFrame;
        LeftKey_Pressed = ((KeyControl)board[kb.Directional_Key[1]]).wasPressedThisFrame;
        RightKey_Pressed = ((KeyControl)board[kb.Directional_Key[0]]).wasPressedThisFrame;
        UpKey_Pressed = ((KeyControl)board[kb.Directional_Key[2]]).wasPressedThisFrame;
        DownKey_Pressed = ((KeyControl)board[kb.Directional_Key[3]]).wasPressedThisFrame;
        Held();
    }
    void KeyReleases()
    {
        Held();
        Atk1_released = ((KeyControl)board[kb.Atk1_Key]).wasReleasedThisFrame;
        Atk2_released = ((KeyControl)board[kb.Atk2_Key]).wasReleasedThisFrame;
        Atk3_released = ((KeyControl)board[kb.Atk3_Key]).wasReleasedThisFrame;
        Jump_released = ((KeyControl)board[kb.Jump_Key]).wasReleasedThisFrame;
        Dash_released = ((KeyControl)board[kb.Dash_Key]).wasReleasedThisFrame;
        SwapWep_released = ((KeyControl)board[kb.SwapWep_Key]).wasReleasedThisFrame;
        Inv_released = ((KeyControl)board[kb.Inv_Key]).wasReleasedThisFrame;
        LeftKey_Released = ((KeyControl)board[kb.Directional_Key[1]]).wasReleasedThisFrame;
        RightKey_Released = ((KeyControl)board[kb.Directional_Key[0]]).wasReleasedThisFrame;
        UpKey_Released = ((KeyControl)board[kb.Directional_Key[2]]).wasReleasedThisFrame;
        DownKey_Released = ((KeyControl)board[kb.Directional_Key[3]]).wasReleasedThisFrame;
        Held();
    }
    void ButtonPresses()
    {
        Held();
        Atk1_pressed = ((ButtonControl)pad[kb.Atk1_btn]).wasPressedThisFrame;
        Atk2_pressed = ((ButtonControl)pad[kb.Atk2_btn]).wasPressedThisFrame;
        Atk3_pressed = ((ButtonControl)pad[kb.Atk3_btn]).wasPressedThisFrame;
        Jump_pressed = ((ButtonControl)pad[kb.Jump_btn]).wasPressedThisFrame;
        Dash_pressed = ((ButtonControl)pad[kb.Dash_btn]).wasPressedThisFrame;
        SwapWep_pressed = ((ButtonControl)pad[kb.SwapWep_btn]).wasPressedThisFrame;
        Inv_pressed = ((ButtonControl)pad[kb.Inv_btn]).wasPressedThisFrame;
        Held();
    }
    void ButtonReleases()
    {
        Held();
        Atk1_released = ((ButtonControl)pad[kb.Atk1_btn]).wasReleasedThisFrame;
        Atk2_released = ((ButtonControl)pad[kb.Atk2_btn]).wasReleasedThisFrame;
        Atk3_released = ((ButtonControl)pad[kb.Atk3_btn]).wasReleasedThisFrame;
        Jump_released = ((ButtonControl)pad[kb.Jump_btn]).wasReleasedThisFrame;
        Dash_released = ((ButtonControl)pad[kb.Dash_btn]).wasReleasedThisFrame;
        SwapWep_released = ((ButtonControl)pad[kb.SwapWep_btn]).wasReleasedThisFrame;
        Inv_released = ((ButtonControl)pad[kb.Inv_btn]).wasReleasedThisFrame;
        Held();
    }
    void KeyHelds()
    {
        switch(curControlType)
        {
            case ControlType.controller:
                ButtonPresses();
                ButtonReleases();
                break;
            case ControlType.keyboard:
                KeyPresses();
                KeyReleases();
                break;
        }
    }
    void Held()
    {
        if (Atk1_pressed)
            Atk1_held = true;
        if (Atk2_pressed)
            Atk2_held = true;
        if (Atk3_pressed)
            Atk3_held = true;
        if (Jump_pressed)
            Jump_held = true;
        if (Dash_pressed)
            Dash_held = true;
        if (SwapWep_pressed)
            SwapWep_held = true;
        if (Inv_pressed)
            Inv_held = true;
        if (LeftKey_Pressed)
            LeftKey_Held = true;
        if (RightKey_Pressed)
            RightKey_Held = true;
        if (UpKey_Pressed)
            UpKey_Held = true;
        if (DownKey_Pressed)
            DownKey_Held = true;
        if (Atk1_released)
            Atk1_held = false;
        if (Atk2_released)
            Atk2_held = false;
        if (Atk3_released)
            Atk3_held = false;
        if (Jump_released)
            Jump_held = false;
        if (Dash_released)
            Dash_held = false;
        if (SwapWep_released)
            SwapWep_held = false;
        if (Inv_released)
            Inv_held = false;
        if (LeftKey_Released)
            LeftKey_Held = false;
        if (RightKey_Released)
            RightKey_Held = false;
        if (UpKey_Released)
            UpKey_Held = false;
        if (DownKey_Released)
            DownKey_Held = false;
    }
}

[System.Serializable]
public class KeyBinding
{
    [Header("Set your button bindings here if you are using controller and do not leave anything blank")]
    public string Atk1_btn;
    public string Atk2_btn;
    public string Atk3_btn;
    public string Jump_btn;
    public string Dash_btn;
    public string SwapWep_btn;
    public string Inv_btn;

    [Header("Set your keybindings for keyboard here and do not leave anything blank")]
    public string Atk1_Key;
    public string Atk2_Key;
    public string Atk3_Key;
    public string Jump_Key;
    public string Dash_Key;
    public string SwapWep_Key;
    public string Inv_Key;
    public string[] Directional_Key;
    
}
