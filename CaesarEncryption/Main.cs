// encrypt function (string, key1, key2), the result of which is the encrypted text with a cipher
// Caesar with modification - the vowels were coded with key1, and the consonants were coded with key2.
// decrypt function (string, mostcommon) - result is decrypted text from a chipher Caesar. Note, that text has to be
// long enough for the program to find the most common letter. Use only characters that are between 127 and 32 (including) from ASCII table.
public static class Caesar{
    const int MAX = 127; // maximum ASCII number (including)
    const int MIN = 32; // minimum ASCII number (including)

    /// <param name="text">Text you want to encrypt</param>
    /// <param name="key1">type here how vovels will be coded</param>
    /// <param name="key2">type here how consonants will be coded. leave blank if you want this key to has same value as key1</param>
    /// <param name="overlap">overlaping the code, that in the returned string will be only big letters, not recomended</param>
    /// <param name="polishVovels">type here is your text in polish language</param>
    public static string Encryption(string text, int key1,bool overlap = true, int? key2 = null, bool polishVovels = false){
        
        if(key2==null) key2=key1; 

        string encryptedText="";   
        char encryptedChar='x';     

        foreach (char letter in text)
        {
            encryptedChar = isVovel(letter,polishVovels)?  (char)(letter+key1) : (char)(letter+key2);
            if(overlap&&encryptedChar<MIN) encryptedChar = (char)(MAX-((MIN-1)-encryptedChar));
            else if(overlap&&encryptedChar>MAX) encryptedChar = (char)((encryptedChar-MAX)+(MIN-1));      
            encryptedText += encryptedChar;     
        }
        return encryptedText;
    }
    /// <summary>Decrypting the text. NOTE - to decrypt the text, the text can't be overlaped, enrypted in different keys or too short.</summary>
    /// <param name="encryptedText">Text you want to decrypt</param>
    /// <param name="mostCommonLetter">Type here a most common letter in your language for ex. for english it's "e"</param>
    public static string Decryption(string encryptedText,char mostCommonLetter, bool overlap = true){

        //finds most common letter in given string
        int[] indexes = new int[MAX];
        foreach (char letter in encryptedText)
        {
            try{
                indexes[letter]++;
            }
            catch(IndexOutOfRangeException){
                continue;
            }

        }
        int mostCommonIndex = 0;
        for (int i = 0; i < indexes.Length; i++)
        {
            if(indexes[i]>mostCommonIndex) mostCommonIndex = i;
        }

        // determine the shift
        int shift = mostCommonLetter - mostCommonIndex;

        // decrypt each letter of the given text
        string decryptedText = "";
        char decryptedChar;
        foreach (var letter in encryptedText)
        {
            decryptedChar = (char)(letter + shift);
            if(overlap&&decryptedChar>MAX) decryptedChar = (char)((decryptedChar-MAX)+(MIN-1));
            if(overlap&&decryptedChar<MIN) decryptedChar = (char)(MAX-((MIN-1)-decryptedChar));

            //if still
            if(overlap&&(decryptedChar<MIN||decryptedChar>MAX)) decryptedChar = (char)63; // 63 = '?'

            decryptedText += decryptedChar;
        }
        return decryptedText;
    }
    private static bool isVovel(char letter, bool polishVovels){

        string vowels = "aeyuio";
        if(!polishVovels) vowels = "aeuio";
        
        foreach (var item in vowels)
        {
            if(letter.ToString().ToUpper()==item.ToString().ToUpper())return true;
        }
        return false;
    }
}