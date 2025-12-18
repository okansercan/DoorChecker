using DoorChecker.Data;
using DoorChecker.Models;

namespace DoorChecker
{
    public partial class CheckListPage : ContentPage
    {
        private DoorCheckDatabase database;
        readonly CheckListViewModel viewModel;

        public CheckListPage(DoorCheckDatabase database)
        {
            InitializeComponent();
            viewModel = new CheckListViewModel(database);
            BindingContext = viewModel;
            this.database = database;
        }

        void OnAddNewClicked(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new CheckItemPage(database, 0));
        }

        void OnUpdateClicked(Object sender, EventArgs e)
        {
            if (viewModel.SelectedItem != null)
            {
                Navigation.PushAsync(new CheckItemPage(database, viewModel.SelectedItem.ID));
            }
            else
            {
                DisplayAlert("WARNING", "Please select an item", "OK");
            }
        }
    }

}
