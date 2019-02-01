using CommonServiceLocator;

namespace XFNutsAndBolts.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            
        }

        public MainPageViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainPageViewModel>(); }
        }
    }
}
