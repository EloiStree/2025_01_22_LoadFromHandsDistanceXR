using UnityEngine;
using UnityEngine.Events;

public class HandLoaderMono_DetectHandFlatStanding : MonoBehaviour
{
    public HandLoaderMono_CurrentHandState m_handState;
    public HandLoaderMono_NotMovingTransformCounter m_leftHand;
    public HandLoaderMono_NotMovingTransformCounter m_rightHand;

    public float m_timeToConsiderNotMoving = 3;

    public bool m_isLeftNotMoving;
    public bool m_isRightNotMoving;
    public bool m_currentIsBothNotMoving;
    public bool m_previousIsBothNotMoving;

    public UnityEvent<HandsFlatClockStateFrame> m_onBothHandsNotMoving;
    public UnityEvent m_onBothHandsMovingAgain;

    public void Update()
    {
        m_previousIsBothNotMoving= m_currentIsBothNotMoving;
        m_isLeftNotMoving = m_leftHand.m_timeNotMoving > m_timeToConsiderNotMoving;
        m_isRightNotMoving = m_rightHand.m_timeNotMoving > m_timeToConsiderNotMoving;
        m_currentIsBothNotMoving = m_isLeftNotMoving && m_isRightNotMoving;

        if (m_currentIsBothNotMoving != m_previousIsBothNotMoving)
        {
            if (m_currentIsBothNotMoving)
            {
                m_onBothHandsNotMoving.Invoke(m_handState.m_currentFrame);
            }
            else
            {
                m_onBothHandsMovingAgain.Invoke();
            }
        }
        
    }

}