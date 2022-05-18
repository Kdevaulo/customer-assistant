namespace Mapbox.Examples
{
    using UnityEngine;

    public class CameraBillboard : MonoBehaviour
    {
        public Camera _camera;

        public float PositionY;

        public void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
                _camera.transform.rotation * Vector3.up);

            // note: Z-fighting fix
            transform.position = new Vector3(transform.position.x, PositionY, transform.position.z);
        }
    }
}