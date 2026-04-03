using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static FileSaver;
using static Unity.Burst.Intrinsics.X86.Avx;



static public  class FileSaver
{
	public enum E_special_characters
	{
		space = 27,
		endname = 31,
		endnum = 15
	}

	// Letters are stored with 5 bites, scores are stored as characters with 4 bits

	// Turns a HighScore List into a string to be saved to a file
	static public string SaveToString(List<S_HighScore> highScoreList)
	{ 
		BitArray bitArray = new BitArray(0);
		int bit = 0;

		foreach (S_HighScore highScore in highScoreList)
		{
			bitArray.Length = bitArray.Length + 5 * (highScore.HsName.Length + 1) + 4 * (highScore.HsScore.ToString().Length + highScore.HsCombo.ToString().Length + 2);
			bitArray = StoreName(bitArray, ref bit, highScore.HsName);
			bitArray = StoreNumbers(bitArray, ref bit, highScore.HsScore);
			bitArray = StoreNumbers(bitArray, ref bit, highScore.HsCombo);
		}

		string outstring = new string( BitArrayToCharArray(bitArray) );
		return (outstring);
	}

	static public List<S_HighScore> ReadToList(string str)
	{
		BitArray bitArray = StringToBitArray(str);
		List<S_HighScore> highScores = new List<S_HighScore>();

		int count = bitArray.Count;
		for (int bit = 0; bit < bitArray.Count;)
		{
			if (bitArray.Count - bit < sizeof(char) * 8)
			{
				break;
			}
			S_HighScore hs = new S_HighScore();
			hs.HsName = BinaryToString(bitArray, ref bit, (char)0b11111, 5, 'A');
			if (bit == -1)
			{
				break;
			}
			hs.HsScore = int.Parse(BinaryToString(bitArray, ref bit, (char)0b1111, 4, '0'));
			if (bit == -1)
			{
				break;
			}
			hs.HsCombo = int.Parse(BinaryToString(bitArray, ref bit, (char)0b1111, 4, '0'));
			if (bit == -1)
			{
				break;
			}
			highScores.Add(hs);
		}

		return (highScores);
	}

	// Stores a name in 5 bits, followed by the End Of Name character (11111)
	static BitArray StoreName(BitArray bitArray, ref int bit, string toStore)
	{
		foreach (char ch in toStore)
		{
			int numval = (ch == ' ' ? (int)E_special_characters.space : (ch - 'A'));
			for (int e = bit + 5; bit < e; bit++)
			{
				bitArray.Set(bit, (numval & 0b00000001) == 1);
				numval = (numval >> 1);
			}
		}
		for (int e = bit + 5; bit < e; bit++)
		{
			bitArray.Set(bit, true);
		}
		return (bitArray);
	}

	// Stores a number as characters in 4 bits, followed by the End of Number character (1111)
	static BitArray StoreNumbers(BitArray bitArray, ref int bit, int toStore)
	{
		string str = toStore.ToString();

		foreach (char ch in str)
		{
			int numval = ch - '0';
			for (int e = bit + 4; bit < e; bit++)
			{
				bitArray.Set(bit, (numval & 0b00000001) == 1);
				numval = (numval >> 1);
			}
		}
		for (int e = bit + 4; bit < e; bit++)
		{
			bitArray.Set(bit, true);
		}
		return (bitArray);
	}

	static char[] BitArrayToCharArray(BitArray bitArray)
	{
		char[] charArray = new char[(int)Mathf.Ceil(((float)bitArray.Count / (sizeof(char) * 8)))];
		int ch = 0;
		int charBit = sizeof(char) * 8 - 1;
		foreach (bool b in bitArray)
		{
			if (b == true)
			{
				charArray[ch] |= (char)(1 << charBit);
			}
			if (charBit == 0)
			{
				ch++;
				charBit = sizeof(char) * 8 - 1;
			}
			else
			{
				charBit--;
			}
		}
		return (charArray);
	}

	static BitArray StringToBitArray(string str)
	{
		BitArray bitArray = new BitArray(0);
		bitArray.Length = str.Length * sizeof(char) * 8;

		int i = 0;
		int bitPosition;
		foreach (char c in str)
		{
			bitPosition = sizeof(char) * 8 - 1;
			while (bitPosition >= 0)
			{
				bitArray.Set(i, (c & (char)(0b1 << bitPosition)) != 0);
				i++;
				bitPosition--;
			}
		}
		return (bitArray);
	}

	// reads information from BitArray into a string, starting from bit 'bit' (used as iterator), until it reads the string terminator.
	// it will read readAmount of bits for each character, and increases the resulting character by AsciiCharacterkey to form the characters
	// returns a string. if bit is out of range of bitArray, returns null and sets bit  to -1
	static string BinaryToString(BitArray bitArray, ref int bit, char stringTerminator, int readAmount, char AsciiCharacterKey)
	{
		string returnString = "";
		while (true)
		{
			char tempChar = (char)0;
			if (bit + 1 >= bitArray.Count)
			{
				bit = -1;
				return (null);
			}
			for (int shiftDistance = 0; shiftDistance < readAmount; shiftDistance++, bit++)
			{
				if (bitArray[bit] == true)
				{
					tempChar |= (char)(1 << shiftDistance);
				}
			}
			if (tempChar == stringTerminator)
			{
					return (returnString);
			}
			else
			{
				tempChar += AsciiCharacterKey;
				returnString += tempChar;
			}
		}
	}

	



}

