using System;
using IKVM;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDuplication
{
    class Program
    {

        static void Main(string[] args)
        {
            string text1;
            string text2;


            //sentence1 = new TikaOnDotNet.TextExtraction.TextExtractor().Extrac
            // sentence2 = Console.ReadLine();
            //float similarity = (testIfSimilar(sentence1, sentence2) * 100);
            //Console.WriteLine(similarity + "%");

            //text1 = readDocument("C:\\Users\\nasri\\Desktop\\identical1.docx");
            //text2 = readDocument("C:\\Users\\nasri\\Desktop\\identical2.docx");

            //text1 = readDocument("C:\\Users\\nasri\\Desktop\\oneDiffirentSentence1.docx");
            //text2 = readDocument("C:\\Users\\nasri\\Desktop\\oneDiffirentSentence2.docx");
       
            text1 = readDocument("C:\\Users\\nasri\\Desktop\\CompletelyDifferent1.docx");
            text2 = readDocument("C:\\Users\\nasri\\Desktop\\CompletelyDifferent2.docx");

            //text1 = readDocument("C:\\Users\\nasri\\Desktop\\OneEmpty1.docx");
            //text2 = readDocument("C:\\Users\\nasri\\Desktop\\OneEmpty2.docx");

            //text1 = readDocument("C:\\Users\\nasri\\Desktop\\test1.docx");
            //text2 = readDocument("C:\\Users\\nasri\\Desktop\\test2.docx");

            float similarity = testIfSimilar(text1.Replace("\r", String.Empty), text2.Replace("\r", String.Empty)) * 100;
            Console.WriteLine(similarity + "%");

            Console.ReadKey();




        }

        private static String readDocument(string filePath)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;
            object path = @"" + filePath;
            object readOnly = true;
            Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            string totaltext = "";
            for (int i = 0; i < docs.Paragraphs.Count; i++)
            {
                totaltext += docs.Paragraphs[i + 1].Range.Text.ToString();
            }
            docs.Close();
            word.Quit();
            return totaltext;

        }


        private static float testIfSimilar(string text1, string text2)
        {

            if (text1.ToLower() == text2.ToLower())
                return 1;

            float countOfDuplicatedSentences = 0;
            float countOfSimilarWords = 0;
            string[] sentencesInText1 = text1.Split("!?.".ToCharArray());
            string[] sentencesInText2 = text2.Split("!?.".ToCharArray());

            foreach (string sentence1 in sentencesInText1)
            {
                if (sentence1 == "")
                    continue;
                foreach (string sentence2 in sentencesInText2)
                {
                    if (sentence2 == "")
                        break;
                    if (sentence1.ToLower() == sentence2.ToLower())
                    {
                        countOfDuplicatedSentences++;
                    }
                    else
                    {
                        string[] wordsInSentence1 = sentence1.Split(' ');
                        string[] wordsInSentence2 = sentence2.Split(' ');
                        float wordsCount = 0;
                        foreach (string word1 in wordsInSentence1)
                        {
                            foreach (string word2 in wordsInSentence2)
                            {
                                if (word1.ToLower() == word2.ToLower())
                                {
                                    wordsCount++;

                                }
                            }
                        }
                        countOfSimilarWords += wordsCount / Math.Max(wordsInSentence1.Length, wordsInSentence2.Length);
                    }
                }
            }
            return (countOfDuplicatedSentences / Math.Max(sentencesInText1.Length, sentencesInText2.Length)) + (countOfSimilarWords / Math.Max(sentencesInText1.Length, sentencesInText2.Length));

        }
    }
}
