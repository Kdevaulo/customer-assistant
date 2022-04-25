using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dummy
{
    public class DummyMainView : MonoBehaviour, IPointerClickHandler
    {
        public event Action DescriptionClose;

        public void OnPointerClick(PointerEventData eventData)
        {
            DescriptionClose?.Invoke();
        }
    }
}