using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdminDashboard
{
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (DateTime.TryParseExact(valueProviderResult.FirstValue, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                bindingContext.Result = ModelBindingResult.Success(date);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    "Invalid date format. Use 'dd/MM/yyyy' format.");
            }

            return Task.CompletedTask;
        }
    }
}