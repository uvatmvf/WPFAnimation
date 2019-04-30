using Microsoft.Win32;
using System.Threading.Tasks;

namespace AnimationSample.Commands
{
    public interface IFileCommand
    {
        string CommandFile { get; }
    }

    public class AttachFileCommand : ActionCommand, IFileCommand
    {
        public string CommandFile { get; private set; }

        public AttachFileCommand()
        {
            CanExecuteAction = e => true;
            ExecuteAction = e =>
            {
                Task.Run(() =>
                {
                    var dlg = new OpenFileDialog()
                    { Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif" };
                    var result = dlg.ShowDialog();
                    if (result == false) return;
                    CommandFile = dlg.FileName;
                    OnPropertyChanged(nameof(CommandFile));
                });
            };
        }
    }
}
