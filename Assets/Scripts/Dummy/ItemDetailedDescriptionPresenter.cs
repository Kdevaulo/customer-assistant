namespace Dummy
{
    public class ItemDetailedDescriptionPresenter
    {
        private ItemModel _model;
        private ItemDetailedDescriptionView _view;

        public ItemDetailedDescriptionPresenter(ItemDetailedDescriptionView view, ItemModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.OpenDescription += OnOpenDescription;
        }

        public void Disable()
        {
            _view.OpenDescription += OnOpenDescription;
        }

        private void OnOpenDescription()
        {
            _view.SetIcon(_model.ItemImage);
            _view.SetDescriptionText(_model.GetCurrentItem());
        }
    }
}