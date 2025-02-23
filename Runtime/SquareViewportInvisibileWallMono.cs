using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class SquareViewportInvisibileWallMono : MonoBehaviour
{
    public Camera m_cameraToUse;
    public Transform m_leftWall;
    public Transform m_topWall;
    public Transform m_rightWall;
    public Transform m_downWall;
    public Transform m_ground;
    public Transform [] m_setTargetsToCenter;


    public bool m_useUpdateRefresh = true;
    public bool m_refresh;


    public float m_groundAdjustmentHeight = 0.05f;
    private void Update()
    {
        if (m_useUpdateRefresh)
            Refresh();
    }
    private void OnValidate()
    {
        Refresh();
    }
    void Refresh()
    {


        if (m_cameraToUse == null)
            return;

        Vector3 m_start = m_cameraToUse.ViewportToWorldPoint(new Vector3(0, 0.5f, m_cameraToUse.nearClipPlane));
        Vector3 m_end = m_cameraToUse.ViewportToWorldPoint(new Vector3(0, 0.5f, m_cameraToUse.farClipPlane));
        Vector3 m_left = m_cameraToUse.ViewportToWorldPoint(new Vector3(0, 1, m_cameraToUse.farClipPlane));
        Vector3 m_right = m_cameraToUse.ViewportToWorldPoint(new Vector3(0, 0, m_cameraToUse.farClipPlane));

        Debug.DrawLine(m_start, m_end, Color.cyan, Time.deltaTime);
        Vector3 dir = m_end - m_start;
        m_leftWall.position = m_start + (dir / 2f);
        m_leftWall.localScale = new Vector3(1, Vector3.Distance(m_left, m_right), dir.magnitude);
        m_leftWall.rotation = Quaternion.LookRotation(dir, m_cameraToUse.transform.up);



        m_start = m_cameraToUse.ViewportToWorldPoint(new Vector3(1, 0.5f, m_cameraToUse.nearClipPlane));
        m_end = m_cameraToUse.ViewportToWorldPoint(new Vector3(1, 0.5f, m_cameraToUse.farClipPlane));
        m_left = m_cameraToUse.ViewportToWorldPoint(new Vector3(1, 1, m_cameraToUse.farClipPlane));
        m_right = m_cameraToUse.ViewportToWorldPoint(new Vector3(1, 0, m_cameraToUse.farClipPlane));

        Debug.DrawLine(m_start, m_end, Color.cyan, Time.deltaTime);
        dir = m_end - m_start;
        m_rightWall.position = m_start + (dir / 2f);
        m_rightWall.localScale = new Vector3(1, Vector3.Distance(m_left, m_right), dir.magnitude);
        m_rightWall.rotation = Quaternion.LookRotation(dir, m_cameraToUse.transform.up) * Quaternion.Euler(0, 0, 180);




        m_start = m_cameraToUse.ViewportToWorldPoint(new Vector3(0.5f, 0, m_cameraToUse.nearClipPlane));
        m_end = m_cameraToUse.ViewportToWorldPoint(new Vector3(0.5f, 0, m_cameraToUse.farClipPlane));
        m_left = m_cameraToUse.ViewportToWorldPoint(new Vector3(0, 0, m_cameraToUse.farClipPlane));
        m_right = m_cameraToUse.ViewportToWorldPoint(new Vector3(1, 0, m_cameraToUse.farClipPlane));

        Debug.DrawLine(m_start, m_end, Color.cyan, Time.deltaTime);
        dir = m_end - m_start;
        m_downWall.position = m_start + (dir / 2f);
        m_downWall.localScale = new Vector3( Vector3.Distance(m_left, m_right), 1, dir.magnitude);
        m_downWall.rotation = Quaternion.LookRotation(dir, m_cameraToUse.transform.up) * Quaternion.Euler(0, 0, 180);




        m_start = m_cameraToUse.ViewportToWorldPoint(new Vector3(0.5f, 1, m_cameraToUse.nearClipPlane));
        m_end = m_cameraToUse.ViewportToWorldPoint(new Vector3(0.5f, 1, m_cameraToUse.farClipPlane));
        m_left = m_cameraToUse.ViewportToWorldPoint(new Vector3(0, 1, m_cameraToUse.farClipPlane));
        m_right = m_cameraToUse.ViewportToWorldPoint(new Vector3(1, 1, m_cameraToUse.farClipPlane));

        Debug.DrawLine(m_start, m_end, Color.cyan, Time.deltaTime);
        dir = m_end - m_start;
        m_topWall.position = m_start + (dir / 2f);
        m_topWall.localScale = new Vector3(Vector3.Distance(m_left, m_right), 1, dir.magnitude);
        m_topWall.rotation = Quaternion.LookRotation(dir, m_cameraToUse.transform.up);



        m_start = m_cameraToUse.ViewportToWorldPoint(new Vector3(0,0, m_cameraToUse.farClipPlane));
        m_right = m_cameraToUse.ViewportToWorldPoint(new Vector3(1, 0, m_cameraToUse.farClipPlane));
        m_end = m_cameraToUse.ViewportToWorldPoint(new Vector3(0, 1, m_cameraToUse.farClipPlane));

        Vector3 centerTop = m_cameraToUse.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, m_cameraToUse.farClipPlane));
        m_ground.position = centerTop;
        m_ground.position -= m_cameraToUse.transform.forward * m_groundAdjustmentHeight;
        m_ground.rotation = m_cameraToUse.transform.rotation;
        m_ground.localScale = new Vector3( Vector3.Distance(m_start, m_right), Vector3.Distance(m_start, m_end),1);


        foreach (var item in m_setTargetsToCenter)
        {
            item.position = centerTop;
            item.rotation = m_cameraToUse.transform.rotation * Quaternion.Euler(-90,0,0);
            item.localScale = Vector3.one;
        }

    }
}
