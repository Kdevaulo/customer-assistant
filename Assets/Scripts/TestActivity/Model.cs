using UnityEngine;

namespace TestActivity
{
    [CreateAssetMenu(fileName = "Model", menuName = "TestActivity/Model")]
    public class Model : ScriptableObject
    {
        public GameObject MovingObjectPrefab => _movingObjectPrefab;

        [SerializeField] private GameObject _movingObjectPrefab;
    }
}