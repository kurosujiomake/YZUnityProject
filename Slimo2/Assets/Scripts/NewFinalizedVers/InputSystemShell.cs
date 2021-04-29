using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Controls;

public class InputSystemShell : MonoBehaviour
{
    Dictionary<string, InputState> Input = new Dictionary<string, InputState>();
    public InputMapping IMap = new InputMapping();
    public Gamepad pad;
    // Start is called before the first frame update
    void Awake()
    {
        Assign();
        pad = Gamepad.current;
    }

    void Update()
    {
        if(WasPressed("Jump"))
        {
            print("Jump Pressed");
        }
    }

    public bool WasPressed(string which)
    {
        return Input[which].buttonMapping.wasPressedThisFrame;
    }

    void Assign()
    {
        Input["Jump"] = new InputState();
        InputState i = new InputState();
        i.buttonMapping = pad[IMap.gButton];
        Input["Jump"] = i;
        Input["Atk1"] = new InputState();
        Input["Dash"] = new InputState();
    }
}
[System.Serializable]
public struct InputMapping
{
    public GamepadButton gButton;
}
public struct InputState
{
    public ButtonControl buttonMapping;
}

struct DictionaryEntry
{
    string key;
    int value;
}
