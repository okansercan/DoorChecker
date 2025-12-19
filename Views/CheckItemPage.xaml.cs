using DoorChecker.Data;
using DoorChecker.Models;

namespace DoorChecker
{
    public partial class CheckItemPage : ContentPage
    {
        readonly CheckItemViewModel viewModel;
        private string Username;

        public CheckItemPage(DoorCheckDatabase database, string username, int checkLogID)
        {
            InitializeComponent();
            viewModel = new CheckItemViewModel(database, checkLogID);
            BindingContext = viewModel;
            this.Username = username;
            SetReaderType();
        }

        private void SetReaderType()
        {
            if (viewModel.ReaderType == "PIN")
                rbPin.IsChecked = true;
            else if (viewModel.ReaderType == "PROX")
                rbProx.IsChecked = true;
        }

        async void OnLocationChanged(Object sender, EventArgs e)
        {
            await viewModel.RefreshDoors();
            cmbDoor.ItemsSource = viewModel.Doors;
            cmbDoor.SelectedIndex = 0;
        }

        async void OnSubmitClicked(Object sender, EventArgs e)
        {
            await DisplayAlert("Information", "Upload image", "OK");
        }

        async void OnReaderChanged(Object sender, EventArgs e)
        {
            if (rbPin.IsChecked)
                viewModel.ReaderType = "PIN";
            else if (rbProx.IsChecked)
                viewModel.ReaderType = "PROX";
        }

        async void OnSaveClicked(Object sender, EventArgs e)
        {
            viewModel.AddOrUpdateCheckLog(PrepareCheckLog());
            await Shell.Current.GoToAsync("..");
        }

        private CheckLog PrepareCheckLog()
        {
            CheckLog log = new CheckLog
            {
                ID = viewModel.ID,
                CheckDate = viewModel.CheckDate,
                Username = this.Username,
                ReaderType = viewModel.ReaderType,
                DoorID = viewModel.DoorID,
                Check1 = viewModel.Check1,
                Check2 = viewModel.Check2,
                Check3 = viewModel.Check3,
                Check4 = viewModel.Check4,
                Check5 = viewModel.Check5,
                Check6 = viewModel.Check6,
                Check7 = viewModel.Check7,
                Check8 = viewModel.Check8,
                Check9 = viewModel.Check9,
                Check10 = viewModel.Check10,
                Check11 = viewModel.Check11,
                Check12 = viewModel.Check12,
                ReaderIn = viewModel.ReaderIn,
                ReaderOut = viewModel.ReaderOut,
                Lock1 = viewModel.Lock1,
                Lock2 = viewModel.Lock2,
                BGU = viewModel.BGU,
                RTE = viewModel.RTE
            };

            return log;
        }

        async void OnDeleteClicked(Object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation", "Are you sure to delete this check log?", "Yes", "No");

            if (answer)
                viewModel.DeleteCheckLog(viewModel.ID);

            await Shell.Current.GoToAsync("..");
        }

        async void OnCancelClicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
