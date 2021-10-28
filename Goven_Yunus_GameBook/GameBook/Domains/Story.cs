using System.Collections.Generic;
using System.Linq;

namespace GameBook.Domains
{
    public interface IStory
    {
        IBook Book { get; set; }
        int CurrentParagraph { get; set; }
        List<int> ChoiceDone { get; set; }
        SortedSet<string> ChoiceDoneS { get; set; }
        string CurrentParagraphContent { get; set; }
        void AddNextParaInChoice(int number);
        void GoBack();
        void ComboSelected(int number);
        bool IsFinished { get; }
        bool AlreadyPass();
        int LastParagraph { get; }
        string Title { get; }
        bool IsEmpty { get; }
        IList<string> Actions();

    }
    public class Story : IStory
    {
        public IBook Book { get; set; }
        private Paragraph _paragraphCurrent;
        public int CurrentParagraph { get; set; }
        public List<int> ChoiceDone { get; set; }
        public SortedSet<string> ChoiceDoneS { get; set; }

        public Story(IBook book)
        {
            Book = book;
            Initialize(1);
        }
        public Story(IBook book, int current)
        {
            Book = book;
            Initialize(current);
        }

        private void Initialize(int nb)
        {
            CurrentParagraph = nb;
            _paragraphCurrent = Book.GetThisParagraph(CurrentParagraph);
            ChoiceDone = new List<int>() { 1 };
            ChoiceDoneS = new SortedSet<string>() { _paragraphCurrent.GetContentInShort() };
            CurrentParagraphContent = _paragraphCurrent.Content;
        }

        public string CurrentParagraphContent { get; set; }

        public void AddNextParaInChoice(int number)
        {
            ChoiceDone.Add(number);
            CurrentParagraph = number;
            _paragraphCurrent = Book.GetThisParagraph(CurrentParagraph);
            CurrentParagraphContent = _paragraphCurrent.Content;
            ChoiceDoneS.Add(_paragraphCurrent.GetContentInShort());
        }

        public void GoBack()
        {
            if (ChoiceDone.Count == 1) return;
            var cur = CurrentParagraph;
            CurrentParagraph = LastParagraph;
            _paragraphCurrent = Book.GetThisParagraph(CurrentParagraph);
            RemoveWhenGoBack(cur);
            CurrentParagraphContent = _paragraphCurrent.Content;
        }

        private void RemoveWhenGoBack(int element)
        {
            var pos = ChoiceDone.LastIndexOf(element);
            ChoiceDone.RemoveAt(pos);
            if (!ChoiceDone.Contains(element)) ChoiceDoneS.RemoveWhere(x => x.StartsWith(element.ToString()));
        }

        public void ComboSelected(int number)
        {
            var index = ChoiceDone.LastIndexOf(number);
            var length = ChoiceDone.Count;

            NewComboBox(index, length);

            _paragraphCurrent = Book.GetThisParagraph(number);
            CurrentParagraph = number;
            CurrentParagraphContent = _paragraphCurrent.Content;
        }

        private void NewComboBox(int index, int length)
        {
            ChoiceDone.RemoveRange(index + 1, length - index - 1);
            var newDone = new SortedSet<string>();
            foreach (var variable in from variable in ChoiceDoneS
                                     from nombre in ChoiceDone
                                     let contains = variable.Contains($"{nombre}")
                                     where contains
                                     select variable)
            {
                newDone.Add(variable);
            }

            ChoiceDoneS = newDone;
        }

        public bool IsFinished => !Book.IsEmpty && _paragraphCurrent.ChoiceIsEmpty();

        public bool AlreadyPass()
        {
            var copyOfChoix = new List<int>();
            for (var i = 0; i < ChoiceDone.Count - 1; ++i)
                copyOfChoix.Add(ChoiceDone[i]);
            return copyOfChoix.Contains(CurrentParagraph);
        }
        public int LastParagraph => ChoiceDone[^2];
        public string Title => Book.Title;
        public bool IsEmpty => Book.IsEmpty;

        public IList<string> Actions()
        {
            var actionsDispo = _paragraphCurrent.Actions;
            return actionsDispo.Select(action => action.Information).ToList();
        }

    }
}
