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

        void OnSubmitClicked(Object sender, EventArgs e)
        {
            DisplayAlert("OK", "PDF generated successfully", "OK");
        }
    }

}
