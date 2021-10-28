using System;
using System.Collections.Generic;
using System.Text;
using GameBook.Domains;
using NUnit.Framework;
using Action = GameBook.Domains.Action;

namespace GameBookTest.Domains
{
    class ParagraphTest
    {
        [Test]
        public void testParagrapheCreated()
        {
            ISet<Action> actions = new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2),new Action("act3",3)
            };
            var p = new Paragraph(1, "p1", actions);
            Assert.AreEqual(p.GetType(), typeof(Paragraph));
        }

        [Test]
        public void testParagraphWithoutAction()
        {
            var p = new Paragraph(1, "p1");
            Assert.True(p.Equals(p));
        }

        [Test]
        public void testParagrapheGetNum()
        {
            ISet<Action> actions = new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2),new Action("act3",3)
            };
            var p = new Paragraph(1, "p1", actions);
            Assert.AreEqual(1, p.Number);
        }
        [Test]
        public void testParagrapheGetDescription()
        {
            ISet<Action> actions = new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2),new Action("act3",3)
            };
            var p = new Paragraph(1, "p1", actions);
            Assert.AreEqual("p1", p.Content);
        }
        [Test]
        public void testParagrapheGetDescriptionVide()
        {
            ISet<Action> actions = new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2),new Action("act3",3)
            };
            var p = new Paragraph(1, null, actions);
            Assert.AreEqual("----", p.Content);
        }
        [Test]
        public void testParagrapheGetChoixPoss()
        {
            ISet<Action> actions = new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2),new Action("act3",3)
            };
            var p = new Paragraph(1, "p1", actions);
            foreach (var act in actions)
            {
                Assert.True(p.Actions.Contains(act));
            }

        }
        [Test]
        public void testParagrapheGetChoixPossNull()
        {
            ISet<Action> actions = new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2),new Action("act3",3)
            };
            var p = new Paragraph(1, "p1", null);
            Assert.AreNotEqual(actions, p.Actions);
            Assert.AreEqual(new List<Action>(), p.Actions);
        }

        [Test]
        public void testParagrapheEqualsNull()
        {
            var p = new Paragraph(1, "p1", null);
            Assert.False(p.Equals(null));
        }



        [Test]
        public void testParagrapheChoiceIsEmpty()
        {
            ISet<Action> actions = new HashSet<Action>()
            {
                new Action("act1",1),new Action("act2",2)
            };
            var p = new Paragraph(1, "p1", actions);
            Assert.False(p.ChoiceIsEmpty());

        }
        [Test]
        public void testParagrapheChoiceIsNotEmpty()
        {

            var p = new Paragraph(1, "p1", new HashSet<Action>());
            Assert.True(p.ChoiceIsEmpty());

        }

        [Test]
        public void testHashCode()
        {
            var p = new Paragraph(1, "p1", new HashSet<Action>());
            string description = "p1";
            int number = 1;

            Assert.AreEqual(p.GetHashCode(), HashCode.Combine(description, number));

        }

        [Test]
        public void GetInfoShort()
        {
            var p = new Paragraph(1, "Il etait une fois un chat", new HashSet<Action>());
            Assert.AreEqual(p.GetContentInShort(), "1 - Il etait une fois ");
        }
        [Test]
        public void GetInfoShortContShort()
        {
            var p = new Paragraph(1, "Il etait", new HashSet<Action>());
            Assert.AreEqual(p.GetContentInShort(), "1 - Il etait");
        }
    }
}
