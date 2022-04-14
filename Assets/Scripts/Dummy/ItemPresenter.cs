namespace Dummy
{
    public class ItemPresenter
    {
        private ItemView _view;
        private ItemModel _model;

        public ItemPresenter(ItemView view, ItemModel model)
        {
            _view = view;
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
            _view.SetIcon(_model.ItemImage);
            _view.DescriptionPanel.SetActive(false);
            _view.InfoToggle.isOn = false;
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
            string descriptionText = _model.GetDescriptionText();
            if (descriptionText == null)
                return;
            _view.SetDescriptionText(descriptionText);
            _view.DescriptionPanel.SetActive(clicked);
        }

        private void OnViewClick(bool clicked)
        {
            _model.ShowInfo(clicked);
            OnInfoClick(clicked);
        }
    }
}