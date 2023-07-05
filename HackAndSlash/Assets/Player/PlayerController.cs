using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    private Vector2 mousePos;
    private Rigidbody rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Camera.main.ScreenPointToRay(mousePos);
            }
        }
    }
    public void OnQuaryMousePosition(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }
}
