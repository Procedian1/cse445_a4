using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Net;


/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://procedian1.github.io/cse445_a4/Hotels.xml";
		public static string xmlErrorURL = "https://procedian1.github.io/cse445_a4/HotelsErrors.xml";
		public static string xsdURL = "https://procedian1.github.io/cse445_a4/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
			try
			{
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.Schemas.Add(null, XmlReader.Create(WebRequest.Create(xsdUrl).GetResponse().GetResponseStream()));
				settings.ValidationType = ValidationType.Schema;

				string errorMsg = "No Error";

				settings.ValidationEventHandler += (sender, args) =>
				{
					errorMsg = args.Message;
				};

				using (XmlReader reader = XmlReader.Create(WebRequest.Create(xmlUrl).GetResponse().GetResponseStream(), settings))
				{
					while (reader.Read()) { }
				}

				return errorMsg;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
        }

        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                string xmlContent = string.Empty;

                using (WebClient client = new WebClient())
                {
                    xmlContent = client.DownloadString(xmlUrl);
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent);

                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);

                return jsonText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}
