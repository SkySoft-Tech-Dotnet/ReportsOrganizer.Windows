using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsOrganizer.UI.Abstractions
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void SetValue<T>(ref T field, T value, string propertyName)
        {
            field = value;
            NotifyPropertyChanged(propertyName);
        }

        #region INotifyDataErrorInfo Implementation

        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return new string[0];
            List<string> errorsForName;
            _errors.TryGetValue(propertyName, out errorsForName);
            return errorsForName;
        }

        public bool HasErrors
        {
            get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }

        public Task ValidatePropertyAsync(string propertyName, object value)
        {
            return Task.Run(() => Validate(propertyName, value));
        }

        public Task ValidateAsync()
        {
            return Task.Run(() => Validate());
        }

        private readonly object _lock = new object();

        public void Validate(string propertyName = null, object value = null)
        {
            lock (_lock)
            {
                var validateAllProperties = string.IsNullOrEmpty(propertyName);
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();

                if (!validateAllProperties)
                {
                    validationContext.MemberName = propertyName;
                    Validator.TryValidateProperty(value, validationContext, validationResults);
                }
                else
                {
                    Validator.TryValidateObject(this, validationContext, validationResults, true);
                }

                foreach (var kv in _errors.ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        if (validateAllProperties || kv.Key == propertyName)
                        {
                            List<string> outLi;
                            _errors.TryRemove(kv.Key, out outLi);
                            OnErrorsChanged(kv.Key);
                        }
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q)
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        List<string> outLi;
                        _errors.TryRemove(prop.Key, out outLi);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    OnErrorsChanged(prop.Key);
                }
            }
        }

        protected void AddErrorToProperty(string propertyName, List<string> errorMessages)
        {
            lock (_lock)
            {
                var existedErrors = new List<string>();
                if (_errors.ContainsKey(propertyName))
                    _errors.TryRemove(propertyName, out existedErrors);
                foreach (var errorMessage in errorMessages)
                {
                    if (!existedErrors.Contains(errorMessage))
                        existedErrors.Add(errorMessage);
                }

                _errors.TryAdd(propertyName, existedErrors);
                OnErrorsChanged(propertyName);
            }
        }

        #endregion
    }
}
