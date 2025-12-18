using DoorChecker.Data;
using DoorChecker.Models;

namespace DoorChecker
{
    public partial class CheckItemPage : ContentPage
    {
        readonly CheckItemViewModel viewModel;

        public CheckItemPage(DoorCheckDatabase database, int checkLogID)
        {
            InitializeComponent();
            viewModel = new CheckItemViewModel(database, checkLogID);
            BindingContext = viewModel;
        }

        async void OnSubmitClicked(Object sender, EventArgs e)
        {
            await DisplayAlert("Information", "Upload image", "OK");
        }

        async void OnSaveClicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteClicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        async void OnCancelClicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
