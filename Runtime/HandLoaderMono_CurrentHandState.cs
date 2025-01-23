using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HandsFlatClockStateFrame {

    public float m_distanceBetweenHands;
    public float m_inverseClockWiseAngleLeftToRightHands;
    public float m_heightLeftHand;
    public float m_upAngleLeftHand;
    public HandFlatStandingType m_handFlatStandingTypeLeft;
    public float m_upAngleRightHand;
    public float m_heightRightHand;
    public HandFlatStandingType m_handFlatStandingTypeRight;
}

public class HandLoaderMono_CurrentHandState : MonoBehaviour {

    public Transform m_xrGuardianRoot;
    public Transform m_leftHandCenter;
    public Transform m_rightHandCenter;
    public float m_flatDetectionRangeAngle = 20;

    public HandsFlatClockStateFrame m_currentFrame;

    public UnityEvent<HandsFlatClockStateFrame> m_onFrameChanged;
    public void Update()
    {
        Refresh();
    }

    [ContextMenu("Refresh")]
    private void Refresh()
    {
        GetWorldToLocal_Point(
            m_rightHandCenter.position
            , m_leftHandCenter.position,
            m_leftHandCenter.rotation, out Vector3 localRightHandCenter);
        localRightHandCenter.y = 0;
        m_currentFrame.m_distanceBetweenHands = localRightHandCenter.magnitude;
        m_currentFrame.m_inverseClockWiseAngleLeftToRightHands =
          Mathf.Rad2Deg*  Mathf.Atan2(localRightHandCenter.z, localRightHandCenter.x);

        
        GetHandInfo(m_leftHandCenter, 
            out m_currentFrame.m_heightLeftHand,
            out m_currentFrame.m_upAngleLeftHand, out m_currentFrame.m_handFlatStandingTypeLeft);

        GetHandInfo(m_rightHandCenter,
            out m_currentFrame.m_heightRightHand,
            out m_currentFrame.m_upAngleRightHand, out m_currentFrame.m_handFlatStandingTypeRight);

        m_onFrameChanged.Invoke(m_currentFrame);
    }

    private void GetHandInfo(Transform hand,        
        out float height,
        out float groundToUpAngle, out HandFlatStandingType handFlatType)
    {
        groundToUpAngle = Vector3.Angle(hand.up, Vector3.up);

        handFlatType = HandFlatStandingType.Custom;
        if (m_xrGuardianRoot == null) { 
        
            height = hand.position.y;
        }
        else
        {
            GetWorldToLocal_Point(hand.position, m_xrGuardianRoot.position, m_xrGuardianRoot.rotation, out Vector3 localHandPosition);
            height = localHandPosition.y;
        }
        
        if (groundToUpAngle > 90 - m_flatDetectionRangeAngle &&
            groundToUpAngle < 90 + m_flatDetectionRangeAngle)
        {
            handFlatType = HandFlatStandingType.Wall;
        }
        else if (groundToUpAngle < m_flatDetectionRangeAngle )
        {
            handFlatType = HandFlatStandingType.FlatHorizontalSurface;
        }
        else if (groundToUpAngle > 180 - m_flatDetectionRangeAngle)
        {
            handFlatType = HandFlatStandingType.Ceiling;
        }

        if (handFlatType == HandFlatStandingType.FlatHorizontalSurface) {
            if (height < m_groundHeight) {

                handFlatType = HandFlatStandingType.Ground;
            }        
        }
    }
    public float m_groundHeight = 0.1f;

    public static void GetWorldToLocal_Point(in Vector3 worldPosition, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 localPosition) =>
           localPosition = Quaternion.Inverse(rotationReference) * (worldPosition - positionReference);

    public static void GetLocalToWorld_Point(in Vector3 localPosition, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 worldPosition) =>
        worldPosition = (rotationReference * localPosition) + (positionReference);

}
