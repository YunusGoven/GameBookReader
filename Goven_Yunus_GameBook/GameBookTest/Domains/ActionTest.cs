using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Action = GameBook.Domains.Action;


namespace GameBookTest.Domains
{
    class ActionTest
    {
        [TestCase(1, "act1")]
        [TestCase(2, "act2")]
        public void TestActionCreated(int i, string name)
        {
            Action action = new Action(name, i);
            Assert.AreEqual(action.GetType(), typeof(Action));
        }

        [TestCase(1, "act1")]
        [TestCase(2, "act2")]
        [TestCase(3, "act3")]
        [TestCase(4, "act4")]
        public void TestActionName(int i, string name)
        {
            Action action = new Action(name, i);
            Assert.AreEqual(name, action.Content);
        }
        [TestCase(1, "act1")]
        [TestCase(2, "act2")]
        [TestCase(3, "act3")]
        [TestCase(4, "act4")]
        public void TestActionNum(int i, string name)
        {
            Action action = new Action(name, i);
            Assert.AreEqual(i, action.Destination);
        }

        [TestCase(1, "act1")]
        [TestCase(2, "act2")]
        [TestCase(3, "act3")]
        [TestCase(4, "act4")]
        public void TestActionEquals(int i, string name)
        {
            Action action = new Action(name, i);
            Assert.AreEqual(action, action);
        }
        [TestCase(1, "act1")]
        [TestCase(2, "act2")]
        [TestCase(3, "act3")]
        [TestCase(4, "act4")]
        public void TestActionEqualsDifferentAction(int i, string name)
        {
            Action action = new Action(name, i);
            Action action1 = new Action(name, i);
            Assert.AreEqual(action1, action);
        }
        [TestCase(1, "act1")]
        [TestCase(2, "act2")]
        [TestCase(3, "act3")]
        [TestCase(4, "act4")]
        public void TestActionNotEqualsDifferentAction(int i, string name)
        {
            Action action = new Action(name, i);
            Action action1 = new Action("action7", 7);
            Assert.AreNotEqual(action1, action);
        }
        [TestCase(1, "act1")]
        [TestCase(2, "act2")]
        [TestCase(3, "act3")]
        [TestCase(4, "act4")]
        public void TestActionNotEqualsNull(int i, string name)
        {
            Action action = new Action(name, i);
            Assert.AreNotEqual(action, null);
        }
        [TestCase(1, "act1")]
        [TestCase(2, "act2")]
        [TestCase(3, "act3")]
        [TestCase(4, "act4")]
        public void TestActionNotEqualsInteger(int i, string name)
        {
            Action action = new Action("action1", 1);
            Assert.AreNotEqual(action, 12);
        }
        [TestCase(1, "act1")]
        [TestCase(2, "act2")]
        [TestCase(3, "act3")]
        [TestCase(4, "act4")]
        public void TestActionGetHashCode(int i, string name)
        {
            Action action = new Action(name, i);
            Assert.AreEqual(i.GetHashCode(), action.GetHashCode());
        }

        [Test]
        public void TestActionInforamtion()
        {
            Action a = new Action("akha", 3);
            Assert.AreEqual("akha (aller au 3)", a.Information);
        }
    }
}
