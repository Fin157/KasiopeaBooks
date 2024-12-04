namespace Books;

internal class Program
{
    static void Main(string[] args)
    {
        string inputPath = "D:\\Coding\\Seminary\\kasiopea24\\Books\\input.txt";
        string outputPath = "D:\\Coding\\Seminary\\kasiopea24\\Books\\output.txt";
        using StreamReader sr = new(inputPath);
        using StreamWriter sw = new(outputPath);

        int problemCount = int.Parse(sr.ReadLine());

        for (int i = 0; i < problemCount; i++)
            SolveProblem(sr, sw);
    }

    /// <summary>
    /// Loads data for itself, solves the problem based on the data, and pushes the output of the calculations into the output file.
    /// This function was made as a blackbox due to this assignment's unique possibility to process the input right as it is loaded.
    /// 
    /// The number of steps we have to do to make a coherent sequence out of all the books on a shelf is identical to the number
    /// of all the spaces between the leftmost and the rightmost book on that shelf. We just need to get that number of spaces.
    /// 
    /// We process the data in a left-to-right iterative manner. Firstly, we find the leftmost book. We then iterate further to find
    /// the next book to the right. This makes all spaces between the first and the second book actually effective and we can
    /// add the number of these spaces to the total space count. This process is repeated starting from the second book
    /// (the previous end point becomes the new starting point and a newfound book will become the new end point) until no end point is found
    /// at the end of the whole shelf. This is the reason why we couldn't simply count all the spaces on the shelf to begin with. The spaces after the
    /// rightmost book are, by definition from paragraph 2, irrelevant. We won't add these to the total space count at all.
    /// After we process the last position on the shelf, the number of total steps needed to get a coherent sequence of books
    /// will be stored in the total space count variable.
    /// </summary>
    static void SolveProblem(StreamReader sr, StreamWriter sw)
    {
        int shelfLength = int.Parse(sr.ReadLine()); // Determines how many characters this specific shelf consists of
        bool isSearching = true; // Makes the program not account for any spaces before the leftmost book is found (they are redundant)
        int currentSpaceCount = 0; // The number of spaces after the current last found book
        int totalSpaceCount = 0; // The total number of spaces before the current last found book

        for (int i = 0; i < shelfLength; i++)
        {
            char c = (char)sr.Read();

            if (c == 'K')
            {
                if (isSearching)
                    isSearching = false;

                totalSpaceCount += currentSpaceCount; // Add the buffered number of spaces to the total number of spaces
                currentSpaceCount = 0;                // (the current section of spaces got closed by a newfound book)
            }
            else if (c == '_')
            {
                if (isSearching) // Skip while the leftmost book on the shelf hasn't been found yet
                    continue;

                currentSpaceCount++;
            }
        }

        sr.ReadLine(); // Move to the next line of the input file

        // Write the result of the calculations into the output file
        sw.Write(totalSpaceCount);

        if (!sr.EndOfStream) // Make sure that there is no newline character at the end of the file
            sw.WriteLine();  // to stay faithful to the prescribed format of the output data.
    }
}
