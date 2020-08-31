using GalaSoft.MvvmLight.Ioc;
using HexAnnotator.Models;
using HexAnnotator.ViewModels;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HexAnnotator.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HexGrid : Page
    {
        public HexGridViewModel ViewModel { get; }

        public HexGrid()
        {
            this.InitializeComponent();
            ViewModel = SimpleIoc.Default.GetInstance<HexGridViewModel>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadFile("TEST.bin");
        }

        private void Block_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if(sender is StackPanel sp)
            {
                if(sp.DataContext is ByteRange r)
                {
                    ViewModel.RangeStart = r.AddressRange.Start;
                }
            }
        }
    }
}
