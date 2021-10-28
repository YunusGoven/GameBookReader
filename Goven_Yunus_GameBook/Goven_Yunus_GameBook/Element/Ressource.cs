using GameBookViewModel.ViewModels;
using Microsoft.Win32;

namespace GameBookView
{
  

    public class FileRessourceChooser : IChooseResource
    {
        public string ResourceIdentifier
        {
            get
            {
                var dlg = new OpenFileDialog();
                var filePath = string.Empty;
                if ((bool) dlg.ShowDialog())
                {
                    filePath = dlg.FileName;
                }

                return filePath;
            }
        }
    }
}
