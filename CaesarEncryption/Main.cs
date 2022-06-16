// encrypt function (string, key1, key2), the result of which is the encrypted text with a cipher
// Caesar with modification - the vowels were coded with key1, and the consonants were coded with key2.
// decrypt function (string, mostcommon) - result is decrypted text from a chipher Caesar. Note, that text has to be
// long enough for the program to find the most common letter. Use only characters that are between 126 and 32 (including) from ASCII table.
public static class Caesar{
    const int MAX = 127; // maximum ASCII number (not including)
    const int MIN = 32; // minimum ASCII number (including)

    /// <param name="text">Text you want to encrypt</param>
    /// <param name="key1">type here how vovels will be coded</param>
    /// <param name="key2">type here how consonants will be coded. leave blank if you want this key to has same value as key1</param>
    /// <param name="overlap">overlaping the code, that in the returned string, chars will be only between 32 and 126(including) from ASCII table, recomended.</param>
    /// <param name="polishVovels">type here is your text in polish language to use polish vovels</param>
    /// <param name="neutralizeSpecialSigns">modifies the returned text in such a way that the special characters are neutralized (for ex. instead of '\' in string it will return '\\'). recomended only if you want to copy output and paste it in the code</param>
    public static string Encryption(string text, int key1,bool overlap = true, bool neutralizeSpecialSigns = false,int? key2 = null, bool polishVovels = false){

        key1 = key1 % (MAX-MIN);

        if(key2==null) key2=key1; 

        string encryptedText="";  
        int? encryptedCharInt;  

        foreach (char letter in text)
        {
            encryptedCharInt = isVovel(letter,polishVovels)?  letter+key1 : letter+key2;
            if(overlap&&encryptedCharInt<MIN) encryptedCharInt = MAX-(MIN-encryptedCharInt);
            else if(overlap&&encryptedCharInt>=MAX) encryptedCharInt = (encryptedCharInt-MAX)+MIN;      
            encryptedText += (char)encryptedCharInt;     
        }

        // this is needed to remove '\' and ' " ' activity in the string
        if(neutralizeSpecialSigns){
            for (int i = 0; i < encryptedText.Length; i++)
            {
                if(encryptedText[i]=='\\' || encryptedText[i]=='\"'){
                    encryptedText = encryptedText.Insert(i,"\\");
                    i++;
                }
            }
        }
        return encryptedText;
    }

    /// <summary>Decrypting the text. NOTE - to decrypt the text, the text can't be encrypted in different keys or too short.</summary>
    /// <param name="encryptedText">Text you want to decrypt</param>
    /// <param name="mostCommonLetter">Type here a most common letter in your language for ex. for english it's 'e' or ' '</param>
    /// <param name="overlap">true, if you want to decrypt an encrypted text which has been overlaped.</param>
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
        int maxNumberOfChars = 0;
        for (int i = 0; i < indexes.Length; i++)
        {
            if(indexes[i]>maxNumberOfChars){
                mostCommonIndex = i;
                maxNumberOfChars = indexes[i];
            }
        }

        // determine the shift
        int shift = mostCommonIndex-mostCommonLetter;
        shift = shift % (MAX-MIN);

        // decrypt each letter of the given text
        string decryptedText = "";
        int decryptedCharInt;
        foreach (var letter in encryptedText)
        {
            decryptedCharInt = letter - shift;
            if(overlap&&decryptedCharInt>=MAX) decryptedCharInt = (decryptedCharInt-MAX)+(MIN);
            if(overlap&&decryptedCharInt<MIN) decryptedCharInt = MAX-(MIN-decryptedCharInt);

            //if still
            if(overlap&&(decryptedCharInt<MIN||decryptedCharInt>MAX)) decryptedCharInt = 63; // 63 = '?'

            decryptedText += (char)decryptedCharInt;
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
    private static void Main(){
}
}