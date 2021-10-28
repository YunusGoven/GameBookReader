using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GameBook.Domains;
using Action = GameBook.Domains.Action;

namespace GameBook.Storage
{
    public interface IStorageManager
    {
        IStory LoadSession(string path, bool firtstime, params int[] curent);
        void SaveSession(IStory story, string pathOfJson);
        IStory LoadSession(string path);
        IStory GetLastStory();
    }
    public class StorageManager : IStorageManager
    {
        public IStory LoadSession(string path, bool firtstime, params int[] curent)
        {
            try
            {
                using var reader = new JsonTextReader(File.OpenText(path));
                var obj = (JObject)JToken.ReadFrom(reader);
                reader.Close();
                return GetJsonBook(obj, firtstime, curent);

            }
            catch (Exception e)
            {
                if (e is FileNotFoundException || e is JsonReaderException || e is FormatException || e is ArgumentException)
                {
                    return new Story(new Book("Fichier Json incorrect", new Dictionary<int, Paragraph>()));
                }
                return new Story(new Book("Exception", new Dictionary<int, Paragraph>()));
            }

        }

        private IStory GetJsonBook(JObject obj, bool firtstime, params int[] nb)
        {
            var title = (string)obj["title"];
            var paragraphs = ParseParagraph(obj["paragraphs"]);
            IBook book = new Book(title, paragraphs);
            IStory story = nb.Length == 0 ? new Story(book) : new Story(book, nb[0]);
            if (firtstime) story = VerifySessExist(story, book.Title);
            return story;
        }

        private IStory VerifySessExist(IStory story, string title)
        {
            title = title.Replace(" ", "_");
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\GameBookSession\\{title}.txt";
            var exsit = File.Exists(path);
            return exsit ? LoadSession(path) : story;
        }

        private string GetPathOfSession(string title)
        {
            title = title.Replace(" ", "_");
            var pathOfSessFolder =
                $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\GameBookSession";
            return $"{pathOfSessFolder}\\{title}.txt";
        }

        private void CreateSess(string fullPath, string pathOfJson)
        {
            using TextWriter textWriter =
                new StreamWriter(new FileStream(fullPath, FileMode.Create), Encoding.UTF8);
            textWriter.WriteLine(pathOfJson);
            textWriter.Close();
        }

        private string GetJsonPath(string fullPath)
        {
            using TextReader textReader =
                new StreamReader(new FileStream(fullPath, FileMode.Open), Encoding.UTF8);
            var pathOfJson = textReader.ReadLine();
            textReader.Close();
            return pathOfJson;
        }

        private string GetBookInfo(IStory story, string fullPath)
        {
            var pathJson = GetJsonPath(fullPath);
            File.Delete(fullPath);
            var num = story.CurrentParagraph;
            var choix = story.ChoiceDone;
            var choixString = story.ChoiceDoneS;
            var contentCurrent = story.CurrentParagraphContent;

            return InformationToWrite(pathJson, num, choix, choixString, contentCurrent);
        }

        private string InformationToWrite(string pathJson, int num, List<int> choix, SortedSet<string> choixString, string contentCurrent)
        {
            var info = new StringBuilder();

            info.Append($"{pathJson}\r\n");
            info.Append($"{num}-");
            AppendInString(info, choix.ConvertAll(x => x.ToString()), ",");
            AppendInString(info, choixString, "´");
            info.Append(contentCurrent);
            return info.ToString();
        }

        private void AppendInString(StringBuilder info, ICollection<string> col, string sep)
        {
            foreach (var element in col)
                info.Append(element + sep);
            info.Remove(info.Length - 1, 1);
            info.Append("\r\n");
        }

        public void SaveSession(IStory story, string pathOfJson)
        {
            var book = story?.Book;
            if (book == null || book.IsEmpty) return;
            var fullPath = GetPathOfSession(book.Title);

            try
            {
                if ("".Equals(pathOfJson))
                    WriteInFile(story, fullPath);
                else
                    CreateSessOnQuit(fullPath, pathOfJson);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void CreateSessOnQuit(string fullPath, string pathOfJson)
        {
            if (File.Exists(fullPath)) return;
            CreateSess(fullPath, pathOfJson);
        }

        private void WriteInFile(IStory story, string fullPath)
        {
            var info = GetBookInfo(story, fullPath);
            using TextWriter textWriter =
                new StreamWriter(new FileStream(fullPath, FileMode.OpenOrCreate), Encoding.UTF8);
            textWriter.WriteLine($"{info}");
            textWriter.Close();

        }

        ///path = chemin du fichier de la session
        public IStory LoadSession(string path)
        {
            try
            {
                return ChargeBookLoadSess(path);
            }
            catch (FileNotFoundException)
            {
                return new Story(new Book("Fichier non trouvé", new Dictionary<int, Paragraph>()));
            }
            catch (IndexOutOfRangeException)
            {
                return new Story(new Book("Contenu du fichier pas bon", new Dictionary<int, Paragraph>()));
            }
            catch (NullReferenceException)
            {
                return new Story(new Book("fichier JSON non trouvé", new Dictionary<int, Paragraph>()));
            }
            catch (FormatException)
            {
                return new Story(new Book("erreur de format pour les numero", new Dictionary<int, Paragraph>()));
            }
        }

        public IStory GetLastStory()
        {
            try
            {
                return LoadStory();
            }
            catch (IOException)
            {
                return new Story(new Book("Vide", new Dictionary<int, Paragraph>()));
            }
            catch (InvalidOperationException)
            {
                return new Story(new Book("Veuillez ouvrir un livre (New Book) au format Json", new Dictionary<int, Paragraph>()));
            }
        }
        private IStory LoadStory()
        {
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\GameBookSession";
            var dossier = Directory.CreateDirectory(path);
            var file = dossier.GetFiles()
                .OrderByDescending(files => files.LastWriteTime)
                .First();
            return LoadSession(file.FullName);
        }

        private IStory ChargeBookLoadSess(string path)
        {
            using TextReader textReader = new StreamReader(new FileStream(path, FileMode.Open), Encoding.UTF8);
            var infoInFile = textReader.ReadToEnd();
            textReader.Close();
            var infos = infoInFile.Split("\r\n");
            return CreateBook(infos);
        }

        private IStory CreateBook(string[] infos)
        {
            var infoLivre = infos[1].Split("-");
            var choixEffectuer = infoLivre[1].Split(",");
            var choixString = infos[2].Split("´");
            var currentParagraph = int.Parse(infoLivre[0]);
            var story = LoadSession(infos[0], false, currentParagraph);
            ChargeBook(story, choixEffectuer, choixString, infos[3]);

            return story;
        }

        private void ChargeBook(IStory story, string[] choixEffectuer, string[] choixString, string curentContent)
        {
            var choiceList = choixEffectuer.Select(int.Parse).ToList();
            var choixS = new SortedSet<string>();
            foreach (var choice in choixString)
                choixS.Add(choice);
            story.ChoiceDone = choiceList;
            story.ChoiceDoneS = choixS;
            story.CurrentParagraphContent = curentContent;
        }


        private IDictionary<int, Paragraph> ParseParagraph(JToken jToken)
        {
            var dictionary = new Dictionary<int, Paragraph>();

            foreach (var property in (JArray)jToken)
            {
                var num = int.Parse((string)property["number"] ?? "1");
                var content = (string)property["content"];
                var listActions = ParseActions(property);
                var paragraph = new Paragraph(num, content, listActions);
                dictionary.Add(num, paragraph);
            }
            return dictionary;
        }

        private ISet<Action> ParseActions(JToken property)
        {
            var listActions = new HashSet<Action>();
            foreach (var actions in (JArray)property["actions"])
            {
                var destination = int.Parse((string)actions["destiantion"] ?? string.Empty);
                var contentAction = (string)actions["contentAction"];
                var action = new Action(contentAction, destination);
                listActions.Add(action);
            }

            return listActions;
        }
    }
}
