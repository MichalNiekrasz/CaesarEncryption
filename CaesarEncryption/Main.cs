// Write the encrypt function (string, key1, key2), the result of which is the encrypted text with a cipher
// Caesar with modification - the vowels were coded with key1, and the consonants were coded with key2.
public static class Program{
    public static string CaesarEncryption(string text, int key1, int key2,bool overlap = true, bool polishVovels = true){
        
        string encryptedText="";   
        char encryptedChar='x';     
        foreach (char letter in text)
        {
            encryptedChar = isVovel(letter,polishVovels)?  (char)(letter+key1) : (char)(letter+key2);
            if(overlap&&encryptedChar<65) encryptedChar = (char)(90-(64-encryptedChar));
            else if(overlap&&encryptedChar>90) encryptedChar = (char)((encryptedChar-90)+64);      
            encryptedText += encryptedChar;     
        }
        return encryptedText;
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
    }
}