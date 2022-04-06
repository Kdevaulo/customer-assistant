using System;

using SettingsActivity.Views;

using UnityEngine;

namespace SettingsActivity
{
    public class GameplayController
    {
        private readonly BackButtonView _backButton;

        public GameplayController(BackButtonView backButton)
        {
            _backButton = backButton;
            
            Start();
        }

        private void Start()
        {
            _backButton.OnBackButtonClicked += BackButtonClickHandler;
        }

        private void BackButtonClickHandler()
        {
            // todo: to dummy's activity
            Debug.Log("BackButton pressed");
        }
    }
}