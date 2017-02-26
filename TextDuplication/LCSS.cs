using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDuplication
{
    class LCSS
    {
        private string strOne;
        private string strTwo;
        private int len1;
        private int len2;
        private int lcssLen;
        private int[,] lcsTable; // the LCSS table to be populated while comparing strings using dynamic programming
        private char[] comStr; // the variable to store the longest common subsequence between strOne and strTwo
        public int timeCount = 0;

        public LCSS(string st1, string st2)
        {
            strOne = st1;
            strTwo = st2;
            len1 = strOne.Length;
            len2 = strTwo.Length;
            lcsTable = new int[len1 + 1, len2 + 1];

            for (int i = 0; i < len1; i++)  // lcsTable gets initialized to 0 for all entries
                for (int j = 0; j < len2; j++)
                {
                    lcsTable[i, j] = 0;
                }
        }

        public int computeLCSS()
        {
            lcssLen = 0;
            for (int i = 1; i < len1 + 1; i++) // populates the lcsTable using dynamic programming technique
                for (int j = 1; j < len2 + 1; j++)
                {
                    if (strOne[i - 1] == strTwo[j - 1])
                    {
                        lcsTable[i, j] = lcsTable[i - 1, j - 1] + 1;

                    }
                    else
                        lcsTable[i, j] = (lcsTable[i, j - 1] > lcsTable[i - 1, j]) ? lcsTable[i, j - 1] : lcsTable[i - 1, j];

                    timeCount++;
                }
            lcssLen = lcsTable[len1, len2]; // returns the length of the longest common subsequence
            comStr = new char[lcssLen + 1];
            comStr[lcssLen] = '\0'; // variable to store the common subsequence between the two strings strOne and strTwo
            int k = 1;
            for (int i = len1; i > 0 && (k <= lcssLen);)
            {
                for (int j = len2; j > 0 && (k <= lcssLen);)
                {
                    if (lcsTable[i, j] != 0)
                    {
                        if (lcsTable[i, j] == lcsTable[i, j - 1])
                        {
                            j--;
                        }
                        else
                        {
                            if (lcsTable[i, j] == lcsTable[i - 1, j])
                            {
                                i--;
                            }
                            else
                            {
                                comStr[lcssLen - k] = strOne[i - 1];
                                k++;
                                i--;
                                j--;
                            }
                        }
                    }
                    else
                        break;
                }
            }
            return lcssLen;
        }

        public void printStrings()
        {

            for (int i = 0; i < comStr.Length; i++)
            {
                Console.Write(comStr[i]);
            }

        }
        /**
         * @return the length of the longest common subsequence between strOne and strTwo
         */
        public int getLcssLen()
        {
            return lcssLen;
        }

        public int getTimeCount()
        {
            return timeCount;
        }



    }
}
