using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GameBook.Domains;
using GameBook.Storage;
using GameBookViewModel.Commands;

namespace GameBookViewModel.ViewModels
{
    public class StoryViewModel : INotifyPropertyChanged
    {

        private IStory _story;
        private readonly IStorageManager _manager;
        public IChooseResource OpenFileDialog { get; set; }


        public StoryViewModel(IStory story, IStorageManager manager)
        {
            _manager = manager;
            _story = story??_manager.GetLastStory();
            ParagraphParcouru = new ObservableCollection<string>(_story.ChoiceDoneS);
            InitCommand();
        }



        private void InitCommand()
        {
            GoBack = ParameterlessRelayCommand.From(this.GoBackAsked);
            Changer = ParameterizedRelayCommand<string>.From(this.GoToNext);
            Save = ParameterlessRelayCommand.From(this.SaveToFile);
            Open = ParameterlessRelayCommand.From(this.OpenSession);
            NewBook = ParameterlessRelayCommand.From(this.OpenNewBook);
        }

        public string Title => _story.Title;

        public string ParagraphContent
        {
            get
            {
                var cont = _story.CurrentParagraphContent;
                return cont.Replace("\n", " ");
            }
        }
        public string NumParagraph => _story.IsEmpty?"":$"Paragraph #{_story.CurrentParagraph}";
        public string Visible => _story.IsEmpty ? "Hidden" : "Visible";
        public ObservableCollection<string> ActionPossible => new ObservableCollection<string>(_story.Actions());

        public ObservableCollection<string> ParagraphParcouru { get; set; }
        public string Message => DeterminerMessage();

        private string DeterminerMessage()
        {
            var msg = "";

            if (_story.IsFinished)
            {
                msg = $"Vous avez quitté le paragraphe {_story.LastParagraph} pour le paragraphe {_story.CurrentParagraph}\nVous avez atteind la fin de l'histoire.";
            }
            else if (_story.AlreadyPass() && _story.ChoiceDone.Count > 1)
            {
                msg = "Vous etes déjà passé par ce paragraphe.";
            }
            else if (_story.CurrentParagraph != 1)
            {
                msg =
                    $"Vous avez quitté le paragraphe {_story.LastParagraph} pour aller au paragraphe {_story.CurrentParagraph}";
            }

            return msg;

        }

        public void GoToNext(string action)
        {
            var pos = action.Split("(aller au ");
            var sr = pos[1];
            var posss = sr.Substring(0, sr.Length - 1);
            var numDestination = int.Parse(posss);
            _story.AddNextParaInChoice(numDestination);
            Changed(nameof(GoToNext));
        }

        private string _comboSelected;

        public string ComboSelected
        {
            get => _comboSelected;

            set
            {
                if (value == null) return;
                _comboSelected = value;
                var pos = value.Split(" -");
                var nb = int.Parse(pos[0]);
                _story.ComboSelected(nb);
                ParagraphParcouru.Clear();
                Changed(nameof(ComboSelected));
            }
        }

        public void GoBackAsked()
        {
            _story.GoBack();
            Changed(nameof(GoBackAsked));

        }


        public void SaveToFile()
        {
            if (!_story.IsEmpty) _manager.SaveSession(_story, "");
        }

        public void OpenSession()
        {
            var path = OpenFileDialog.ResourceIdentifier;
            if ("".Equals(path)) return;
            //sauver la session courante
            if (!_story.IsEmpty) _manager.SaveSession(_story, "");
            //charger la session demande
            _story = _manager.LoadSession(path);
            Changed("load");
        }

        public void OpenNewBook()
        {
            var path = OpenFileDialog.ResourceIdentifier;
            if ("".Equals(path)) return;
            //sauver la session courante
            if (!_story.IsEmpty) _manager.SaveSession(_story, "");
            _story = _manager.LoadSession(path, true);
            //sauver le path du nouveau livre
            _manager.SaveSession(_story, path);
            Changed("load");
        }


        private void Changed(string nameOf)
        {
            ParagraphParcouru = new ObservableCollection<string>(_story.ChoiceDoneS);
            PropertyMessage(nameOf);
            PropertyChange(nameof(ParagraphContent));
            PropertyChange(nameof(ActionPossible));
            PropertyChange(nameof(ParagraphParcouru));
            if (!"GoToNext".Equals(nameOf)) PropertyChange(nameof(Message));
            PropertyChange(nameof(NumParagraph));
            ChangedTitle(nameOf);
        }

        private void PropertyMessage(string nameOf)
        {
            if (!"GoToNext".Equals(nameOf)) return;
            PropertyChange(nameof(Message));
        }

        private void ChangedTitle(string nameOf)
        {

            if (!"load".Equals(nameOf)) return;
            PropertyChange(nameof(Visible));
            PropertyChange(nameof(Title));
        }

        private void PropertyChange(string nameOf)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameOf));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand GoBack { get; set; }
        public ICommand Changer { get; set; }
        public ICommand Save { get; set; }
        public ICommand Open { get; set; }
        public ICommand NewBook { get; set; }
        
    }
}
