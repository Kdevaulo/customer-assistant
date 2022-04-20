using UnityEngine;

namespace Dummy
{
    public class ItemSetup : MonoBehaviour
    {
        [SerializeField] private ItemView _view;
        [SerializeField] private ItemModel _model;
        private ItemPresenter _presenter;

        private void Awake()
        {
            _presenter = new ItemPresenter(_view, _model);
            _model.Init();
        }

        private void OnEnable()
        {
            _presenter.Enable();
        }

        private void OnDisable()
        {
            _presenter.Disable();
        }
    }
}