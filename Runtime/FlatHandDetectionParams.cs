[System.Serializable]
public class FlatHandDetectionParams
{
    public HandFlatStandingType m_handStandingType = HandFlatStandingType.Any;
    public float m_angleBetweenHandsDownToTop;
    public float m_angleRangeAround=10f;
    public float m_distanceBetweenHands;
    public float m_distanceRangeAround=0.05f;
}


