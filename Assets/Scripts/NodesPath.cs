using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesPath : MonoBehaviour
{
    public static NodesPath Instance { get; private set; }

    [SerializeField] 
    private float m_defaultRadius =.5f;

    float m_ActualRadius=0.5f;

    public float RadiusRun
    {
        get => m_ActualRadius;
        set => m_ActualRadius = value;
    }

    public GameObject[] Nodes { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        m_ActualRadius= m_defaultRadius;
    }

    private void OnDrawGizmos()
    {
        Nodes = GameObject.FindGameObjectsWithTag("Node");
        for (int i = 0; i < Nodes.Length; ++i)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Nodes[i].transform.position, 0.35f);
            Gizmos.color = Color.green;

            if (i < Nodes.Length - 1)
            {
                Gizmos.DrawLine(Nodes[i].transform.position, Nodes[i + 1].transform.position);
            }
            else if (i == Nodes.Length - 1)
            {
                Gizmos.DrawLine(Nodes[i].transform.position, Nodes[0].transform.position);
            }
        }
    }

    public int GetNearestNodeIndex(Vector2 position)
    {
        int nearestNodeIndex = 0;
        for (int i = 0; i < Nodes.Length; ++i)
        {
            if (Vector2.Distance(Nodes[i].transform.position, position) <
                Vector2.Distance(Nodes[nearestNodeIndex].transform.position, position))
            {
                nearestNodeIndex = i;
            }
        }
        return nearestNodeIndex;
    }

    public int GetNextNodeIndex(int index)
    {
        if (index >= Nodes.Length - 1)
        {
            return 0;
        }
        return index + 1;
    }

    public void SetDefaultRadius()
    {
        m_ActualRadius = m_defaultRadius;
    }

    public float GetDefaultRadius() { return m_defaultRadius; }
}
