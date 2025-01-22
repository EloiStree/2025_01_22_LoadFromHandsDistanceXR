using UnityEngine;

public class HandLoaderMono_SleepyMoveMapToHandsPositions : MonoBehaviour{


    public bool m_isInCondition;
    public HandsFlatClockStateFrame m_handsFrame;
    public HandLoaderMono_CurrentHandState m_currentState;
    public Transform m_whatToMove;



    public void SetWithFrameCondition(bool isInCondition, HandsFlatClockStateFrame frame)
    {
        m_isInCondition = isInCondition;
        m_handsFrame = frame;
    }

    public void Update()
    {
        if (m_isInCondition)
        {
            Transform leftHand = m_currentState.m_leftHandCenter;
            Transform rightHand = m_currentState.m_rightHandCenter;
            Vector3 center = (leftHand.position + rightHand.position) / 2;
            Vector3 leftToRight = rightHand.position - leftHand.position;
            Vector3 leftHandForward = leftHand.position + leftHand.forward;
            Vector3 rightHandForward = rightHand.position + rightHand.forward;
            Vector3 forward = (leftHandForward + rightHandForward) / 2 - center;
            Vector3 up = Vector3.Cross(forward, leftToRight);

                
            Debug.DrawRay(center, forward, Color.blue);
            Debug.DrawRay(center, up, Color.green);
            Debug.DrawRay(center, leftToRight, Color.red);
            m_whatToMove.position = center;
            m_whatToMove.rotation = Quaternion.LookRotation(forward, Vector3.up);

        }
    }

}

