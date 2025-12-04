using DoorChecker.Models;

namespace DoorChecker
{
    public partial class MainPage : ContentPage
    {
        readonly MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            BindingContext = viewModel;
        }

        void OnSubmitClicked(Object sender, EventArgs e)
        {
            DisplayAlert("OK", "PDF generated successfully", "OK");
        }
    }

}
