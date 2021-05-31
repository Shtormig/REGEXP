using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace REGEXP
{
    public class Part : IEquatable<Part>
    {
        public string img { get; set; }
        public int PartId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Part objAsPart = obj as Part;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return PartId;
        }
        public bool Equals(Part other)
        {
            if (other == null) return false;
            return (this.PartId.Equals(other.PartId));
        }
    }
        class Razbor
    {
        private readonly string s;
        public Razbor(string str)
        {
            s = str;
        }
        public void Raz(string input)
        {
            string html = input;
            string htmlCode=null;
            string media = null;
            int i = 0;
            string localFilename = @"C:\Temp\";
            List<Part> parts = new List<Part>();
            try
            {
                using (WebClient client = new WebClient())
                {
                    htmlCode = client.DownloadString(input);
                }
            }
            catch {
                Console.WriteLine("Неудолось скачать страницу. Проверьте введенный адрес");
            }
            Regex regex = new Regex("<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(htmlCode);
            if (matches.Count > 0)
            {
                Console.WriteLine("Нашли " + matches.Count);
                foreach (Match match in matches)
                   parts.Add(new Part() { img = match.Groups[1].Value, PartId = i++ });
            }
            else
            {
                Console.WriteLine("Совпадений не найдено");
            }
             foreach (Part aPart in parts)
            {
                media = aPart.img;
                using (WebClient client = new WebClient())
                {
                    try
                    {
                      client.DownloadFile(Regex.Match(html, "(http|https://.+?)/", RegexOptions.IgnoreCase).Groups[1].Value + media, localFilename + Regex.Match(media, "[a-z0-9._-]+$", RegexOptions.IgnoreCase));
                        Console.WriteLine("По ссылке " + Regex.Match(html, "(http|https://.+?)/", RegexOptions.IgnoreCase).Groups[1].Value + media + " скачали в деррикторию " + localFilename + Regex.Match(media, "[a-z0-9._-]+$", RegexOptions.IgnoreCase));
                    }
                    catch
                    {
                        Console.WriteLine("ОШИБКА По ссылке " + Regex.Match(html, "(http|https://.+?)/", RegexOptions.IgnoreCase).Groups[1].Value + media + " неудолось скачать в деррикторию " + localFilename + Regex.Match(media, "[a-z0-9._-]+$", RegexOptions.IgnoreCase));
                    }
                }

            }
            
        }
    }
}
