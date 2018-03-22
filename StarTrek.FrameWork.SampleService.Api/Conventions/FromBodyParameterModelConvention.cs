using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StarTrek.FrameWork.SampleService.Api.Conventions
{
    /// <summary>
    /// In .NET Core model binding if we want request body as JSON we need to annotate Action Method params with FromBodyAttribute. 
    /// //If we dont want to do that at every place we can change the ApplicationModelConvention like below
    /// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/application-model
    /// http://www.dotnetcurry.com/aspnet-mvc/1149/convert-aspnet-webapi2-aspnet5-mvc6
    /// </summary>
    public class FromBodyParameterModelConvention : IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            if (!IsSimple(parameter.ParameterInfo.ParameterType))
            {
                if (parameter.BindingInfo != null) return;


                parameter.BindingInfo = new BindingInfo();
                parameter.BindingInfo.BindingSource = BindingSource.Body;
            }
        }

        private bool IsSimple(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimple(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal);
        }
    }

}
