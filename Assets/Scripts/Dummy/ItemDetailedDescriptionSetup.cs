using UnityEngine;

namespace Dummy
{
    public class ItemDetailedDescriptionSetup : MonoBehaviour
    {
        [SerializeField] private ItemDetailedDescriptionView _view;
        [SerializeField] private DummyMainView _mainView;
        [SerializeField] private ItemModel _model;
        private ItemDetailedDescriptionPresenter _presenter;

        private void Awake()
        {
            _presenter = new ItemDetailedDescriptionPresenter(_view, _mainView, _model);
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