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
        ResetIsHeld();
    }
    public bool GetButtonDown(string which)
    {
        bool b = false;
        InputState s = Input[which];
        switch (control)
        {
            case ControlType.Both:
                if (Input[which].buttonMapping.wasPressedThisFrame || Input[which].keyMapping.wasPressedThisFrame)
                {
                    s.isHeld = true;
                    Input[which] = s;
                    b = true;
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
                    b = true;
                }
                if (Input[which].buttonMapping.wasReleasedThisFrame)
                {
                    s.isHeld = false;
                    Input[which] = s;
                }

                break;
            case ControlType.Keyboard:
                if (Input[which].keyMapping.wasPressedThisFrame)
                {
                    s.isHeld = true;
                    Input[which] = s;
                    b = true;
                }
                if (Input[which].keyMapping.wasReleasedThisFrame)
                {
                    s.isHeld = false;
                    Input[which] = s;
                }
                break;
        }
        return b;
    }
    public bool GetButtonHeld(string which)
    {
        bool b = false;
        GetButtonDown(which);
        if(Input[which].isHeld)
        {
            b = true;
        }
        else if(GetButtonUp(which))
        {
            b = false;
        }
        return b;
    }
    
    public bool GetButtonUp(string which)
    {
        bool b = false;
        InputState s = Input[which];
        switch (control)
        {
            case ControlType.Both:
                if (Input[which].buttonMapping.wasReleasedThisFrame || Input[which].keyMapping.wasReleasedThisFrame)
                {
                    s.isHeld = false;
                    Input[which] = s;
                    b = true;
                }
                break;
            case ControlType.Controller:
                if (Input[which].buttonMapping.wasReleasedThisFrame)
                {
                    s.isHeld = false;
                    Input[which] = s;
                    b = true;
                }
                    
                break;
            case ControlType.Keyboard:
                if (Input[which].keyMapping.wasReleasedThisFrame)
                {
                    s.isHeld = false;
                    Input[which] = s;
                    b = true;
                }
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
                DuoControlChecks();
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
    void DuoControlChecks()
    {
        if(Mathf.Abs(cx) > Mathf.Abs(x)) //if controller's input is greater, aka keyboard not recieveing anything
        {
            leftStickRead.x = cx;
        }
        if(Mathf.Abs(x) > Mathf.Abs(cx))
        {
            leftStickRead.x = x;
        }
        if(cx == 0 && x == 0)
        {
            leftStickRead.x = 0;
        }
        if(Mathf.Abs(cy) > Mathf.Abs(y))
        {
            leftStickRead.y = cy;
        }
        if(Mathf.Abs(y) > Mathf.Abs(cy))
        {
            leftStickRead.y = y;
        }
        if(cy == 0 && y == 0)
        {
            leftStickRead.y = 0;
        }
    }
    void KeyRead()
    {
        if (board != null)
        {
            x = hortCheck();
            y = vertCheck();
        }
    }
    float hortCheck()
    {
        float f = 0;
        if (GetButtonHeld("left"))
            f = -1f;
        if (GetButtonHeld("right"))
            f = 1;
        return f;
    }
    float vertCheck()
    {
        float f = 0;
        if (GetButtonHeld("down"))
            f = -1f;
        if (GetButtonHeld("up"))
            f = 1;
        return f;
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
    public string GetDirectionL()
    {
        return DirReturn[ReturnDir(0)];
    }
    public bool GetAnyKey()
    {
        bool b = false;
        switch(control)
        {
            case ControlType.Controller:
                b = AnyButton();
                break;
            case ControlType.Keyboard:
                b = board.anyKey.wasPressedThisFrame;
                break;
            case ControlType.Both:
                if (AnyButton() || board.anyKey.wasPressedThisFrame)
                    b = true;
                break;
        }
        return b;
    }
    bool AnyButton() //a function that checks if any controller button was pressed.
    {
        bool b = false;
        foreach (GamepadButton g in Enum.GetValues(typeof(GamepadButton)))
        {
            if (pad[g].wasPressedThisFrame)
                b = true;
        }
        return b;
    }
    public float ReturnAxis(string which, string axis)
    {
        float f = 0;
        StickReads();
        switch(which)
        {
            case "left":
                if (axis == "hori")
                    f = leftStickRead.x;
                if (axis == "vert")
                    f = leftStickRead.y;
                break;
            case "right":
                if (axis == "hori")
                    f = rightStickRead.x;
                if (axis == "vert")
                    f = rightStickRead.y;
                break;
        }
        return f;
    }
    public void SetControls(int type)
    {
        switch(type)
        {
            case 0:
                control = ControlType.none;
                break;
            case 1:
                control = ControlType.Keyboard;
                break;
            case 2:
                control = ControlType.Controller;
                break;
            case 3:
                control = ControlType.Both;
                break;
        }
    }
    void ResetIsHeld()
    {
        GetButtonUp("Atk1");
        GetButtonUp("Atk2");
        GetButtonUp("Atk2_2");
        GetButtonUp("Atk3");
        GetButtonUp("Jump");
        GetButtonUp("Dash");
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
