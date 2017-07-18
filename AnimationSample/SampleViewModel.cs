using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;

namespace AnimationSample
{
    public class SampleViewModel : NotifyPropertyChangedBase<SampleViewModel>
    {
        private bool _alarm;
        public bool Alarm
        {
            get { return _alarm; }
            set
            {
                if (!_alarm.Equals(value))
                {
                    _alarm = value;
                    OnPropertyChanged("Alarm");                    
                }
            }
        }

        private bool _flash;
        public bool Flash
        {
            get { return _flash; }
            set
            {
                if (!_flash.Equals(value))
                {
                    _flash = value;
                    OnPropertyChanged("Flash");
                }
            }
        }


        private string _description = "I am a view model description!";
        public string Description
        {
            get { return _description; }
            set
            {
                if(!_description.Equals(value))
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
    }

    public abstract class NotifyPropertyChangedBase<T> : DependencyObject, INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            { PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
        }

        protected virtual void OnPropertyChanged<T2>(Expression<Func<T, T2>> accessor)
        {
            OnPropertyChanged(PropertyName(accessor));
        }        
      
        public static string PropertyName<T2>(Expression<Func<T, T2>> accessor)
        {
            return ((MemberExpression)accessor.Body).Member.Name;
        }
    }
}