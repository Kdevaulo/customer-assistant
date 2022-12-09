using UnityEngine;

namespace CustomerAssistant.MapKit
{
    public class MarkerView : MonoBehaviour
    {
        public Vector3 RadiusPointPosition => _radiusPoint.position;

        public Transform RingTransform;

        [SerializeField] private Transform _radiusPoint;
    }
}