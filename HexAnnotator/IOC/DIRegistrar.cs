using GalaSoft.MvvmLight.Ioc;
using HexAnnotator.Services;
using HexAnnotator.ViewModels;

namespace HexAnnotator.IOC
{
    static class DIRegistrar
    {
        public static void RegisterTypes()
        {
            SimpleIoc.Default.Register<FileReader>();
            SimpleIoc.Default.Register<HexGridViewModel>();
            SimpleIoc.Default.Register<BinaryFileService>();

        }
    }
}
