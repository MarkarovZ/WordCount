using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework3;

namespace WordCountTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string l_path = "E:/开发内容/1.txt";
            string r_path = "E:/开发内容/2.txt";
            int num = 5;
            int outNum = 3;
            Program program = new Program();
            program.TestMethod(l_path, r_path, num, outNum);
            var res = Program.program;
            Assert.AreEqual(9656, res.characters);
            Assert.AreEqual(15, res.lines);
        }
    }
}
