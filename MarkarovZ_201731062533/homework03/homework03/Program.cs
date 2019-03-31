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

        public static Program program = new Program();

        static void Main(string[] args)
        {
            string path = "E:/开发内容/1.txt";
            program.FileRead(path);
            program.MainCount();
            program.Output();
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
                    //判断当前检测字符是否为空
                    if (word != ' ')
                    {
                        lineJudge++;
                        characters++;
                        //判断当前是否为单词检测状态，判断当前检测字符是否为字母
                        if ((isWord == true) && (((word >= 65) && (word <= 90)) || ((word >= 97) && (word <= 122))))//判断是否为字母内容
                        {
                            wordJudge++;
                            _word = _word + word;
                        }
                        else
                        {
                            //切换至非单词状态
                            if (wordJudge == 0)
                            {
                                isWord = false;
                            }
                            else
                            {
                                //判断当前是否为单词数字后缀
                                if ((wordJudge >= 4) && ((word >= 48) && (word <= 57)))
                                {
                                    _word = _word + word;
                                }
                                else
                                {
                                    //判断是否已经构成单词
                                    if (wordJudge >= 4)
                                    {
                                        WordAdd(_word);
                                        wordJudge = 0;
                                        _word = "";
                                    }
                                    //结束当前判断周期，重新切换至单词检测状态
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
                        //检测到空字符时结束当前判断周期，开启新周期
                        isWord = true;
                        //判断当前检测结果是否构成单词
                        if (wordJudge >= 4)
                        {
                            WordAdd(_word);
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
                //在行末判断是否已构成单词
                if (wordJudge >= 4)
                {
                    WordAdd(_word);
                    wordJudge = 0;
                    _word = "";
                }
                //判断当前行是否为有效行
                if (lineJudge != 0)
                {
                    lines++;
                }
            }
        }
        /// <summary>
        /// 往单词集合添加新单词
        /// </summary>
        /// <param name="_word">要添加的单词</param>
        void WordAdd(string _word)
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
        }
        /// <summary>
        /// 输出单词排序
        /// </summary>
        List<string> WordSort()
        {
            List<Word> HIVword = new List<Word>();
            int i = 0;
            for (; i < Words.Count; i++)
            {
                int j = 0;
                for (; j < Words.Count; j++)
                {
                    if (Words[i].num > Words[j].num)
                    {
                        Word m = Words[i];
                        Words[i] = Words[j];
                        Words[j] = m;
                    }
                }
            }
            i = 0;
            for (; i < 10; i++)
            {
                HIVword.Add(Words[i]);
            }
            List<string> words = new List<string>();
            foreach(var word in HIVword)
            {
                words.Add(word.text);
            }
            words.Sort();
            return words;
        }
        /// <summary>
        /// 输出内容至控制台
        /// </summary>
        void Output()
        {
            List<string> words = program.WordSort();
            Console.WriteLine("characters:" + program.characters);
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
    }
}
