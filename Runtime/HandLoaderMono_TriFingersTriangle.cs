using UnityEngine;

public class HandLoaderMono_TriFingersTriangle : MonoBehaviour
{
    public Transform m_handRoot;
    public Transform m_leftTip;
    public Transform m_centerTip;
    public Transform m_rightTip;

    public MeshFilter m_renderer;


    private void Awake()
    {
        Mesh m  = new Mesh();
        m.name = "Triangle";
        m.vertices = new Vector3[]
        {
            m_leftTip.position,
            m_centerTip.position,
            m_rightTip.position
        };
        m.triangles = new int[] {
            0, 1, 2
        };

        m.uv = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        m.RecalculateNormals();
        m.RecalculateBounds();
        m_renderer.mesh = m;
    }

    public void Update()
    {
        m_renderer.transform.position = Vector3.zero;
        m_renderer.transform.rotation = Quaternion.identity;
        Vector3 left, center, right;
        left = m_leftTip.position;
        center = m_centerTip.position;
        right = m_rightTip.position;

        if (m_handRoot != null) {

            
        
        }

        Mesh m = m_renderer.mesh;
        m.vertices = new Vector3[]
        {left,        center,right
        };
        m.RecalculateNormals();
        m.RecalculateBounds();
    }
    public static void GetWorldToLocal_DirectionalPoint(in Vector3 worldPosition, in Quaternion worldRotation, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 localPosition, out Quaternion localRotation)
    {
        localRotation = Quaternion.Inverse(rotationReference) * worldRotation;
        localPosition = Quaternion.Inverse(rotationReference) * (worldPosition - positionReference);
    }
    public static void GetLocalToWorld_DirectionalPoint(in Vector3 localPosition, in Quaternion localRotation, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 worldPosition, out Quaternion worldRotation)
    {
        worldRotation = localRotation * rotationReference;
        worldPosition = (rotationReference * localPosition) + (positionReference);
    }
}


