using System;

using UnityEngine;
using UnityEngine.UI;

namespace CustomerAssistant.SettingsActivity.Models
{
    [Serializable]
    public class ToggleTextModel
    {
        public Text Text => _text;

        public Toggle Toggle => _toggle;

        [SerializeField] private Text _text;

        [SerializeField] private Toggle _toggle;
    }
}