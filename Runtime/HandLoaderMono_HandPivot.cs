using UnityEngine;




public class HandLoaderMono_HandPivot : MonoBehaviour {


    public Transform m_toMove;
    [Header("Controller")]
    public Transform m_controller;
    [Header("Hand")]
    public GameObject m_isHandActive;
    public Transform m_leftFingerIndex;
    public Transform m_centerFingerIndex;
    public Transform m_rightFingerIndex;

    public Vector3 m_position;
    public Quaternion m_rotation;

    public bool m_inverseRotation = false;

    public void Update()
    {
        
        if (! m_isHandActive.activeInHierarchy)
        {
            m_position = m_controller.position;
            m_rotation = m_controller.rotation;
        }
        else
        {
            m_position = (m_leftFingerIndex.position + m_centerFingerIndex.position + m_rightFingerIndex.position) / 3f;
            
           Vector3 forward = m_centerFingerIndex.position - m_position;
           Vector3 upDir= Vector3.Cross(
               m_leftFingerIndex.position - m_centerFingerIndex.position,
               m_rightFingerIndex.position - m_centerFingerIndex.position);
            
            if (m_inverseRotation)
            {
                upDir = -upDir;
            }
            

            Debug.DrawLine(m_centerFingerIndex.position, m_rightFingerIndex.position, Color.yellow);
            Debug.DrawLine(m_centerFingerIndex.position, m_leftFingerIndex.position, Color.yellow);
            Debug.DrawLine(m_leftFingerIndex.position, m_rightFingerIndex.position , Color.yellow);
       


            m_rotation = Quaternion.LookRotation(forward, upDir);
            Debug.DrawRay(m_position, m_rotation*Vector3.forward, Color.blue);

            Debug.DrawLine(m_position,
               m_position + m_rotation*Vector3.forward*0.1f, Color.blue);
            Debug.DrawLine(m_position,
               m_position + m_rotation * Vector3.right * 0.1f, Color.red);
            Debug.DrawLine(m_position,
               m_position + m_rotation * Vector3.up * 0.1f, Color.green);


            
        }

        m_toMove.rotation = m_rotation;
        m_toMove.position = m_position;
    
    }

}


