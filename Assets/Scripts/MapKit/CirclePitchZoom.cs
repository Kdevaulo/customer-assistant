using Microsoft.Unity.VisualStudio.Editor;

using UnityEngine;
using UnityEngine.EventSystems;

namespace CustomerAssistant.MapKit
{
    public class CirclePitchZoom : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _zoomOutMin = 0.4f;

        [SerializeField]
        private float _zoomOutMax = 3f;

        private Vector3 _touchBegin;

        private Vector3 _originPosition;

        [SerializeField]
        private Camera _mainCamera;

        private float _radius;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _originPosition = transform.position;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            _touchBegin = _mainCamera.ScreenToWorldPoint(eventData.position);
            _radius = Vector3.Distance(_originPosition, _touchBegin);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var dragPosition = _mainCamera.ScreenToWorldPoint(eventData.position);

            var newScale = Vector3.Distance(_originPosition, dragPosition) / _radius;

            // newScale = Mathf.Clamp(newScale, _zoomOutMin, _zoomOutMax);

            var scale = _target.localScale;

            scale *= newScale;

            scale.x = Mathf.Clamp(scale.x, _zoomOutMin, _zoomOutMax);
            scale.y = Mathf.Clamp(scale.y, _zoomOutMin, _zoomOutMax);
            scale.z = Mathf.Clamp(scale.z, _zoomOutMin, _zoomOutMax);

            // var newS = Vector3.ClampMagnitude(_target.localScale, newScale);

            _target.localScale = scale;

            /*if (Vector3.Distance(_originPosition, dragPosition) >
                Vector3.Distance(_originPosition, _touchBegin))
            {
                // zoom out
            }
            else if (Vector3.Distance(_originPosition, dragPosition) <
                     Vector3.Distance(_originPosition, _touchBegin))
            {
                // zoom in
            }*/
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        }
    }
}