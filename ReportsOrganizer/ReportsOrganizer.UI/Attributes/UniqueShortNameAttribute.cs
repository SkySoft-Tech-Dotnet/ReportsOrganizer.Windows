using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Providers;

namespace ReportsOrganizer.UI.Attributes
{
    public class UniqueShortNameAttribute : ValidationAttribute
    {
        private readonly string _idProperty;

        public UniqueShortNameAttribute(string idProperty)
        {
            _idProperty = idProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var projectService = ServiceCollectionProvider.Container.GetInstance<IProjectService>();

            if (value != null)
            {

                var uniqueProject = projectService
                    .FindByShortName((string)value, CancellationToken.None).Result;

                var id = GetPropertyValue<int>(_idProperty, validationContext);

                if (uniqueProject != null && (uniqueProject.Id != id || uniqueProject.Id == 0))
                {
                    return new ValidationResult("Current Short Name is already exist", new []{"ShortName"});
                }
            }

            return ValidationResult.Success;
        }

        protected T GetPropertyValue<T>(string propertyName, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                var secondValue = propertyInfo.GetValue(
                    validationContext.ObjectInstance, null);
                return (T)secondValue;
            }
            return default(T);
        }
    }
}
