using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework3
{
    public class Res
    {
        public int cha;
        public int line;
        public Res(int a,int b)
        {
            cha = a;
            line = b;
        }
    }

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
    public class Program
    {
        List<string> Lines = new List<string>();//从文档读取到的文本内容
        List<Word> Words = new List<Word>();//检索到的单词
        List<Word> MutipleWords = new List<Word>();//检索到的词组

        List<string> SaveStrs = new List<string>();

        public int lines = 0;//有效行数
        public int characters = 0;//有效字符数

        public static int number;

        public static int outPutNum;

        public static string path;
        public static string s_path;

        public static Program program = new Program();

        static void Main(string[] args)
        {
            program.TestMethod();
        }


        public void TestMethod()
        {
            program.DataInput();
            program.FileRead(path);
            program.MainCount();
            program.Output();
            program.FileSave();
        }
        public Res TestMethod(string l_path,string S_path,int num,int outNum)
        {
            path = l_path;

            number = num;
            outPutNum = outNum;
            //path = "E:/开发内容/1.txt";
            s_path = S_path;
            program.FileRead(path);
            program.MainCount();
            program.Output();
            program.FileSave();
          
            Res res = new Res(characters, lines);
            return res;
        }
        /// <summary>
        /// 输入参数
        /// </summary>
        void DataInput()
        {
            Console.WriteLine("请依次输入读取文档路径、词组长度、输出单词数量和存储路径：");
            path = Console.ReadLine();
            number = int.Parse(Console.ReadLine());
            outPutNum = int.Parse(Console.ReadLine());
            //path = "E:/开发内容/1.txt";
            s_path = Console.ReadLine();
            
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
            int wordJudge = 0;//单词长度标记
            int MutiWords = 0;//判断目前是否为词组记录状态的标志变量
            int MutiWordsJudge = 0;//词组长度标记
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
                                MutiWords++;
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
                                        MutiWords = 0;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MutiWordsJudge++;
                        //检测到空字符时结束当前判断周期，开启新周期
                        isWord = true;
                        MutiWords = 0;
                        //判断当前检测结果是否构成单词
                        if (wordJudge >= 4)
                        {
                            WordAdd(_word);
                            wordJudge = 0;
                            _word = "";
                        }
                        else
                        {
                            MutiWords++;
                            wordJudge = 0;
                            _word = "";
                            MutiWordsJudge = 0;
                        }
                        if ((MutiWords == 0) && (MutiWordsJudge == number))
                        {
                            MutiWordsAdd();
                            MutiWordsJudge = 0;
                        }
                    }
                }
                //在行末判断是否已构成单词
                if (wordJudge >= 4)
                {
                    WordAdd(_word);
                    wordJudge = 0;
                    _word = "";
                    if ((MutiWords == 0) && (MutiWordsJudge == number - 1))
                    {
                        MutiWordsAdd();
                        MutiWordsJudge = 0;
                    }
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
            foreach (var word in Words)
            {
                if (word.text == _word)
                {
                    word.num++;
                    flag++;
                    break;
                }
            }
            if (flag == 0)
            {
                Word aword = new Word(_word);
                Words.Add(aword);
            }
        }
        /// <summary>
        /// 往词组集合添加新词组
        /// </summary>
        void MutiWordsAdd()
        {
            int flag = 0;
            int i = 0;
            int j = Words.Count;
            string text = "";
            for (; i < number; i++)
            {
                text = text + Words[j - number + i].text + " ";
            }
            foreach(var word in MutipleWords)
            {
                if (word.text == text)
                {
                    word.num++;
                    flag++;
                    break;
                }
            }
            if (flag == 0)
            {
                Word mutiWord = new Word(text);
                MutipleWords.Add(mutiWord);
            }
        }
        /// <summary>
        /// 输出单词排序
        /// </summary>
        List<string> WordSort()
        {
            List<Word> HIVword = new List<Word>();
            int i = 0;
            for (; i < Words.Count - 1; i++)
            {
                int j = 0;
                for (; j < Words.Count - 1; j++)
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
            if (Words.Count < outPutNum)
            {
                for (; i < Words.Count; i++)
                {
                    HIVword.Add(Words[i]);
                }
            }
            else
            {
                for (; i < outPutNum; i++)
                {
                    HIVword.Add(Words[i]);
                }
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
            SaveStrs.Add("characters:" + program.characters);
            Console.WriteLine("words:" + program.Words.Count);
            SaveStrs.Add("words: " + program.Words.Count);
            Console.WriteLine("lines:" + program.lines);
            SaveStrs.Add("lines:" + program.lines);
            int i = 0;
            //输出高频单词
            if (Words.Count < outPutNum)
            {
                for (; i < Words.Count; i++)
                {
                    foreach (var iword in Words)
                    {
                        if (words[i] == iword.text)
                        {
                            Console.WriteLine(iword.text + " " + iword.num);
                            SaveStrs.Add(iword.text + " " + iword.num);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (; i < outPutNum; i++)
                {
                    foreach (var iword in program.Words)
                    {
                        if (words[i] == iword.text)
                        {
                            Console.WriteLine(iword.text + " " + iword.num);
                            SaveStrs.Add(iword.text + " " + iword.num);
                            break;
                        }
                    }
                }
            }
            //输出词组
            foreach(var word in MutipleWords)
            {
                Console.WriteLine(word.text+" "+word.num);
                SaveStrs.Add(word.text + " " + word.num);
            }
        }
        /// <summary>
        /// 储存内容
        /// </summary>
        /// <param name="s_path">储存路径</param>
        void FileSave()
        {
            System.IO.File.WriteAllLines(s_path, SaveStrs);
        }
    }
}
