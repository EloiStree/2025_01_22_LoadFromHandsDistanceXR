using UnityEngine;
using UnityEngine.Events;

public class HandLoaderMono_NotMovingTransformCounter : MonoBehaviour {


    public Transform m_target;
   
    public float m_distanceToBeNotMoving = 0.04f;
    public float m_angleToBeNotMoving = 10;

    public Vector3 m_lastPosition;
    public Quaternion m_lastRotation;
    public float m_timeNotMoving;
    public bool m_outOfRange;
    public bool m_outOfAngle;
    public bool m_outOfRangeOrAngle;

    public NotMovingEvent [] m_notMovingEvents = new NotMovingEvent[1] { new NotMovingEvent() { m_timeToBeNotMoving = 1 } };
        
    [System.Serializable]
    public class NotMovingEvent { 
    
        public float m_timeToBeNotMoving = 1;
        public UnityEvent m_onNotMoving;
        public UnityEvent m_onMovingAgain;

        public bool m_currentIsNotMoving;
        public bool m_previousIsNotMoving;


        public void UpdateWith(float timeNotMoving)
        {
            m_previousIsNotMoving = m_currentIsNotMoving;
            m_currentIsNotMoving = timeNotMoving > m_timeToBeNotMoving;
            if (m_currentIsNotMoving!= m_previousIsNotMoving)
            {
                if (m_currentIsNotMoving)
                {
                    m_onNotMoving.Invoke();
                }
                else
                {
                    m_onMovingAgain.Invoke();
                }
            }   
        }

    }


    public void Reset()
    {
        m_target = this.transform;
    }

    private void Update()
    {
        if (m_target == null) return;


        m_outOfRange = Vector3.Distance(m_target.position, m_lastPosition) > m_distanceToBeNotMoving;
        m_outOfAngle = Quaternion.Angle(m_target.rotation, m_lastRotation) > m_angleToBeNotMoving;
        m_outOfRangeOrAngle = m_outOfRange || m_outOfAngle;

        if (m_outOfRangeOrAngle)
        {
            m_timeNotMoving = 0;
            m_lastPosition = m_target.position;
            m_lastRotation= m_target.rotation;
        }
        else
        {
            m_timeNotMoving += Time.deltaTime;
        }
    }

}
