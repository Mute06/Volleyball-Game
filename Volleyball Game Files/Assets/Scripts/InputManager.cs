using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Input Manager");
                go.AddComponent<InputManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    // Horizontal Input
    private float _horizontalInput;
    public float HorizontalInput
    {
        get { return _horizontalInput; }
    }

    // Jump button
    private bool _getJump;
    public bool GetJump
    {
        get { return _getJump; }
    }

    // Switch Button
    private bool _switchButton;
    public bool SwitchButton
    {
        get
        {
            return _switchButton;
        }
    }

    private void Update()
    {
        _horizontalInput = SimpleInput.GetAxis("Horizontal");
        _getJump = SimpleInput.GetButtonDown("Jump");
        _switchButton = Input.GetKeyDown(KeyCode.E);
    }
}
