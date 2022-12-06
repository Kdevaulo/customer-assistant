using System;

using UnityEngine;

namespace CustomerAssistant.SettingsActivity.Models
{
    [Serializable]
    public class StringBoolModel
    {
        [SerializeField] private string _text;

        [SerializeField] private bool _value;

        public StringBoolModel(string text, bool value)
        {
            _text = text;
            _value = value;
        }

        public string Text => _text;

        public bool Value => _value;

        public void SetValue(bool value)
        {
            _value = value;
        }
    }
}