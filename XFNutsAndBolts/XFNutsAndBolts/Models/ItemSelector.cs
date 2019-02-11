using Xamarin.Forms;

namespace XFNutsAndBolts.Models
{
    public class ItemSelector : DataTemplateSelector
    {
        public DataTemplate RedTemplate { get; set; }
        public DataTemplate NormalTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is null) return NormalTemplate;
            return ((string)item).StartsWith("In Main Thread...") ? RedTemplate : NormalTemplate;
        }
    }
}
