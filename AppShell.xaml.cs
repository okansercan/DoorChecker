namespace DoorChecker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CheckListPage), typeof(CheckListPage));
            Routing.RegisterRoute(nameof(CheckItemPage), typeof(CheckItemPage));
        }
    }
}
