using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Controls;
using System;

public class InputSystemShell : MonoBehaviour
{
    Dictionary<string, InputState> Input = new Dictionary<string, InputState>();
    public ControlType control;
    public Gamepad pad;
    public Keyboard board;
    public InputMapping[] InputMap;
    public InputMapping[] KeyBoardDirection;

    [Header("Analogue Stick Vars")]
    Dictionary<Direction, string> DirReturn = new Dictionary<Direction, string>();
    public DirMapping[] DirectionMap;
    public Direction curLStickDir;
    public Direction curRStickDir;
    public Vector2 leftStickRead;
    public Vector2 rightStickRead;
    public float[] stickDeadzoneL;
    public float[] stickDeadzoneR;

    //preset vars to get the stick reads so i dont have to initialize new ones each time
    private float x,y;
    private float cx, cy;
    void Awake()
    {
        DirAssign();
        pad = Gamepad.current;
        board = Keyboard.current;
        AutoAssign();
        KeyDirectionAssign();
    }

    void Update()
    {
        if(GetButtonDown("Jump"))
        {
            print("Jump Pressed");
        }
        if(GetButtonHeld("Jump"))
        {
            print("Jump is held");
        }
        curLStickDir = ReturnDir(0);
        if (GetButtonHeld("left"))
            print("left is held");
        if (GetButtonHeld("down"))
            print("down is held");
    }

    public bool GetButtonDown(string which)
    {
        bool b = false;
        switch (control)
        {
            case ControlType.Both:
                if (Input[which].buttonMapping.wasPressedThisFrame || Input[which].keyMapping.wasPressedThisFrame)
                    b = true;
                break;
            case ControlType.Controller:
                if (Input[which].buttonMapping.wasPressedThisFrame)
                    b = true;
                break;
            case ControlType.Keyboard:
                if (Input[which].keyMapping.wasPressedThisFrame)
                    b = true;
                break;
        }
        return b;
    }
    public bool GetButtonHeld(string which)
    {
        InputState s = Input[which];
        switch(control)
        {
            case ControlType.Both:
                if (Input[which].buttonMapping.wasPressedThisFrame || Input[which].keyMapping.wasPressedThisFrame)
                {
                    s.isHeld = true;
                    Input[which] = s;
                }
                if (Input[which].buttonMapping.wasReleasedThisFrame || Input[which].keyMapping.wasReleasedThisFrame)
                {
                    s.isHeld = false;
                    Input[which] = s;
                }
                break;
            case ControlType.Controller:
                if (Input[which].buttonMapping.wasPressedThisFrame)
                {
                    s.isHeld = true;
                    Input[which] = s;
                }
                if (Input[which].buttonMapping.wasReleasedThisFrame)
                {
                    s.isHeld = false;
                    Input[which] = s;
                }
                break;
            case ControlType.Keyboard:
                if(Input[which].keyMapping.wasPressedThisFrame)
                {
                    s.isHeld = true;
                    Input[which] = s;
                }
                if(Input[which].keyMapping.wasReleasedThisFrame)
                {
                    s.isHeld = false;
                    Input[which] = s;
                }
                break;
        }
        return Input[which].isHeld;
    }
    public bool GetButtonUp(string which)
    {
        bool b = false;
        switch (control)
        {
            case ControlType.Both:
                if (Input[which].buttonMapping.wasReleasedThisFrame || Input[which].keyMapping.wasReleasedThisFrame)
                    b = true;
                break;
            case ControlType.Controller:
                if (Input[which].buttonMapping.wasReleasedThisFrame)
                    b = true;
                break;
            case ControlType.Keyboard:
                if (Input[which].keyMapping.wasReleasedThisFrame)
                    b = true;
                break;
        }
        return b;
    }
    void AutoAssign()
    {
        foreach(var m in InputMap)
        {
            var s = new InputState();
            if(pad != null)
                s.buttonMapping = pad[m.gButton];
            if(board != null)
                s.keyMapping = board[m.kKey];
            Input[m.ButtonName] = s;
        }
        
    }
    void DirAssign()
    {
        foreach(var d in DirectionMap)
        {
            DirReturn[d.direction] = d.returnString;
        }
    }
    void KeyDirectionAssign()
    {
        foreach (var m in KeyBoardDirection)
        {
            var s = new InputState();
            if (pad != null)
                s.buttonMapping = pad[m.gButton];
            if (board != null)
                s.keyMapping = board[m.kKey];
            Input[m.ButtonName] = s;
        }
    }
    void StickReads()
    {
        switch(control)
        {
            case ControlType.none:
                break;
            case ControlType.Controller:
                StickRead();
                leftStickRead.x = cx;
                leftStickRead.y = cy;
                break;
            case ControlType.Keyboard:
                KeyRead();
                leftStickRead.x = x;
                leftStickRead.y = y;
                break;
            case ControlType.Both:
                StickRead();
                KeyRead();
                if(Mathf.Abs(cx) > x) //if the controller value is higher, then sets the left stick output to controller
                {
                    leftStickRead.x = cx;
                }
                if(Mathf.Abs(cx) <= x) //if the keyboard value is higher, then sets the left stick output to keyboard
                {
                    leftStickRead.x = x;
                }
                if(Mathf.Abs(cy) > y) //same as above but for y
                {
                    leftStickRead.y = cy;
                }
                if(Mathf.Abs(cy) <= y) //same as above but for y
                {
                    leftStickRead.y = y;
                }
                break;
        }
        
        

    }
    void StickRead()
    {
        if (pad != null)
        {
            cx = pad.leftStick.ReadValue().x;
            cy = pad.leftStick.ReadValue().y;
            rightStickRead = pad.rightStick.ReadValue();
        }
    }
    void KeyRead()
    {
        float fx = 0,fy = 0;
        if (board != null)
        {
            if (GetButtonHeld("left"))
                fx = -1;
            if (GetButtonHeld("right"))
                fx = 1;
            if (GetButtonHeld("up"))
                fy = 1;
            if (GetButtonHeld("down"))
                fy = -1;
            
        }
        x = fx;
        y = fy;
    }
    Direction ReturnDir(int which)
    {
        StickReads();
        Direction d = Direction.neutral;
        switch(which)
        {
            case 0: //left stick
                if (Mathf.Abs(leftStickRead.x) > Mathf.Abs(leftStickRead.y)) //input is biased horizontally
                {
                    if(leftStickRead.x < -stickDeadzoneL[0])
                    {
                        d = Direction.left;
                    }
                    if(leftStickRead.x > stickDeadzoneL[0])
                    {
                        d = Direction.right;
                    }
                }
                if (Mathf.Abs(leftStickRead.x) < Mathf.Abs(leftStickRead.y)) //input is biased vertically
                {
                    if (leftStickRead.y < -stickDeadzoneL[1])
                    {
                        d = Direction.down;
                    }
                    if (leftStickRead.y > stickDeadzoneL[1])
                    {
                        d = Direction.up;
                    }
                }
                if(Mathf.Abs(leftStickRead.x) > 0.5f && Mathf.Abs(leftStickRead.y) > 0.5f) //diagonal biases
                {
                    if(leftStickRead.y > 0) //uptilting half directions
                    {
                        if(leftStickRead.x < 0)
                        {
                            d = Direction.upleft;
                        }
                        if(leftStickRead.x > 0)
                        {
                            d = Direction.upright;
                        }
                    }
                    if(leftStickRead.y < 0) //downtilting half directions
                    {
                        if(leftStickRead.x < 0)
                        {
                            d = Direction.downleft;
                        }
                        if(leftStickRead.x > 0)
                        {
                            d = Direction.downright;
                        }
                    }
                }
                    break;
            case 1: //right stick
                if (Mathf.Abs(rightStickRead.x) > Mathf.Abs(rightStickRead.y)) //input is biased horizontally
                {
                    if (rightStickRead.x < -stickDeadzoneR[0])
                    {
                        d = Direction.left;
                    }
                    if (rightStickRead.x > stickDeadzoneR[0])
                    {
                        d = Direction.right;
                    }
                }
                if (Mathf.Abs(rightStickRead.x) < Mathf.Abs(rightStickRead.y)) //input is biased vertically
                {
                    if (rightStickRead.y < -stickDeadzoneR[1])
                    {
                        d = Direction.down;
                    }
                    if (rightStickRead.y > stickDeadzoneR[1])
                    {
                        d = Direction.up;
                    }
                }
                if (Mathf.Abs(rightStickRead.x) > 0.5f && Mathf.Abs(rightStickRead.y) > 0.5f) //diagonal biases
                {
                    if (rightStickRead.y > 0) //uptilting half directions
                    {
                        if (rightStickRead.x < 0)
                        {
                            d = Direction.upleft;
                        }
                        if (rightStickRead.x > 0)
                        {
                            d = Direction.upright;
                        }
                    }
                    if (rightStickRead.y < 0) //downtilting half directions
                    {
                        if (rightStickRead.x < 0)
                        {
                            d = Direction.downleft;
                        }
                        if (rightStickRead.x > 0)
                        {
                            d = Direction.downright;
                        }
                    }
                }
                break;
        }
        return d;
    }
}
[Serializable]
public struct InputMapping
{
    public string ButtonName;
    public GamepadButton gButton;
    public Key kKey;
}
public struct InputState
{
    public ButtonControl buttonMapping;
    public KeyControl keyMapping;
    public bool isHeld;
}
[Serializable]
public struct DirMapping
{
    public string returnString;
    public Direction direction;
}
public enum Direction
{
    neutral,
    up,
    down,
    left,
    right,
    upleft,
    upright,
    downleft,
    downright
}
public enum ControlType
{
    none,
    Keyboard,
    Controller,
    Both
}
