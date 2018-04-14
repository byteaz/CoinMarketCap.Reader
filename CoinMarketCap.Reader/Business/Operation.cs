using HtmlAgilityPack;
using Core.Data;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Data;

namespace Core.Business
{

    public class Operation
    {
        public static DBUtil DB = new DBUtil();

  


        public static void ConsoleCommands()
        {
            // Commands
            command:

            string command = Console.ReadLine();


            switch (command)
            {
                case "cls" : Console.Clear();
                    break;
            }

            if (command != "exit")
            {
                goto command;
            }
        }

    

        public static string ClearDublicateSpaces(string input)
        {
            string temp = input;

            temp = temp.Replace("  ", " ");
            temp = temp.Replace("  ", " ");
            temp = temp.Replace("  ", " ");
            temp = temp.Replace("  ", " ");
            temp = temp.Replace("  ", " ");
            temp = temp.Replace("  ", " ");
            temp = temp.Replace("  ", " ");
            temp = temp.Replace("  ", " ");
            temp = temp.Replace("  ", " ");
            temp = temp.Replace("  ", " ");

            return temp;
        }
        

        public static string ReplaceDecimalSeperator(string data)
        {
            return data.Replace(".", ",").Trim();
        }

        public static string ClearBlanks(string data)
        {
            string temp_data = data;

            temp_data = temp_data.Replace(" ", String.Empty);

            return temp_data;
        }

        public static string CorrectPrice(string data)
        {
            string temp_data = data;

            temp_data = temp_data.Replace(",", String.Empty);
            temp_data = temp_data.Replace(".", String.Empty);

            return temp_data;
        }

        public static string ReplaceNonNumberic(string data,string ch)
        {
            return Regex.Replace(data, "[^0-9]", ch);
        }

   

        public static string ClearSpesificChars(string input)
        {
            string temp_str = input;

            temp_str = temp_str.Replace(",", String.Empty);
            temp_str = temp_str.Replace(".", String.Empty);
            temp_str = temp_str.Replace("/", String.Empty);
            temp_str = temp_str.Replace(@"\", String.Empty);
            temp_str = temp_str.Replace("'", String.Empty);
            temp_str = temp_str.Replace(":", String.Empty);

            return temp_str;
        }


        public static decimal ConvertToDecimal(string input)
        {
            try
            {
                return Convert.ToDecimal(input);
            }
            catch
            {
                return 0;
            }
        }

        public static string ExtractTagData(string _data, string _tag_region, string _left_tag, string _right_tag)
        {
            string data = String.Empty;
            string tag_region = String.Empty;
            string left_tag = String.Empty;
            string right_tag = String.Empty;

            data = _data;
            tag_region = _tag_region.Replace(@"\n", "\n");
            left_tag = _left_tag.Replace(@"\n", "\n");
            right_tag = _right_tag.Replace(@"\n", "\n");

            try
            {
                int index_of_tag_region = data.IndexOf(tag_region);

                if (index_of_tag_region == -1)
                {
                    return String.Empty;
                }

                index_of_tag_region = index_of_tag_region + tag_region.Length;
                data = data.Substring(index_of_tag_region, data.Length - index_of_tag_region);

                if (left_tag.Length != 0)
                {
                    int index_of_left_tag = data.IndexOf(left_tag);
                    data = data.Substring(index_of_left_tag + left_tag.Length, data.Length - index_of_left_tag - left_tag.Length);
                }

                try
                {
                    return data.Substring(0, data.IndexOf(right_tag));
                }
                catch
                {
                    return String.Empty;
                }
            }
            catch
            {
                return String.Empty;
            }
        }

        public static DataTable ConvertStringToDataTable(string data)
        {
            DataTable dataTable = new DataTable();
            bool columnsAdded = false;
            char[] tab = { '[', 't', 'a', 'b', ']' };
            char[] line = { '[', 'l', 'i', 'n', 'e', ']' };

            int column = 0;

            for(int i=0; i<=7; i++)
            {
                DataColumn dataColumn = new DataColumn(i.ToString());
                dataTable.Columns.Add(dataColumn);
            }

            foreach (string row in data.Split(line))
            {
                DataRow dataRow = dataTable.NewRow();


                 column = 0;
                foreach (string cell in row.Split(tab))
                {
                    dataRow[column.ToString()] = cell;

                    column++;
                }
                columnsAdded = true;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        public static string ReadFileContent(string file)
        {
            string content = String.Empty;

            try
            {
                using (StreamReader streamReader = new StreamReader(file))
                {
                    content = streamReader.ReadToEnd();
                }
            }
            catch
            {

            }

            return content;
        }


        public static string DownloadURL(string url)
        {
            String guid = Guid.NewGuid().ToString();
            string file = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Data\\" + guid + ".txt";

            try
            {
                WebClient Client = new WebClient();
                Client.DownloadFile(url, file);
                return file;
            }
            catch 
            {
                try
                {
                    HtmlWeb hw = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc = hw.Load(url);
                    doc.Save(file);
                    return file;
                }
                catch
                {
                    return string.Empty;
                }

            }
        }

        public static string GetURLContent(string url)
        {
            string content = String.Empty;
            try
            {
                WebClient Client = new WebClient();
                content = Client.DownloadString(url);
            }
            catch
            {
           
            }

            return content;
        }


        public static string ExtractTextFromHTML(string data)
        {
            string temp_data = String.Empty;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(data);

            try
            {
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
                {
                    temp_data = temp_data + " " + node.InnerText;
                }
            }
            catch
            {
                temp_data = String.Empty;
            }

            temp_data = temp_data.Replace("&nbsp;", String.Empty);

            return temp_data;
        }

        public static bool DeleteFile(string file_name)
        {
            if (File.Exists(file_name))
            {
                try
                {
                    File.Delete(file_name);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public static bool CheckForInternetConnection(string host)
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead(host))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
