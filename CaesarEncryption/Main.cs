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
    private static void Main(){
        // System.Console.WriteLine(CaesarEncryption("ABCD",-2,-4));
        // output = YXYZ
        System.Console.WriteLine(Encryption("to seria poradników, która wprowadzi każdego zainteresowanego w świat tych nowoczesnych mikrokontrolerów – od instalacji środowiska, przez stworzenie pierwszego projektu, aż do wykorzystywania popularnych peryferiów i modułów zewnętrznych. Kurs zawiera niezbędne minimum informacji teoretycznych – w zdecydowanej większości przypadków nacisk został położony na naukę przez obcowanie z danym zagadnieniem w praktyce. Kurs ten jest pełen praktycznych wskazówek i przykładów, które przydadzą się podczas późniejszej realizacji różnych projektów.aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",-4));
        System.Console.WriteLine();
        System.Console.WriteLine(Decryption("pk|oane]|lkn]`jeg?s(|gp?n]|slnks]`ve|g]Ę`ack|v]ejpanaoks]jack|s|÷se]p|pu_d|jksk_vaoju_d|iegnkgkjpnkhan?s|?|k`|ejop]h]_fe|÷nk`kseog](|lnvav|opsknvajea|leansovack|lnkfagpq(|]Ę|`k|sugknvuopus]je]|lklqh]nju_d|lanubane?s|e|ik`q??s|vasjupnvju_d*|Gqno|v]sean]|jeav^u`ja|iejeiqi|ejbkni]_fe|paknapu_vju_d|?|s|v`a_u`ks]jaf|seugovk÷_e|lnvul]`g?s|j]_eog|vkop]?|lk?kĘkju|j]|j]qgu|lnvav|k^_ks]jea|v|`]jui|v]c]`jeajeai|s|ln]gpu_a*|Gqno|paj|faop|la?aj|ln]gpu_vju_d|sog]v?sag|e|lnvug?]`?s(|gp?na|lnvu`]`v!|oeu|lk`_v]o|l?Ejeafovaf|na]hev]_fe|n?Ęju_d|lnkfagp?s*",'a'));
        
    }
}