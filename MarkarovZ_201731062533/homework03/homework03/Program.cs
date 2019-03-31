using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace homework3
{
    class Word
    {
        public string text;
        public int num;
        public Word(string text)
        {
            this.text = text;
            num = 1;
        }
    }
    class Program
    {
        List<string> Lines = new List<string>();//从文档读取到的文本内容
        List<Word> Words = new List<Word>();//检索到的单词

        int lines = 0;//有效行数
        int characters = 0;//有效字符数


        static void Main(string[] args)
        {
            Program program = new Program();
            string path = "E:/开发内容/1.txt";
            program.FileRead(path);
            program.MainCount();
            List<string> words = program.WordSort();
            Console.WriteLine("characters:"+program.characters);
            Console.WriteLine("words:" + program.Words.Count);
            Console.WriteLine("lines:" + program.lines);
            int i = 0;
            for (; i < 10; i++)
            {
                foreach (var iword in program.Words)
                {
                    if (words[i] == iword.text)
                    {
                        Console.WriteLine(iword.text + " " + iword.num);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 读取文档
        /// </summary>
        /// <param name="path">文档路径</param>
        void FileRead(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Lines.Add(line);
            }
        }
        /// <summary>
        /// 对字符、单词、行计数
        /// </summary>
        void MainCount()
        {
            int wordJudge = 0;
            bool isWord = true;
            string _word = "";
            foreach (var str in Lines)
            {
                int lineJudge = 0;
                foreach (var word in str)
                {
                    if (word != ' ')
                    {
                        lineJudge++;
                        characters++;
                        if ((isWord == true) && (((word >= 65) && (word <= 90)) || ((word >= 97) && (word <= 122))))//判断是否为字母内容
                        {
                            wordJudge++;
                            _word = _word + word;
                        }
                        else
                        {
                            if (wordJudge == 0)
                            {
                                isWord = false;
                            }
                            else
                            {
                                if ((wordJudge >= 4) && ((word >= 48) && (word <= 57)))
                                {
                                    _word = _word + word;
                                }
                                else
                                {
                                    if (wordJudge >= 4)
                                    {
                                        int flag = 0;
                                        int i = 0;
                                        foreach (var nword in Words)
                                        {
                                            if (nword.text == _word)
                                            {
                                                nword.num++;
                                                flag++;
                                                break;
                                            }
                                            i++;
                                        }
                                        if (flag == 0)
                                        {
                                            Word aword = new Word(_word);
                                            Words.Add(aword);
                                        }
                                        wordJudge = 0;
                                        _word = "";
                                    }
                                    else
                                    {
                                        wordJudge = 0;
                                        _word = "";
                                        isWord = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        isWord = true;
                        if (wordJudge >= 4)
                        {
                            int flag = 0;
                            int i = 0;
                            foreach (var nword in Words)
                            {
                                if (nword.text == _word)
                                {
                                    nword.num++;
                                    flag++;
                                    break;
                                }
                                i++;
                            }
                            if (flag == 0)
                            {
                                Word aword = new Word(_word);
                                Words.Add(aword);
                            }
                            wordJudge = 0;
                            _word = "";
                        }
                        else
                        {
                            wordJudge = 0;
                            _word = "";
                        }
                    }
                }
                if (wordJudge >= 4)
                {
                    int flag = 0;
                    int i = 0;
                    foreach (var nword in Words)
                    {
                        if (nword.text == _word)
                        {
                            nword.num++;
                            flag++;
                            break;
                        }
                        i++;
                    }
                    if (flag == 0)
                    {
                        Word aword = new Word(_word);
                        Words.Add(aword);
                    }
                    wordJudge = 0;
                    _word = "";
                }
                if (lineJudge != 0)
                {
                    lines++;
                }
            }
        }
        /// <summary>
        /// 输出单词排序
        /// </summary>
        List<string> WordSort()
        {
            List<string> words = new List<string>();
            foreach(var word in Words)
            {
                words.Add(word.text);
            }
            words.Sort();
            return words;
        }
    }
}
