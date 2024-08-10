

using System.Text;

public static class Program
{
    public static void Main(string[] args)
    {
        uint numOfFiles = 0;
        uint nameTablePosition = 0;
        uint fileValue = 0;
        uint fileContentOffset = 0;
        uint fileContentLength = 0;
        string readFileValue;

        // :3


        if (File.Exists("C:/Users/aivazquez/VisualStudioProjects/C# Projects/IgnitorStuff/010_FlameKnight.arc"))
        {
            using (var stream = File.Open("C:/Users/aivazquez/VisualStudioProjects/C# Projects/IgnitorStuff/010_FlameKnight.arc", FileMode.Open))
            {

                // Reading the file names

                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    reader.BaseStream.Position = 0xC;
                    numOfFiles = reader.ReadUInt32();
                    reader.BaseStream.Position = 0x18;
                    nameTablePosition = reader.ReadUInt32();

                    

                    for (int currentFile = 0; currentFile < numOfFiles; currentFile++)
                    {
                        reader.BaseStream.Position = 0x30 + numOfFiles * 4 + currentFile * 12;
                        fileContentOffset = reader.ReadUInt32();
 
                        fileContentLength = reader.ReadUInt32();


                        reader.BaseStream.Position = fileContentOffset;
                        byte[] fileBytes = new byte[fileContentLength];
                        reader.Read(fileBytes);
                        Console.WriteLine(fileContentLength);

                        FileStream extractedFile = File.Create("C:/Users/aivazquez/VisualStudioProjects/C# Projects/IgnitorStuff/ExtractedFiles/file.extension");
                          
                        extractedFile.Write(fileBytes);
                        extractedFile.Close();

                        fileContentOffset = fileContentOffset + 12;
                            
                    }

                   /* for (int fileNum = 0; fileNum < numOfFiles; fileNum++)
                    {
                        reader.BaseStream.Position = nameTablePosition + fileNum * 4;
                        fileValue = reader.ReadUInt32();
                        fileValue = nameTablePosition + fileValue;
                        reader.BaseStream.Position = fileValue;
                        readFileValue = ReadString(reader);
                        Console.WriteLine(readFileValue);


                    }*/
                       
                    // Extracting files from the arc file

                }
            }
        }   
    }

    // Code by NefariousTechSupport <3
    public static string ReadString(BinaryReader br)
    {
        List<byte> data = new List<byte>();
        while (true)
        {
            var newByte = br.ReadByte();
            if (newByte == 0) break;
            data.Add(newByte);
        }
        return System.Text.Encoding.UTF8.GetString(data.ToArray());
    }

}

