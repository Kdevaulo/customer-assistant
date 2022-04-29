namespace Dummy
{
    public class ItemPresenter
    {
        private ItemView _view;
        private DummyMainView _mainView;
        private ItemModel _model;

        public ItemPresenter(ItemView view, DummyMainView mainView, ItemModel model)
        {
            _view = view;
            _mainView = mainView;
            _model = model;
        }

        public void Enable()
        {
            _model.ItemChanged += OnItemChange;
            _model.InfoClick += OnInfoClick;
            _view.NextClick += OnViewNextClick;
            _view.PreviousClick += OnViewPreviousClick;
            _view.InfoClick += OnViewClick;
        }

        private void OnDescriptionClose()
        {
            _view.InfoToggle.isOn = false;
        }

        public void Disable()
        {
            _model.ItemChanged -= OnItemChange;
            _model.InfoClick -= OnInfoClick;
            _view.NextClick -= OnViewNextClick;
            _view.PreviousClick -= OnViewPreviousClick;
            _view.InfoClick -= OnViewClick;
        }

        private void OnItemChange()
        {
            if (_model.ItemImage == null)
            {
                _view.ButtonImage.gameObject.SetActive(false);
            }
            else
            {
                _view.ButtonImage.gameObject.SetActive(true);
            }
            _view.SetButtonShape(_model.ItemImage);
            _view.EnableItemObject(_model.ItemImage);
            _mainView.CloseInfoPanels();
        }

        private void OnViewNextClick()
        {
            _model.NextItem();
            OnItemChange();
        }

        private void OnViewPreviousClick()
        {
            _model.PreviousItem();
            OnItemChange();
        }

        private void OnInfoClick(bool clicked)
        {
            Item selectedItem = _model.GetCurrentItem();
            if (selectedItem == null)
                return;

            _view.SetDescriptionText(selectedItem);
            _view.DescriptionPanel.SetActive(clicked);
        }

        private void OnViewClick(bool clicked)
        {
            _model.ShowInfo(clicked);
            OnInfoClick(clicked);
        }
    }
}