using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 相机管理器
/// </summary>
public class CameraManager : MonoBehaviour
{
    private PlayerManager playerManager;

    public enum CameraCtrlState
    {
        isCtrl,
        NoCtrl
    }

    public CameraCtrlState cameraCtrlState = CameraCtrlState.NoCtrl;


    [SerializeField]
    //与目标物体距离距离
    [Range(0.5f,15)]
    private float distance;

    private const float MAX_DISTANCE = 15;
    private const float MIN_DISTANCE = 1.5f;

    [SerializeField]
    private float smoothTime = 0.3f;

    [SerializeField]
    private float moveSpeed = 0.3f;

    private Vector3 smoothValue = Vector3.zero;

    private Vector2 lastPoint;
    private Vector2 nowPoint;

    [SerializeField]
    private bool mouse1FirstDown;

    private void Start()
    {
        InitSet();
        SetCameraPos();
    }

    private void Update()
    {
        SetCameraDistance();
    }

    private void FixedUpdate()
    {
        transform.LookAt(playerManager.transform);
        transform.position = Vector3.SmoothDamp(transform.position,SetCameraPos(),ref smoothValue, smoothTime);
        SetCameraRotX();
    }

    private void InitSet()
    {
        SetPlayer();
    }


    private PlayerManager SetPlayer()
    {
        if(playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
        playerManager.cameraManager = this;
        return playerManager;
    }


    private Vector3 SetCameraPos()
    {
        Vector3 v = (transform.position - playerManager.transform.position).normalized;
        Vector3 pos = distance * v + playerManager.transform.position;
        return pos;
    }


    /// <summary>
    /// 设置摄像机据目标点距离
    /// </summary>
    private void SetCameraDistance()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (distance <= MIN_DISTANCE)
                return;
            distance -= 0.5f;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (distance >= MAX_DISTANCE)
                return;
            distance += 0.5f;
        }
    }

    private void SetCameraRotX()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mouse1FirstDown = true;
            cameraCtrlState = CameraCtrlState.isCtrl;
        }

        if (Input.GetMouseButton(1))
        {
            nowPoint = Input.mousePosition;
            if (mouse1FirstDown)
            {
                lastPoint = nowPoint;
                mouse1FirstDown = false;
            }
            if ((nowPoint - lastPoint).sqrMagnitude != 0)
            {
                float x = nowPoint.x - lastPoint.x;
                float y = nowPoint.y - lastPoint.y;
                transform.RotateAround(playerManager.transform.position, transform.right, y * Time.deltaTime * moveSpeed);
                transform.RotateAround( playerManager.transform.position, Vector3.up, x * Time.deltaTime * moveSpeed);
            }
            lastPoint = nowPoint;
        }

        if (Input.GetMouseButtonUp(1))
        {
            cameraCtrlState = CameraCtrlState.NoCtrl;
        }
    }

    private void SetCameraConfig(float distance)
    {
        this.distance = distance;
    }
}
