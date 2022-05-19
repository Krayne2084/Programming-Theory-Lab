using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    Transform _transform;
    Camera cam;
    private void Start()
    {
        _transform = transform;
        cam = Camera.main;
    }
    private void LateUpdate()
    {
        if (GameManager.isPaused || GameManager.hasGameEnded)
        {
            return;
        }
        _transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, 0);
    }
}
