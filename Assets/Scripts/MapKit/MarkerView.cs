using UnityEngine;

namespace CustomerAssistant.MapKit
{
    public class MarkerView : MonoBehaviour
    {
        public Vector3 RadiusPointPosition => _radiusPoint.position;

        [SerializeField] private Transform _radiusPoint;

        public Transform RingTransform;
    }
}