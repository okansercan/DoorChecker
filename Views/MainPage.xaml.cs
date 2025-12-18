using DoorChecker.Data;
using DoorChecker.Models;

namespace DoorChecker
{
    public partial class MainPage : ContentPage
    {
        private DoorCheckDatabase database;
        readonly MainViewModel viewModel;

        public MainPage(DoorCheckDatabase database)
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            BindingContext = viewModel;
            this.database = database;
        }

        void OnSubmitClicked(Object sender, EventArgs e)
        {
            if (!viewModel.Validate())
                DisplayAlert("HATA", viewModel.ErrorText, "TAMAM");
            else
                Navigation.PushAsync(new CheckListPage(database));
        }
    }

}
