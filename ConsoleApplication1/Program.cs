using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            readConfig();
            //ReadLine();
        }

        private static string[] readConfig() {
            //return new string[] { "Date", "Time", "PID", "#", "Level", "Thread", "Message" };
            List<string> titles = new List<string>();

            Dictionary<string, string> dicNodes = new Dictionary<string, string>();

            XmlDocument doc = new XmlDocument();
            doc.Load("c:\\configColumnizer.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                titles.Add(node.Name);
                dicNodes.Add(node.Name,node.Attributes.Count > 0 ? node.Attributes[0].Value : string.Empty);
                if (node.HasChildNodes)
                {
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        titles.Add(n.Name);
                        dicNodes.Add(node.Name, node.Attributes.Count > 0 ? node.Attributes[0].Value : string.Empty);
                    }
                }
            }
            return titles.ToArray();
        }


        private static void ReadLine()
        {
            try
            {
                using (FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + @"\ProvaFede.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        String line;

                        while (!streamReader.EndOfStream)  // <= Check for end of file
                        {
                            line = streamReader.ReadLine(); // <=Get a single line
                            String[] res = Regex.Split(line, "\\|");
                            string[] cols = new string[10] { "", "", "", "", "", "", "", "", "", "" };

                            //if (res.Length == 8)
                            //{
                            //    String[] dateTime = Regex.Split(res[0], "\\s");
                            //    cols[0] = dateTime[0];
                            //    cols[1] = dateTime[1];
                            //    cols[2] = res[1];
                            //    cols[3] = res[2];
                            //    cols[4] = res[3];
                            //    cols[5] = res[4];
                            //    String[] splitted = Regex.Split(res[5], "\\,");
                            //    cols[6] = splitted[0];
                            //    cols[7] = splitted[1];
                            //    cols[8] = res[6];
                            //    cols[9] = res[7];
                            //}
                            if (res.Length == 8)
                            {
                                String[] dateTime = Regex.Split(res[0], "\\s");
                                cols[0] = dateTime[0];
                                cols[1] = dateTime[1];
                                cols[2] = Regex.Split(res[1], "\\|")[0]; 
                                cols[3] = string.Join("|", res[2], res[3], res[4], res[5], res[6], res[7]);
                                //cols[4] = res[3];
                                //cols[5] = res[4];
                                //String[] splitted = Regex.Split(res[5], "\\,");
                                //cols[6] = splitted[0];
                                //cols[7] = splitted[1];
                                //cols[8] = res[6];
                                //cols[9] = res[7];
                            }


                            if (line.Contains("Button1"))  // <= Check for condition ; line contains 'Button1'
                            {
                                //txt.Text += line + "\n"; // <== Append text  with a newline 
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}
