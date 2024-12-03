using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace XmlEncodingConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: XmlEncodingConverter <directory-path>");
                return;
            }

            string directoryPath = args[0];

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Error: Directory not found.");
                return;
            }

            try
            {
                // 取得目錄及其子目錄下的所有 XML 檔案
                string[] xmlFiles = Directory.GetFiles(directoryPath, "*.xml", SearchOption.AllDirectories);

                foreach (string filePath in xmlFiles)
                {
                    // 讀取原始 XML 檔案的前幾行以取得編碼訊息
                    string firstLine;
                    using (StreamReader reader = new StreamReader(filePath, Encoding.Default))
                    {
                        firstLine = reader.ReadLine();
                    }

                    Encoding originalEncoding = Encoding.Default;
                    if (firstLine != null && firstLine.StartsWith("<?xml"))
                    {
                        Match match = Regex.Match(firstLine, "encoding=\"(.*?)\"");
                        if (match.Success)
                        {
                            string encodingName = match.Groups[1].Value;
                            try
                            {
                                originalEncoding = Encoding.GetEncoding(encodingName);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine($"Warning: Unsupported encoding '{encodingName}' in file '{filePath}', using default encoding.");
                            }
                        }
                    }

                    // 讀取原始 XML 檔案內容
                    string xmlContent;
                    using (StreamReader reader = new StreamReader(filePath, originalEncoding))
                    {
                        xmlContent = reader.ReadToEnd();
                    }

                    // 修改 XML 聲明中的編碼為 UTF-8
                    if (xmlContent.StartsWith("<?xml"))
                    {
                        int encodingIndex = xmlContent.IndexOf("encoding=");
                        if (encodingIndex != -1)
                        {
                            int start = encodingIndex + 10;
                            int end = xmlContent.IndexOf('"', start);
                            xmlContent = xmlContent.Substring(0, start) + "UTF-8" + xmlContent.Substring(end);
                        }
                    }

                    // 將修改後的內容以 UTF-8 編碼保存
                    using (StreamWriter writer = new StreamWriter(filePath, false, new UTF8Encoding(false)))
                    {
                        writer.Write(xmlContent);
                    }

                    Console.WriteLine($"XML file '{filePath}' encoding has been successfully changed to UTF-8.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
