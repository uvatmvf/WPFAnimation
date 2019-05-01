using AnimationSample.Commands;
using AnimationSample.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnimationSample
{
    public class SampleViewModel : ViewModel<SampleViewModel>
    {
        public string ImagePath { get; set; } = Path.GetFullPath($"Resources/image1.png");
        public ICommand ChangeImagePathCommand { get; private set; }
        public IFileCommand PostImage { get; set; } = new AttachFileCommand();
        public ObservableCollection<string> NoteVersions { get; set; } = new ObservableCollection<string>();

        public SampleViewModel()
        {
            ChangeImagePathCommand = new ActionCommand()
            {
                CanExecuteAction = e => true,
                ExecuteAction = e => {
                    Task.Run(() =>
                    {
                        int eI = int.Parse(e.ToString());
                        var filePath = Path.GetFullPath($"Resources/image{eI + 1}.png");
                        if (!File.Exists(filePath))
                        { filePath = Path.GetFullPath($"Resources/image{eI + 1}.jpg"); }
                        ImagePath = filePath;
                        Alarm = eI < NoteVersions.Count - 1;
                        OnPropertyChanged(x => x.ImagePath);
                    });
                }
            };
            ((AttachFileCommand)PostImage).PropertyChanged += SampleViewModel_PropertyChanged;
            
            for (var i=0; i< Directory.EnumerateFiles(Path.GetFullPath("Resources/")).ToList().Count; i++)
            { NoteVersions.Add(i.ToString()); }
            OnPropertyChanged(x => x.NoteVersions);
            
        }


        private void SampleViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(AttachFileCommand.CommandFile)))
            {
                var newFileName = $"Resources/image{Directory.GetFiles(Path.GetFullPath($"Resources/")).Length + 1}.jpg";
                File.Copy(PostImage.CommandFile, 
                        Path.GetFullPath(newFileName));
                Dispatcher.Invoke(() =>
                {
                    NoteVersions.Add((NoteVersions.Count).ToString());
                    OnPropertyChanged(x => x.NoteVersions);
                });
            }
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
                    OnPropertyChanged(nameof(Alarm));                    
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
}