using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine2D : MonoBehaviour
{
    public Boolean isDraw = false;
    [SerializeField]
    protected LineRenderer m_LineRenderer;
    [SerializeField]
    protected bool m_AddCollider = true;
    [SerializeField]
    protected EdgeCollider2D m_EdgeCollider2D;
    [SerializeField]
    protected Camera m_Camera;
    public float guess = 0.2f;
    protected List<Vector2> m_Points;
    public double mana = 100;
    public bool start = false;
    public Slider manaBar;
    public bool Ree = true;
    public int delay = 0;

    protected void Awake()
    {
        m_LineRenderer.sortingLayerName = "background";
        m_LineRenderer.positionCount = 0;
        m_LineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        m_LineRenderer.startColor = Color.white;
        m_LineRenderer.endColor = Color.white;


        m_LineRenderer.startWidth = 1.0f;
        m_LineRenderer.endWidth = 1.0f;
        m_LineRenderer.useWorldSpace = true;
        if (m_LineRenderer == null)
        {
            CreateDefaultLineRenderer();
        }
        if (m_EdgeCollider2D == null && m_AddCollider)
        {
            CreateDefaultEdgeCollider2D();
        }
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
        m_Points = new List<Vector2>();
    }

    protected  void Update()
    {
        if (Ree)
            mana += 0.3;
        else
            mana -= 0.3;
        delay--;
        mana = Math.Min(100, mana);
        mana = Math.Max(mana, 0);
        if (Input.GetMouseButton(1) && mana > 20 )
        {
          
            Ree = false;
            delay = 120;
     
            if (!start )
            {
                start = true;
                mana -= 20;
            }
            m_EdgeCollider2D.enabled = true;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!m_Points.Contains(mousePosition))
            {
               
                m_Points.Add(mousePosition);
                m_LineRenderer.positionCount = m_Points.Count;
                m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, mousePosition);
                if (m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1)
                {
                    m_EdgeCollider2D.points = m_Points.ToArray();
                 
                    mana -= (m_Points[m_Points.Count - 1].x - m_Points[m_Points.Count - 2].x) * (m_Points[m_Points.Count - 1].x - m_Points[m_Points.Count - 2].x) * guess;
                    mana -= (m_Points[m_Points.Count - 1].y - m_Points[m_Points.Count - 2].y) * (m_Points[m_Points.Count - 1].y - m_Points[m_Points.Count - 2].y) * guess;

                }
            }
        }
        else if(delay < 0 && !Ree)
            Reset();

        //UI
        manaBar.value = (float)mana;
    }

    protected  void Reset()
    {
        isDraw = false;
        Ree = true;
        start = false;
        if (m_EdgeCollider2D != null && m_AddCollider)
        {
            m_EdgeCollider2D.Reset();
            m_EdgeCollider2D.enabled = false;
        }
        if (m_LineRenderer != null)
        {
            m_LineRenderer.positionCount = 0;
        }
        if (m_Points != null)
        {
            m_Points.Clear();
        }
       
    }

    protected  void CreateDefaultLineRenderer()
    {
        m_LineRenderer = gameObject.AddComponent<LineRenderer>();
    
    }

    protected  void CreateDefaultEdgeCollider2D()
    {
        m_EdgeCollider2D = gameObject.AddComponent<EdgeCollider2D>();
    }

}