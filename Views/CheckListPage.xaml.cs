using DoorChecker.Data;
using DoorChecker.Models;

namespace DoorChecker
{
    public partial class CheckListPage : ContentPage
    {
        private DoorCheckDatabase database;
        private string Username;
        readonly CheckListViewModel viewModel;

        public CheckListPage(DoorCheckDatabase database, string username)
        {
            InitializeComponent();
            viewModel = new CheckListViewModel(database);
            BindingContext = viewModel;
            this.database = database;
            this.Username = username;
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            viewModel.RefreshData();
            dataGrid.RefreshData();
        }

        async void OnLogoutClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(database));
        }

        void OnAddNewClicked(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new CheckItemPage(database, Username, 0));
        }

        void OnUpdateClicked(Object sender, EventArgs e)
        {
            if (viewModel.SelectedItem != null)
            {
                Navigation.PushAsync(new CheckItemPage(database, Username, viewModel.SelectedItem.ID));
            }
            else
            {
                DisplayAlert("WARNING", "Please select an item", "OK");
            }
        }
    }

}
