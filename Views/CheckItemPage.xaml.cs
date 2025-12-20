using DoorChecker.Data;
using DoorChecker.Models;
using Microsoft.Maui.Storage;

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
            SetImages();
        }

        private void SetImages()
        {
            if (viewModel.ReaderIn != null && 
                viewModel.ReaderIn.Length > 0)
                imgReaderIn.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.ReaderIn));

            if (viewModel.Lock1 != null &&
                viewModel.Lock1.Length > 0)
                imgLock1.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.Lock1));

            if (viewModel.BGU != null &&
                viewModel.BGU.Length > 0)
                imgBGU.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.BGU));

            if (viewModel.ReaderOut != null &&
                viewModel.ReaderOut.Length > 0)
                imgReaderOut.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.ReaderOut));

            if (viewModel.Lock2 != null &&
                viewModel.Lock2.Length > 0)
                imgLock2.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.Lock2));

            if (viewModel.RTE != null &&
                viewModel.RTE.Length > 0)
                imgRTE.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.RTE));
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

        async void OnReaderInClicked(Object sender, EventArgs e)
        {
            ShowFilePicker(ImageType.ReaderIn);
        }

        async void OnLock1Clicked(Object sender, EventArgs e)
        {
            ShowFilePicker(ImageType.Lock1);
        }

        async void OnBguClicked(Object sender, EventArgs e)
        {
            ShowFilePicker(ImageType.BGU);
        }

        async void OnReaderOutClicked(Object sender, EventArgs e)
        {
            ShowFilePicker(ImageType.ReaderOut);
        }

        async void OnLock2Clicked(Object sender, EventArgs e)
        {
            ShowFilePicker(ImageType.Lock2);
        }

        async void OnRteClicked(Object sender, EventArgs e)
        {
            ShowFilePicker(ImageType.RTE);
        }

        async void ShowFilePicker(ImageType imageType)
        {
            Stream stream;

            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Please select a file",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
            {
                await DisplayAlert("Warning", "No file was selected", "OK");
                return;
            }
            else
            {
                stream = await result.OpenReadAsync();

                switch (imageType)
                {
                    case ImageType.ReaderIn:
                        await SetReaderInImage(stream);
                        break;
                    case ImageType.Lock1:
                        await SetLock1Image(stream);
                        break;
                    case ImageType.BGU:
                        await SetBguImage(stream);
                        break;
                    case ImageType.ReaderOut:
                        await SetReaderOutImage(stream);
                        break;
                    case ImageType.Lock2:
                        await SetLock2Image(stream);
                        break;
                    case ImageType.RTE:
                        await SetRteImage(stream);
                        break;
                }
            }
        }

        private async Task SetRteImage(Stream stream)
        {
            imgRTE.Source = ImageSource.FromStream(() => stream);
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            viewModel.RTE = memoryStream.ToArray();
        }

        private async Task SetLock2Image(Stream stream)
        {
            imgLock2.Source = ImageSource.FromStream(() => stream);
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            viewModel.Lock2 = memoryStream.ToArray();
        }

        private async Task SetReaderOutImage(Stream stream)
        {
            imgReaderOut.Source = ImageSource.FromStream(() => stream);
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            viewModel.ReaderOut = memoryStream.ToArray();
        }

        private async Task SetBguImage(Stream stream)
        {
            imgBGU.Source = ImageSource.FromStream(() => stream);
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            viewModel.BGU = memoryStream.ToArray();
        }

        private async Task SetLock1Image(Stream stream)
        {
            imgLock1.Source = ImageSource.FromStream(() => stream);
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            viewModel.Lock1 = memoryStream.ToArray();
        }

        private async Task SetReaderInImage(Stream stream)
        {
            imgReaderIn.Source = ImageSource.FromStream(() => stream);
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            viewModel.ReaderIn = memoryStream.ToArray();
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
            {
                viewModel.DeleteCheckLog(viewModel.ID);
                await Shell.Current.GoToAsync("..");
            }
        }

        async void OnCancelClicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    public enum ImageType
    {
        ReaderIn,
        Lock1,
        BGU,
        ReaderOut,
        Lock2,
        RTE
    }
}
