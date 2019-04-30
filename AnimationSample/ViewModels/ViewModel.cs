using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;

namespace AnimationSample.ViewModels
{
    public abstract class ViewModel<T> : DependencyObject, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            { PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
        }

        public virtual void OnPropertyChanged<T2>(Expression<Func<T, T2>> accessor)
        {
            OnPropertyChanged(PropertyName(accessor));
        }

        public static string PropertyName<T2>(Expression<Func<T, T2>> accessor)
        {
            return ((MemberExpression)accessor.Body).Member.Name;
        }
    }
}
