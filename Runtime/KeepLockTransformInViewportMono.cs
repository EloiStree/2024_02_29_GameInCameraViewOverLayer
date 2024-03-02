using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLockTransformInViewportMono : MonoBehaviour
{
    public Camera m_cameraToUse;
    public Transform m_toAffect;
    public float m_offsetBorder = 0.05f;
    public float m_offsetNearPlane = 1;
    public float m_offsetfarPlane = 1;
    public bool m_useDebugDraw = false;
    public  void RecenterInViewportIfCenterOut()
    {
        if (m_cameraToUse == null)
            return;

        Vector3 worldPosition = m_toAffect.transform.position;
        Vector3 localToCamera=  m_cameraToUse.transform.InverseTransformPoint(m_toAffect.position);


        Vector3 m_leftDown= m_cameraToUse.ViewportToWorldPoint(new Vector3(0,0,localToCamera.z));
        Vector3 m_rightTop = m_cameraToUse.ViewportToWorldPoint(new Vector3(1, 1, localToCamera.z));

        if (m_useDebugDraw) { 
            Debug.DrawLine(m_leftDown, m_rightTop, Color.white, Time.deltaTime);
            Debug.DrawLine(m_leftDown, m_rightTop, Color.white, Time.deltaTime);
            Debug.DrawLine(worldPosition, m_rightTop, Color.white, Time.deltaTime);
            Debug.DrawLine(m_leftDown, worldPosition, Color.white, Time.deltaTime);
        }

        Vector3 viewPort = m_cameraToUse.WorldToViewportPoint(m_toAffect.position);
        if (viewPort.x < 0)
        {
            m_toAffect.position = ChangeXto(m_toAffect.position, m_leftDown.x);
        }
        if (viewPort.y < 0)
        {
            m_toAffect.position = ChangeYto(m_toAffect.position, m_leftDown.y);
        }
        if (viewPort.x > 1)
        {
            m_toAffect.position = ChangeXto(m_toAffect.position, m_rightTop.x);
        }
        if (viewPort.y > 1)
        {
            m_toAffect.position = ChangeYto(m_toAffect.position, m_rightTop.y);
        }
        if (viewPort.z > m_cameraToUse.farClipPlane)
        {
            m_toAffect.position = m_cameraToUse.transform.position + m_cameraToUse.transform.forward * (m_cameraToUse.farClipPlane - m_offsetfarPlane);      }
        if (viewPort.z < m_cameraToUse.nearClipPlane)
        {
            m_toAffect.position = m_cameraToUse.transform.position + m_cameraToUse.transform.forward * (m_cameraToUse.nearClipPlane + m_offsetNearPlane);
        }
    }

    private Vector3 ChangeXto(Vector3 position, float x)
    {
        Vector3 t = position;
        t.x = x;
        return t;

    }
    private Vector3 ChangeYto(Vector3 position, float y)
    {
        Vector3 t = position;
        t.y = y;
        return t;

    }

    public bool m_useUpdate;
    private void Update()
    {
        if(!m_useUpdate) return;
        RecenterInViewportIfCenterOut();
    }
}
