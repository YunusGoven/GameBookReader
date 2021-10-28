using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameBook.Domains;
using GameBook.Storage;
using GameBookViewModel.ViewModels;
using Moq;
using NUnit.Framework;

namespace GameBookTest.ViewModels
{
    class ViewModelLocatorTest
    {
        [Test]
        public void testGetViewModelFileExist()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var pathLivre = $"{projectDirectory}\\TestRessource\\Merlin_l'enchanteur.json";

            var ma = new StorageManager();
            var b = ma.LoadSession(pathLivre, true);

            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\GameBookSession";
            var dossier = Directory.CreateDirectory(path);
            File.CreateText(path + "\\Merlin_l'enchanteur.txt").Write(pathLivre);


            ViewModelLocator vm = new ViewModelLocator();
            StoryViewModel viewModel = vm.StoryVm;
            Assert.NotNull(viewModel);
        } 




}
}