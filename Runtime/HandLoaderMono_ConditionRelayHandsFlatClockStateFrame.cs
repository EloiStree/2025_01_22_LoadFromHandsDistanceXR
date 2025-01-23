using UnityEngine;
using UnityEngine.Events;

public class HandLoaderMono_ConditionRelayHandsFlatClockStateFrame : MonoBehaviour
{
    public HandsFlatClockStateFrame m_frame;
    public HandsFlatCondition m_condition;
    public bool m_isInCondition;
    private bool m_previousInCondition;
    public UnityEvent<bool,HandsFlatClockStateFrame> m_onInConditionUpdatedFrame;
    public UnityEvent<bool,HandsFlatClockStateFrame> m_onConditionChangedFrame;
  
    public void SetFrame(HandsFlatClockStateFrame frame)
    {
        m_previousInCondition= m_isInCondition;
        m_frame = frame;
        m_isInCondition = m_condition.IsInCondition(in frame);
        if (m_isInCondition)
            m_onInConditionUpdatedFrame.Invoke(m_isInCondition,m_frame);
        if (m_previousInCondition && !m_isInCondition)
            m_onConditionChangedFrame.Invoke(m_isInCondition, frame);
    }
}

