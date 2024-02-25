using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdminDashboard
{
    public class CustomDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
            {
                return new CustomDateTimeModelBinder();
            }

            return null;
        }
    }

}