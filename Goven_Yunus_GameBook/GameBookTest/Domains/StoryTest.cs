using System;
using System.Collections.Generic;
using System.Text;
using GameBook.Domains;
using NUnit.Framework;
using Action = GameBook.Domains.Action;

namespace GameBookTest.Domains
{
    class StoryTest
    {
        [Test]
        public void testLastParagraph()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1");
            map.Add(1, p);
            Book book = new Book("Livre", map);
            Story story = new Story(book);
            story.AddNextParaInChoice(2);
            Assert.AreEqual(1, story.LastParagraph);

        }
         [Test]
          public void testAlreadyNotPass()
          {
              IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
              Paragraph p = new Paragraph(1, "p1");
              map.Add(1, p);

              Book book = new Book("Livre", map);
              Story story = new Story(book);

              story.AddNextParaInChoice(2);
              story.CurrentParagraph = 2;

              Assert.False(story.AlreadyPass());
          }
        [Test]
         public void testAlreadyNoPass()
         {
             IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
             Paragraph p = new Paragraph(1, "p1");
             map.Add(1, p);

             Book book = new Book("Livre", map);
             Story story = new Story(book,1);
             story.AddNextParaInChoice(1);
             Assert.True(story.AlreadyPass());
         }

        [Test]
        public void testIsFinished()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1");
            map.Add(1, p);

            Book book = new Book("Livre", map);
            Story story = new Story(book);
            Assert.True(story.IsFinished);
        }
        [Test]
         public void testIsNotFinished()
         {
             IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
             Paragraph p = new Paragraph(1, "p1", new HashSet<GameBook.Domains.Action>() { new Action("oki", 2) });
             map.Add(1, p);

             Book book = new Book("Livre", map);
             Story story = new Story(book);
             Assert.False(story.IsFinished);
         }
         [Test]
          public void testAddParagraphinChoice()
          {
              IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
              Paragraph p = new Paragraph(1, "p1");
              map.Add(1, p);

              Book book = new Book("Livre", map);
              Story s = new Story(book);
              s.AddNextParaInChoice(2);

              Assert.True(s.ChoiceDone.Contains(2));

          }
     
        [Test]
          public void testParagrapheCourant()
          {
              Book book = new Book("Livre", new Dictionary<int, Paragraph>());
              Story s =new Story(book);
              Assert.AreEqual(1, s.CurrentParagraph);
              s.CurrentParagraph += 2;
              Assert.AreEqual(3, s.CurrentParagraph);
          }

        [Test]
         public void testChoieDone()
         {
             Book book = new Book("Livre", new Dictionary<int, Paragraph>());
             Story s = new Story(book);
             Assert.AreEqual(1, s.ChoiceDone.Count);
             Assert.True(s.ChoiceDone.Contains(1));
             Assert.AreEqual("Livre", s.Title);
         }

        [Test]
        public void SetChoiceDone()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1", new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2)
            });
            Paragraph p1 = new Paragraph(2, "p2");
            Paragraph p2 = new Paragraph(3, "p3");
            map.Add(1, p);
            map.Add(2, p1);
            map.Add(3, p2);

            Book book = new Book("Livre", map);
            Story s = new Story(book);
            s.ChoiceDone = new List<int>() { 1, 2, 3 };
            Assert.AreEqual(3, s.ChoiceDone.Count);
        }
        [Test]
        public void testAction()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1", new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2)
            });
            Paragraph p1 = new Paragraph(2, "p2");
            Paragraph p2 = new Paragraph(3, "p3");
            map.Add(1, p);
            map.Add(2, p1);
            map.Add(3, p2);

            Book book = new Book("Livre", map);
            Story s = new Story(book);
            Assert.AreEqual(2, s.Actions().Count);
        }
        [Test]
        public void GoBackFirstParagraph()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1", new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2)
            });
            Paragraph p1 = new Paragraph(2, "p2");
            Paragraph p2 = new Paragraph(3, "p3");
            map.Add(1, p);
            map.Add(2, p1);
            map.Add(3, p2);

            Book book = new Book("Livre", map);
            Story s = new Story(book);
            s.GoBack();
            Assert.AreEqual(1,s.CurrentParagraph);
        }
        [Test]
        public void GoBackOnSecondParagraph()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1", new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2)
            });
            Paragraph p1 = new Paragraph(2, "p2");
            Paragraph p2 = new Paragraph(3, "p3");
            map.Add(1, p);
            map.Add(2, p1);
            map.Add(3, p2);

            Book book = new Book("Livre", map);
            Story s = new Story(book);
            s.AddNextParaInChoice(2);
            s.GoBack();
            Assert.AreEqual(1, s.CurrentParagraph);
        }
        [Test]
        public void GetCuurentParagraphContent()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1", new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2)
            });
            Paragraph p1 = new Paragraph(2, "p2");
            Paragraph p2 = new Paragraph(3, "p3");
            map.Add(1, p);
            map.Add(2, p1);
            map.Add(3, p2);

            Book book = new Book("Livre", map);
            Story s = new Story(book);
            Assert.AreEqual(s.CurrentParagraphContent,"p1" );
        }
        [Test]
        public void IsEmpty()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
          

            Book book = new Book("Livre", map);
            Story s = new Story(book);
            Assert.True(s.IsEmpty);
        }
        [Test]
        public void ComboSelct()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1", new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2)
            });
            Paragraph p1 = new Paragraph(2, "p2");
            Paragraph p2 = new Paragraph(3, "p3");
            map.Add(1, p);
            map.Add(2, p1);
            map.Add(3, p2);

            Book book = new Book("Livre", map);
            Story s = new Story(book);
            s.AddNextParaInChoice(2);
            s.AddNextParaInChoice(3);

            s.ComboSelected(1);
            Assert.AreEqual(1, s.ChoiceDoneS.Count);


        }
    }
}
