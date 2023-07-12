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
    private bool isMoveMentOrded = false;
    public Vector3 worldPos;
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
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(worldPos.x,transform.position.y,worldPos.z),moveSpeed*Time.deltaTime);
        {
            if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.down), out groundHit, cc.bounds.size.y * 1010, groundLayer))
            {
                transform.position = new Vector3(transform.position.x, groundHit.point.y + cc.bounds.extents.y, transform.position.z);
            }
            if (isMoveMentOrded)
            {

                if (transform.position.x == worldPos.x && transform.position.z == worldPos.z)
                {
                    isMoveMentOrded = false;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(worldPos.x, transform.position.y, worldPos.z), 2 * Time.deltaTime);
        }
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
                    isMoveMentOrded = true;
                }
            }
        }
    }
    public void OnQuaryMousePosition(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }
}
