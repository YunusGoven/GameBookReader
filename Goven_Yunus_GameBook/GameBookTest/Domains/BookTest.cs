using System;
using System.Collections.Generic;
using System.Text;
using GameBook.Domains;
using NUnit.Framework;
using Action = GameBook.Domains.Action;
namespace GameBookTest.Domains
{
    class BookTest
    {



        [Test]
        public void testIsEmpty()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1");

            Book book = new Book("Livre", map);
            Assert.True(book.IsEmpty);
        }
        [Test]
        public void testIsNotEmpty()
        {
            IDictionary<int, Paragraph> map = new Dictionary<int, Paragraph>();
            Paragraph p = new Paragraph(1, "p1");
            map.Add(1, p);

            Book book = new Book("Livre", map);
            Assert.False(book.IsEmpty);
        }



        [Test]
        public void testCreated()
        {
            Book book = new Book("Livre", new Dictionary<int, Paragraph>());
            Assert.AreEqual(book.GetType(), typeof(Book));
        }
        [Test]
        public void testBookTitle()
        {
            Book book = new Book("Livre", new Dictionary<int, Paragraph>());
            Assert.AreEqual("Livre", book.Title);
        }
        [Test]
        public void testBookTitleVide()
        {
            Book book = new Book(null, new Dictionary<int, Paragraph>());
            Assert.AreEqual("Pas de titre", book.Title);
        }





        [Test]
        public void testGetThisParagraphExist()
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
            Assert.True(p.Equals(book.GetThisParagraph(1)));
        }
        [Test]
        public void testGetThisParagraphNotExist()
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
            Assert.AreEqual("", book.GetThisParagraph(4).Content);
        }

    }
}
