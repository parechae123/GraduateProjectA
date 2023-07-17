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
    public bool isMoveMentOrded = false;
    public Vector3 worldPos;
    public Vector3 tempWorldPos;
    public RaycastHit groundHit;
    private Ray clickRay;
    private RaycastHit clickHit;

    public LayerMask groundLayer;
    public float moveSpeed;
    private Collider cc;
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<Collider>();
    }
    private void Update()
    {
        isMouseUIAttached = EventSystem.current.IsPointerOverGameObject();
        if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.down), out groundHit, cc.bounds.size.y * 1010, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, groundHit.point.y + cc.bounds.extents.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(worldPos.x, transform.position.y, worldPos.z), moveSpeed * Time.deltaTime);

        }
        if (isMoveMentOrded)
        {
            clickRay = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(clickRay, out clickHit, Mathf.Infinity, groundLayer))
            {
                worldPos = clickHit.point;
                if(clickHit.collider.gameObject.TryGetComponent<Stair>(out Stair strcompo))
                {
                    tempWorldPos = worldPos;
                    worldPos = strcompo.GetStairWayPoint(clickHit.point);
                    Debug.Log(strcompo.GetStairWayPoint(transform.position));
                }
            }
        }
        if (transform.position.x == worldPos.x && transform.position.z == worldPos.z)
        {
            if(tempWorldPos != Vector3.zero)
            {
                worldPos = tempWorldPos;
                tempWorldPos= Vector3.zero;
            }
            isMoveMentOrded = false;
        }
    }
    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (!isMouseUIAttached)
        {
            if (ctx.performed)
            {
                isMoveMentOrded = true;
            }
            else if (ctx.canceled)
            {
                isMoveMentOrded = false;
            }

        }
    }
    public void OnQuaryMousePosition(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }
}
