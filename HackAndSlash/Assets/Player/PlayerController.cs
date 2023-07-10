using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public Vector2 mousePos;
    private Rigidbody rb;
    private bool isMouseUIAttached;
    public Vector3 worldPos;
    private Ray clickRay;
    private RaycastHit clickHit;
    public LayerMask groundLayer;
    public float moveSpeed;
    private Collider playerCol;
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCol = GetComponent<Collider>();
    }
    private void Update()
    {
        isMouseUIAttached = EventSystem.current.IsPointerOverGameObject();
        transform.position = Vector3.MoveTowards(transform.position,worldPos + (Vector3.up*playerCol.bounds.extents.y),moveSpeed*Time.deltaTime);
    }
    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!isMouseUIAttached)
            {
                Debug.Log("UI¾È´ê¾Ò¾î");
                clickRay = Camera.main.ScreenPointToRay(mousePos);
                if(Physics.Raycast(clickRay, out clickHit, Mathf.Infinity, groundLayer))
                {
                    worldPos = clickHit.point;
                    if(clickHit.collider.TryGetComponent<Stair>(out Stair stairCompo))
                    {
                        foreach (var item in stairCompo.waypoints)
                        {
                            
                        }
                    }
                }
            }
        }
    }
    public void OnQuaryMousePosition(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }
}
