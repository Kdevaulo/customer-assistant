using System;

using UnityEngine;

namespace CustomerAssistant.SettingsActivity.Models
{
    [Serializable]
    public class StringBoolModel
    {
        public string Text => _text;

        public bool Value => _value;

        public void SetValue(bool value)
        {
            _value = value;
        }

        [SerializeField] private string _text;

        [SerializeField] private bool _value;

        public StringBoolModel(string text, bool value)
        {
            _text = text;
            _value = value;
        }
    }
}