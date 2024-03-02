using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawViewportOfCameraMono : MonoBehaviour
{

    public Camera m_camera;


    public bool m_drawExtraLines;
    void Update()
    {
        if (m_camera == null)
            m_camera = Camera.main;
        if (m_camera == null)
            return;

        DrawBorder(0, 0);
        DrawBorder(1, 0);
        DrawBorder(0, 1);
        DrawBorder(1, 1);
        DrawRegionHorizontal(0);
        DrawRegionHorizontal(1);
        if (m_drawExtraLines)
        {
            DrawRegionHorizontal(0.5f);
            DrawRegionHorizontal(0.75f);
            DrawRegionHorizontal(0.25f);
        }


    }

    private void DrawRegionHorizontal(float percent)
    {
        DrawBorder(0, 0, 0, 1, percent);
        DrawBorder(0, 0, 1, 0, percent);
        DrawBorder(0, 1, 1, 1, percent);
        DrawBorder(1, 0, 1, 1, percent);
    }

    private void DrawBorder(int v1, int v2, int v3, int v4, float v5)
    {
        float percent = m_camera.nearClipPlane + v5 * (m_camera.farClipPlane - m_camera.nearClipPlane);
        Vector3 start = m_camera.ViewportToWorldPoint(new Vector3(v1, v2, percent));
        Vector3 end = m_camera.ViewportToWorldPoint(new Vector3(v3,v4, percent));
        Debug.DrawLine(start, end, Color.yellow, Time.deltaTime);

    }

    private void DrawBorder(int leftRight, int downTop)
    {
        Vector3 start = m_camera.ViewportToWorldPoint(new Vector3(leftRight, downTop, m_camera.nearClipPlane));
        Vector3 end = m_camera.ViewportToWorldPoint(new Vector3(leftRight, downTop, m_camera.farClipPlane));
        Debug.DrawLine(start, end, Color.yellow, Time.deltaTime);
    }
}
