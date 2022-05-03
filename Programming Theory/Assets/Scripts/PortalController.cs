using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] float speed = 6f;
    [SerializeField] Transform pool;
    [SerializeField] bool isDynamic;
    private Transform _transform;
    private Vector3 startPos;
    private Quaternion startRot;
    private Camera mainCam;
    private void Start()
    {
        
        mainCam = FindObjectOfType<Camera>();
        _transform = transform;
        startPos = Vector3.zero;
        startRot = Quaternion.Euler(0, 0, 90);
    }
    private void Awake()
    {
    }

    private void FixedUpdate()
    {
        if (!isDynamic)
        {
            return;
        }
        Vector2 screenBounds = mainCam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        _transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (Mathf.Abs(_transform.position.x) > screenBounds.x || Mathf.Abs(_transform.position.y) > screenBounds.y)
        {
            DisablePortal();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Damage();
            Teleport(enemy.gameObject);
            enemy.Stun();
        }
    }

    void Teleport(GameObject thing)
    {
        Transform worldPortal = MainManager.WorldPortal.transform;
        thing.transform.SetPositionAndRotation(worldPortal.position, worldPortal.rotation);
        thing.GetComponent<Rigidbody2D>().AddRelativeForce(worldPortal.right * speed, ForceMode2D.Impulse);
    }

    void DisablePortal()
    {
        _transform.SetParent(pool);
        _transform.localPosition = startPos;
        _transform.localRotation = startRot;
        MainManager.CursorSprite.enabled = true;
        gameObject.SetActive(false);
    }
}
