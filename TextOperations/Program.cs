using System.Diagnostics;

try
{
    Console.WriteLine("Program Started...");
    Stopwatch stopwatch = Stopwatch.StartNew();

    string filePath = "book.txt";

    string fileContent = File.ReadAllText(filePath);


    List<string> words = CountWords(fileContent);
    double averageWordLength = CalculateAverageWordLength(fileContent);

    Console.WriteLine($"The book contains {words[0]} valid words. \nThe shortest word is {words[1]}. \nThe longest word is {words[2]}");
    Console.WriteLine($"The average word length is: {averageWordLength:F2}");
    var mostCommonWords = GetMostCommonWords(fileContent, 5);
    var leastCommonWords = GetLeastCommonWords(fileContent, 5);

    Console.WriteLine("Five Most Common Words:");
    foreach (var word in mostCommonWords)
    {
        Console.WriteLine($"{word.Key}: {word.Value} occurrences");
    }

    Console.WriteLine("\nFive Least Common Words:");
    foreach (var word in leastCommonWords)
    {
        Console.WriteLine($"{word.Key}: {word.Value} occurrences");
    }

    stopwatch.Stop();

    Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}


static List<string> CountWords(string content)
{
    List<string> words = new List<string>();

    char[] punctuation = { '.', ',', ';', ':', '!', '?', '-', '_', '(', ')', '[', ']', '{', '}', '"', '\'', '`' };

    string[] potentialWords = content.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


    int wordCount = 0;
    string shortestWord = string.Empty;
    string longestWord = string.Empty;

    foreach (string word in potentialWords)
    {

        string cleanedWord = RemovePunctuation(word, punctuation);


        if (cleanedWord.Length >= 3)
        {
            wordCount++;

            if (string.IsNullOrEmpty(shortestWord))
            {
                shortestWord = cleanedWord;
            }
            else if(word.Length < shortestWord.Length)
            {
                shortestWord = cleanedWord;
            }


            if (string.IsNullOrEmpty(longestWord))
            {
                longestWord = cleanedWord;
            }
            else if (word.Length > longestWord.Length)
            {
                longestWord = cleanedWord;
            }
        }
    }

    words.Add(wordCount.ToString());
    words.Add(shortestWord);
    words.Add(longestWord);

    return words;
}

static double CalculateAverageWordLength(string content)
{
    
    char[] punctuation = { '.', ',', ';', ':', '!', '?', '-', '_', '(', ')', '[', ']', '{', '}', '"', '\'', '`' };

   
    string[] potentialWords = content.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

    int totalLength = 0;
    int wordCount = 0;

    foreach (string word in potentialWords)
    {
     
        string cleanedWord = RemovePunctuation(word, punctuation);

    
        if (cleanedWord.Length < 3)
        {
            continue;
        }

        
        totalLength += cleanedWord.Length;
        wordCount++;
    }

  
    return wordCount > 0 ? (double)totalLength / wordCount : 0.0;
}

static Dictionary<string, int> CountWordFrequencies(string content)
{
   
    char[] punctuation = { '.', ',', ';', ':', '!', '?', '-', '_', '(', ')', '[', ']', '{', '}', '"', '\'', '`' };

   
    string[] potentialWords = content.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

    var wordFrequencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

    foreach (string word in potentialWords)
    {

        string cleanedWord = RemovePunctuation(word, punctuation);


        if (cleanedWord.Length < 3)
        {
            continue;
        }

        if (wordFrequencies.ContainsKey(cleanedWord))
        {
            wordFrequencies[cleanedWord]++;
        }
        else
        {
            wordFrequencies[cleanedWord] = 1;
        }
    }

    return wordFrequencies;
}

static List<KeyValuePair<string, int>> GetMostCommonWords(string content, int count)
{
    var wordFrequencies = CountWordFrequencies(content);

    var mostCommonWords = new List<KeyValuePair<string, int>>(wordFrequencies);
    mostCommonWords.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
    return mostCommonWords.GetRange(0, Math.Min(count, mostCommonWords.Count));
}

static List<KeyValuePair<string, int>> GetLeastCommonWords(string content, int count)
{
    var wordFrequencies = CountWordFrequencies(content);

    var leastCommonWords = new List<KeyValuePair<string, int>>(wordFrequencies);
    leastCommonWords.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
    return leastCommonWords.GetRange(0, Math.Min(count, leastCommonWords.Count));
}

static string RemovePunctuation(string word, char[] punctuation)
{
 
    string cleanedWord = "";

    foreach (char c in word)
    {
        if (char.IsLetterOrDigit(c)) 
        {
            cleanedWord += c;
        }
    }

    return cleanedWord;
}