using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameBook.Domains;
using GameBook.Storage;
using NUnit.Framework;

namespace GameBookTest.Storage
{
    class StorageManagerTest
    {
        [Test]
        public void BookIsImported()
        {
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                        "\\GameBookSession\\Merlin_l'enchanteur.txt");
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var path = $"{projectDirectory}\\TestRessource\\Merlin_l'enchanteur.json";
            IStorageManager m = new StorageManager();
            var b = m.LoadSession(path, true);
            Assert.AreEqual(b.Title, "Merlin l'enchanteur");


        }
        [Test]
        public void BookIsNotImported()
        {
            IStorageManager m = new StorageManager();
            var b = m.LoadSession("ee", true);
            Assert.AreEqual(b.Title, "Fichier Json incorrect");

        }

        [Test]
        public void SaveNotSessionIfParagraphIsEmpty()
        {
            var ba = new Book("z", new Dictionary<int, Paragraph>());
            var b = new Story(ba);
            IStorageManager m = new StorageManager();
            m.SaveSession(b, "erreur");
            Assert.False(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                     "\\GameBookSession\\z.txt"));

        }

        [Test]
        public void SaveSessionFirstTime()
        {
            IStorageManager m = new StorageManager();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var path = $"{projectDirectory}\\TestRessource\\Merlin_l'enchanteur.json";
            var b = m.LoadSession(path, true);

            m.SaveSession(b, path);

            var sespath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                         "\\GameBookSession\\Merlin_l'enchanteur.txt";
            Assert.True(File.Exists(sespath));
        }
        [Test]
        public void SaveSession()
        {

            IStorageManager m = new StorageManager();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var path = $"{projectDirectory}\\TestRessource\\Merlin_l'enchanteur.json";
            var b = m.LoadSession(path, true);

            m.SaveSession(b, path);
            m.SaveSession(b, "");

            var sespath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                          "\\GameBookSession\\Merlin_l'enchanteur.txt";
            Assert.True(File.Exists(sespath));
        }
        [Test]
        [Order(1)]
        public void Load()
        {
            var sespath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                          "\\GameBookSession\\Merlin_l'enchanteur.txt";
            var pathFold = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                          "\\GameBookSession";
            if (File.Exists(sespath))
                Directory.Delete(pathFold, true);
            Directory.CreateDirectory(pathFold);
            IStorageManager m = new StorageManager();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var path = $"{projectDirectory}\\TestRessource\\Merlin_l'enchanteur.json";
            var b = m.LoadSession(path, true);

            m.SaveSession(b, path);
            m.SaveSession(b, "");
            var ses = m.LoadSession(sespath);
            Assert.True(ses.Title.Contains("Merlin"));
        }

        [Test]
        public void SessionFileNotFound()
        {
            IStorageManager m = new StorageManager();
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                       "\\GameBookSession\\Mserlin_l'enchanteur.txt";
            var b = m.LoadSession(path);
            Assert.AreEqual(b.Title, "Fichier non trouvé");
        }

        [Test]
        public void GetLastStorySaved()
        {
            IStorageManager m = new StorageManager();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var pathLivre = $"{projectDirectory}\\TestRessource\\Merlin_l'enchanteur.json";

            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\GameBookSession";
            var dossier = Directory.CreateDirectory(path);
            File.CreateText(path + "\\Merlin_l'enchanteur.txt").Write(pathLivre);

            var s = m.GetLastStory();
            Assert.True(s.Title.Contains("Vide"));

        }
        [Test]
        public void GetLastStoryDirectoryEmpty()
        {
            IStorageManager m = new StorageManager();

            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\GameBookSession";
            Directory.Delete(path, true);


            var s = m.GetLastStory();
            Assert.True(s.Title.Contains("Veuillez ouvrir"));

        }

        [Test]
        public void GetLastStoryEmpty()
        {
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\GameBookSession";
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
            var m = new StorageManager();

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var pathLivre = $"{projectDirectory}\\TestRessource\\Merlin_l'enchanteur.json";

            var s = m.LoadSession(pathLivre, false);
            m.SaveSession(s, pathLivre);
            s.AddNextParaInChoice(2);
            m.SaveSession(s, "");

            var nes = m.GetLastStory();
            Assert.True(nes.Title.Contains("Merlin"));

        }

    }
}
