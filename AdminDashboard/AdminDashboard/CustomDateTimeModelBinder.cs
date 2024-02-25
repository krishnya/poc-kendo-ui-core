using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdminDashboard
{
    public class CustomDateTimeModelBinder : IModelBinder
    {
        private static readonly string[] DateFormats = { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd" };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            foreach (var format in DateFormats)
            {
                if (DateTime.TryParseExact(valueProviderResult.FirstValue, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    bindingContext.Result = ModelBindingResult.Success(date);
                    return Task.CompletedTask;
                }
            }

            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
    }
}