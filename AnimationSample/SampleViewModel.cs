using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AnimationSample
{
    public class SampleViewModel : NotifyPropertyChangedBase<SampleViewModel>
    { 
        public SampleViewModel()
        {
            ChangeImagePathCommand = new ActionCommand()
            {
                CanExecuteAction = e => true,
                ExecuteAction = e => {
                    Task.Run(() =>
                    {
                        int eI = int.Parse(e.ToString());
                        ImagePath = Path.GetFullPath($"Resources/image{eI}.png");
                        Alarm = eI < 3;
                        OnPropertyChanged(x => x.ImagePath);
                    });
                }
            };
        }

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

        public string ImagePath { get; set; } = Path.GetFullPath($"Resources/image1.png");
        public ICommand ChangeImagePathCommand { get; private set; }
    }

    public class ActionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Func<object, bool> CanExecuteAction { get; set; }
        public bool CanExecute(object parameter) => CanExecuteAction != null ? CanExecuteAction.Invoke(parameter) : true;

        public Action<object> ExecuteAction { get; set; }
        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
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