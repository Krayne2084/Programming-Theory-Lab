using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xInput;
    float yInput;
    Vector2 vectorInput;
    [SerializeField] float speed = 3f;
    [SerializeField] List<GameObject> portalPrefabs;

    Transform _transform;
    Rigidbody2D _rb;
    CircleCollider2D _collider;
    Camera mainCam;
    MainManager mainManager;
    private void Start()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        mainManager = FindObjectOfType<MainManager>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (GameManager.isPaused)
        {
            return;
        }
        HandleMovement();
        HandleRotation();
        if (Input.GetMouseButtonDown(0))
        {
            ShootPortal();
        }
    }

    void ShootPortal()
    {
        //setactive a portal fro the pool of portals
        portalPrefabs[0].SetActive(true);
        GameObject portal = portalPrefabs[0];
        portalPrefabs.RemoveAt(0);
        portalPrefabs.Add(portal);
        portal.transform.SetParent(null);
        MainManager.CursorSprite.enabled = false;
    }

    void HandleMovement()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        vectorInput = new Vector2(xInput, yInput).normalized;
       
        _rb.AddForce(vectorInput * Time.deltaTime * speed, ForceMode2D.Force);

        KeepOnScreen();
    }
    void KeepOnScreen()
    {
        Vector2 screenBounds = mainCam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float xBoundPos = Mathf.Clamp(_transform.position.x, -screenBounds.x + _collider.radius, screenBounds.x - _collider.radius);
        float yBoundPos = Mathf.Clamp(_transform.position.y, -screenBounds.y + _collider.radius, screenBounds.y - _collider.radius);

        _transform.position = new Vector3(xBoundPos, yBoundPos, _transform.position.z);
        if (_transform.position.x == screenBounds.x || _transform.position.x == -screenBounds.x)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
        if (_transform.position.y == screenBounds.y || _transform.position.y == -screenBounds.y)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }
    }
    void HandleRotation()
    {
        Vector3 mousePos =  mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - _transform.position;
        _transform.rotation = Quaternion.Euler(Vector3.forward * ((Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg) - 90f));
    }
    public void Kill()
    {
        mainManager.GameOver();
    }
}
