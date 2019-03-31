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
            int number = 3;
            int outPutNum = 5;
            string s_path = "E:/开发内容/2.txt";

            Program program = new Program();
            program.TestMethod(l_path, s_path, number, outPutNum);
            Assert.AreEqual(1423, program.characters);
            Assert.AreEqual(6, program.lines);
        }
    }
}
