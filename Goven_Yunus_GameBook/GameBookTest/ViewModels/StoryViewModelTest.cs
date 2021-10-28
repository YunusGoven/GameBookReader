using System;
using System.Collections.Generic;
using System.Text;
using GameBook.Domains;
using GameBook.Storage;
using GameBookViewModel.Commands;
using GameBookViewModel.ViewModels;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using NUnit.Framework;
using Action = GameBook.Domains.Action;

namespace GameBookTest.ViewModels
{
    class StoryViewModelTest
    {
        [Test]
        public void testTtile()
        {
            Story s = new Story(new Book("title", new Dictionary<int, Paragraph>()));
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();

            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Assert.AreEqual("title", vm.Title);

        }

        [Test]
        public void testSetCommand()
        {
            Story s = new Story(new Book("title", new Dictionary<int, Paragraph>()));
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();



            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            var gb = vm.GoBack;
            vm.GoBack = new ParameterlessRelayCommand();
            Assert.AreNotEqual(gb, vm.GoBack);

            gb = vm.Changer;
            vm.Changer = new ParameterlessRelayCommand();
            Assert.AreNotEqual(gb, vm.Changer);

            gb = vm.Save;
            vm.Save = new ParameterlessRelayCommand();
            Assert.AreNotEqual(gb, vm.Save);

            gb = vm.Open;
            vm.Open = new ParameterlessRelayCommand();
            Assert.AreNotEqual(gb, vm.Open);

            gb = vm.NewBook;
            vm.NewBook = new ParameterlessRelayCommand();
            Assert.AreNotEqual(gb, vm.NewBook);
        }

        [Test]
        public void testGetParagraphGoBack()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            dic.Add(1, new Paragraph(1, "a"));
            dic.Add(2, new Paragraph(2, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);

            vm.GoBack.Execute(null);
            Assert.AreEqual(1, s.ChoiceDone.Count);
        }

        [Test]
        public void testGetParagraphCombo()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            dic.Add(1, new Paragraph(1, "a"));
            dic.Add(2, new Paragraph(2, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Assert.AreEqual(null, vm.ComboSelected);
        }

        [Test]
        public void testGetParagraphComboSelect()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            dic.Add(1, new Paragraph(1, "a"));
            dic.Add(2, new Paragraph(2, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            s.AddNextParaInChoice(2);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            vm.ComboSelected = "1";
            Assert.AreEqual(1, s.ChoiceDoneS.Count);
        }
        [Test]
        public void testGetParagraphComboSelectNULL()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            dic.Add(1, new Paragraph(1, "a"));
            dic.Add(2, new Paragraph(2, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            s.AddNextParaInChoice(2);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            vm.ComboSelected = null;
            Assert.AreEqual(2, s.ChoiceDoneS.Count);
        }
        [Test]
        public void testGetParagraphButnVisible()
        {
            IStory b;
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            b = new Story(new Book("title", new Dictionary<int, Paragraph>() { { 1, new Paragraph(1, "cont") } }));

            StoryViewModel vm = new StoryViewModel(b, sm.Object);

            Assert.AreEqual(vm.Visible, "Visible");
        }

        [Test]
        public void testGetParagraphActions()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            var act = new HashSet<Action>();
            act.Add(new Action("va la", 2));
            dic.Add(1, new Paragraph(1, "a", act));
            dic.Add(2, new Paragraph(2, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);

            Assert.AreEqual(1, vm.ActionPossible.Count);


        }

        [Test]
        public void testGetParagraphCont()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            var act = new HashSet<Action>();
            act.Add(new Action("va la", 2));
            dic.Add(1, new Paragraph(1, "a", act));
            dic.Add(2, new Paragraph(2, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);

            Assert.AreEqual("a", vm.ParagraphContent);


        }
        [Test]
        public void testGetParagraphParrcouru()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            dic.Add(1, new Paragraph(1, "a"));
            dic.Add(2, new Paragraph(2, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            s.AddNextParaInChoice(2);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Assert.AreEqual(2, vm.ParagraphParcouru.Count);
        }
        [Test]
        public void testGetMessageAlreadyPass()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();

            var act = new HashSet<Action>();
            act.Add(new Action("va la", 2));
            dic.Add(1, new Paragraph(1, "a", act));
            dic.Add(2, new Paragraph(2, "ab"));
            dic.Add(3, new Paragraph(3, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            s.AddNextParaInChoice(2);
            s.AddNextParaInChoice(1);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Assert.AreEqual("Vous etes déjà passé par ce paragraphe.", vm.Message);

        }
        [Test]
        public void testGetMessageNextParagraph()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();

            var act = new HashSet<Action>();
            act.Add(new Action("va la", 2));
            dic.Add(1, new Paragraph(1, "a", act));
            dic.Add(2, new Paragraph(2, "ab", act));
            dic.Add(3, new Paragraph(3, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            s.AddNextParaInChoice(2);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Assert.AreEqual("Vous avez quitté le paragraphe 1 pour aller au paragraphe 2", vm.Message);

        }
        [Test]
        public void testGetMessageFinish()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();

            var act = new HashSet<Action>();
            act.Add(new Action("va la", 2));
            dic.Add(1, new Paragraph(1, "a", act));
            dic.Add(2, new Paragraph(2, "ab"));
            dic.Add(3, new Paragraph(3, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            s.AddNextParaInChoice(2);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Assert.True(vm.Message.Contains("fin de "));

        }
        [Test]
        public void testGetMessageGoNext()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();

            var act = new HashSet<Action>();
            var a = new Action("va la", 2);
            act.Add(a);
            dic.Add(1, new Paragraph(1, "a", act));
            dic.Add(2, new Paragraph(2, "ab"));
            dic.Add(3, new Paragraph(3, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            vm.GoToNext(a.Information);
            Assert.True(s.ChoiceDone.Contains(2));

        }
        [Test]
        public void testGetMessageSaveBook()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();

            var act = new HashSet<Action>();
            var a = new Action("va la", 2);
            act.Add(a);
            dic.Add(1, new Paragraph(1, "a", act));
            dic.Add(2, new Paragraph(2, "ab"));
            dic.Add(3, new Paragraph(3, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            vm.SaveToFile();
            sm.Verify(m => m.SaveSession(s, It.IsAny<string>()), Times.AtLeast(1));

        }
        [Test]
        public void testGetMessageSaveBookEmpty()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            Mock<IChooseResource> cr = new Mock<IChooseResource>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();

            bo = new Book("akha", dic);
            var s = new Story(bo);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            vm.SaveToFile();
            sm.Verify(m => m.SaveSession(s, It.IsAny<string>()), Times.Never);

        }
        [Test]
        public void testOpenNewBook()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();

            bo = new Book("akha", dic);
            var s = new Story(bo);
            sm.Setup(s => s.LoadSession(It.IsAny<string>(), true)).Returns(s);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Mock<IChooseResource> m = new Mock<IChooseResource>();
            vm.OpenFileDialog = m.Object;
            m.Setup(m => m.ResourceIdentifier).Returns(It.IsAny<string>());



            vm.OpenNewBook();

            Assert.AreEqual("Hidden", vm.Visible);
        }
        [Test]
        public void testOpenNewBookPathEmpty()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            bo = new Book("akha", dic);
            var s = new Story(bo);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Mock<IChooseResource> m = new Mock<IChooseResource>();
            vm.OpenFileDialog = m.Object;
            m.Setup(m => m.ResourceIdentifier).Returns("");
            vm.OpenNewBook();



            Assert.AreEqual("Hidden", vm.Visible);
            sm.Verify(s => s.LoadSession(It.IsAny<string>(), true), Times.Never);
        }
        [Test]
        public void testOpenNewBookSaveCUURENTbOOK()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            var a = new Action("va la", 2);
            var act = new HashSet<Action>();
            act.Add(a);
            dic.Add(1, new Paragraph(1, "a", act));
            dic.Add(2, new Paragraph(2, "ab"));
            dic.Add(3, new Paragraph(3, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            sm.Setup(s => s.LoadSession(It.IsAny<string>(), true)).Returns(s);

            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Mock<IChooseResource> m = new Mock<IChooseResource>();
            vm.OpenFileDialog = m.Object;
            m.Setup(m => m.ResourceIdentifier).Returns(It.IsAny<string>());
            vm.OpenNewBook();

            sm.Verify(m => m.SaveSession(s, It.IsAny<string>()), Times.Exactly(2));
        }
        [Test]
        public void testOpenSession()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();

            bo = new Book("akha", dic);
            var s = new Story(bo);
            sm.Setup(s => s.LoadSession(It.IsAny<string>())).Returns(s);

            StoryViewModel vm = new StoryViewModel(s, sm.Object);

            Mock<IChooseResource> m = new Mock<IChooseResource>();
            vm.OpenFileDialog = m.Object;
            m.Setup(m => m.ResourceIdentifier).Returns(It.IsAny<string>());

            vm.OpenSession();
            sm.Verify(m => m.LoadSession(It.IsAny<string>()), Times.AtLeast(1));
            Assert.AreEqual("Hidden", vm.Visible);
        }
        [Test]
        public void testOpenSessionPathEmpty()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();

            bo = new Book("akha", dic);
            var s = new Story(bo);
            StoryViewModel vm = new StoryViewModel(s, sm.Object);
            Mock<IChooseResource> m = new Mock<IChooseResource>();
            vm.OpenFileDialog = m.Object;
            m.Setup(m => m.ResourceIdentifier).Returns("");



            vm.OpenSession();





            m.VerifyGet(c => c.ResourceIdentifier, Times.Once);
            Assert.AreEqual("Hidden", vm.Visible);
            sm.Verify(s => s.LoadSession(It.IsAny<string>()), Times.Never);
        }
        [Test]
        public void testOpenSessionSaveCUURENTbOOK()
        {
            Mock<IBook> b = new Mock<IBook>();
            Mock<IStorageManager> sm = new Mock<IStorageManager>();
            var bo = b.Object;
            var dic = new Dictionary<int, Paragraph>();
            var a = new Action("va la", 2);
            var act = new HashSet<Action>();
            act.Add(a);
            dic.Add(1, new Paragraph(1, "a", act));
            dic.Add(2, new Paragraph(2, "ab"));
            dic.Add(3, new Paragraph(3, "ab"));
            bo = new Book("akha", dic);
            var s = new Story(bo);
            sm.Setup(s => s.LoadSession(It.IsAny<string>())).Returns(s);

            StoryViewModel vm = new StoryViewModel(s, sm.Object);

            Mock<IChooseResource> m = new Mock<IChooseResource>();
            vm.OpenFileDialog = m.Object;
            m.Setup(m => m.ResourceIdentifier).Returns(It.IsAny<string>());
            vm.OpenSession();
            m.VerifyGet(c => c.ResourceIdentifier, Times.Once);
            Assert.True(vm.NumParagraph.Contains("1"));
            sm.Verify(m => m.LoadSession(It.IsAny<string>()), Times.Exactly(1));
            sm.Verify(m => m.SaveSession(s, It.IsAny<string>()), Times.Exactly(1));
        }




    }

}
