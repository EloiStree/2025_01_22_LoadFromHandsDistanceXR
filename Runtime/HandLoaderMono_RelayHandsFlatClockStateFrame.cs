using System;
using UnityEngine;
using UnityEngine.Events;

public class HandLoaderMono_RelayHandsFlatClockStateFrame : MonoBehaviour
{

    public HandsFlatClockStateFrame m_frame;

    public UnityEvent<HandsFlatClockStateFrame> m_onFrame;

    public void SetFrame(HandsFlatClockStateFrame frame)
    {
        m_frame = frame;
        m_onFrame.Invoke(m_frame);
    }
}

[System.Serializable]
public class HandsFlatCondition
{
    public HandFlatStandingType m_handPositionType;
    public float m_distanceBetweenHands = 0.15f;
    public float m_distanceErrorMargin = 0.05f;
    public float m_inverseClockWiseAngleLeftToRightHands = 0;
    public float m_angleErrorMargin = 15f;
    
    public bool IsInCondition(in HandsFlatClockStateFrame frame)
    {
        if (m_handPositionType == HandFlatStandingType.Any) {}
        else { 

            if (frame.m_handFlatStandingTypeLeft != m_handPositionType) 
                return false;

            if (frame.m_handFlatStandingTypeRight != m_handPositionType) 
                return false;
        }

        float distance = frame.m_distanceBetweenHands;
        if (distance < m_distanceBetweenHands - m_distanceErrorMargin) return false;
        if (distance > m_distanceBetweenHands + m_distanceErrorMargin) return false;
        float angle = frame.m_inverseClockWiseAngleLeftToRightHands;
        if (angle < m_inverseClockWiseAngleLeftToRightHands - m_angleErrorMargin) return false;
        if (angle > m_inverseClockWiseAngleLeftToRightHands + m_angleErrorMargin) return false;
        return true;    
    }
}
