using System;

using UnityEngine;

namespace CustomerAssistant
{
    public abstract class CustomButtonView : MonoBehaviour
    {
        public event Action ButtonClicked = delegate { };

        public void HandleButtonClick()
        {
            ButtonClicked.Invoke();
        }
    }
}