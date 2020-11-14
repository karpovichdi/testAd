using Xamarin.Forms;

namespace quazimodo.TemplateSelectors
{
    public class SurveyTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _linearTemplate;
        private readonly DataTemplate _tableTemplate;

        public SurveyTemplateSelector()
        {
            // _linearTemplate = new DataTemplate(typeof(LinearView));    
            // _tableTemplate = new DataTemplate(typeof(ProductsView));    
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // return item is LinearSurveyViewModel ? _linearTemplate : _tableTemplate;
            return null;
        }
    }
}