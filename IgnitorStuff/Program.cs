

using System.Text;

public static class Program
{
    public static void Main(string[] args)
    {
        uint numOfFiles = 0;
        uint nameTablePosition = 0;
        uint fileValue = 0;
        uint fileContentOffset = 0;
        uint fileLength = 0;
        string readFileValue;

        // :3


        if (File.Exists("D:\\Skylander Mods\\SSA Files\\DATA\\files\\character\\010_FlameKnight.arc"))
        {
            using (var stream = File.Open("D:\\Skylander Mods\\SSA Files\\DATA\\files\\character\\010_FlameKnight.arc", FileMode.Open))
            {

                // Reading the file names

                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    reader.BaseStream.Position = 0xC;
                    numOfFiles = reader.ReadUInt32();
                    reader.BaseStream.Position = 0x18;
                    nameTablePosition = reader.ReadUInt32();

                    for (int fileNum = 0; fileNum < numOfFiles; fileNum++)
                    {
                        reader.BaseStream.Position = nameTablePosition + fileNum * 4;
                        fileValue = reader.ReadUInt32();
                        fileValue = nameTablePosition + fileValue;
                        reader.BaseStream.Position = fileValue;
                        readFileValue = ReadString(reader);
                        Console.WriteLine(readFileValue);


                        // Extracting files from the arc file

                        reader.BaseStream.Position = 0x30 + numOfFiles * 4;
                        fileContentOffset = reader.ReadUInt32();

                        for (int i = 0; i < numOfFiles; i++)
                        {

                            fileLength = fileLength + 12;
                            fileLength = reader.ReadUInt32();

                            FileStream extractedFile = File.Create("C:\\Users\\aivazquez\\VisualStudioProjects\\C# Projects\\IgnitorStuff\\ExtractedFiles\\file.extension");

                            Console.WriteLine(fileLength);
                            byte[] fileBytes = new byte[fileLength];
                            reader.BaseStream.Position = fileContentOffset;
                            reader.Read(fileBytes);

                            extractedFile.Write(fileBytes);
                            extractedFile.Close();
                            
                        }
                    }
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

