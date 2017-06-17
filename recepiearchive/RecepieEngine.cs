using RecepieScraper.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Collections.Concurrent;

namespace RecepieScraper
{
    public class RecepieEngine
    {
        private string _defaultFilePath;
        private ConcurrentDictionary<string, Recepie> _recepies = new ConcurrentDictionary<string, Recepie>();
        private int _recepieScrapeCount = 0;
        private int _RecepiePages = 0;
        private Timer periodicRecepieScrape;
        private ConcurrentDictionary<DateTime, string> logging = new ConcurrentDictionary<DateTime, string>();
        private bool _discoveryPhaseStarted = false;
        private bool _waitingToFinish = false;


        public RecepieEngine()
        {
            LogMessage("Starting Recepie Engine");

            periodicRecepieScrape = new Timer();
            periodicRecepieScrape.Interval = 10 * 60 * 1000; //ten minutes            
            periodicRecepieScrape.Elapsed += PeriodicRecepieScrape_Elapsed;
            Task.Run(() => PeriodicRecepieScrape_Elapsed(null, null));
        }

        public Dictionary<string, Recepie> Recepies
        {
            get
            {
                if (_recepies != null)
                {
                    return _recepies.Where(w => w.Value.Ingredients!=null && w.Value.Ingredients.Count > 0).ToDictionary(k => k.Key, v => v.Value);
                }
                else
                {
                    return null;
                }
            }
        }

        public void LoadRecepies(string filepath)
        {
            string serializedRecepies = null;

            if (File.Exists(filepath))
            {
                StreamReader sr = new StreamReader(filepath);
                serializedRecepies = sr.ReadToEnd();
                sr.Close();
            }

            _recepies = new ConcurrentDictionary<string, Recepie>();

            if (!String.IsNullOrWhiteSpace(serializedRecepies))
            {
                _recepies = new ConcurrentDictionary<string, Recepie>(JsonConvert.DeserializeObject<List<Recepie>>(serializedRecepies).ToDictionary(k => k.RecepieUrl, v => v));                
            }
            _defaultFilePath = filepath;
        }

        public void SetStarRating(string recepieUrl, int starRating)
        {
            if (starRating > 0 && starRating < 6)
                if (_recepies.ContainsKey(recepieUrl))
                {
                    LogMessage($"Giving '{_recepies[recepieUrl].Title}' a {GenerateStarString(starRating)} rating");
                    _recepies[recepieUrl].Rating = starRating;
                    SaveRecepies(_defaultFilePath);
                }
        }

        public void SetIgnoreProductCompletely(string recepieUrl, bool ignoreProduct)
        {
            if (_recepies.ContainsKey(recepieUrl))
            {
                _recepies[recepieUrl].IgnoreProductCompletely = ignoreProduct;
                SaveRecepies(_defaultFilePath);
            }
        }

        public void RescrapeRecepieSite()
        {
            Task.Run(() => RunRecepieDiscoveryPhase());
        }

        public string GenerateStarString(int starts)
        {
            StringBuilder sb = new StringBuilder();

            for(int i=0;i<starts;i++)
            {
                sb.Append("★");
            }

            return sb.ToString();
        }

        #region private methods

        private void SaveRecepies(string filepath)
        {
            LogMessage("Saving Recepies");
            if (!String.IsNullOrWhiteSpace(filepath))
            {
                StreamWriter sw = new StreamWriter(filepath);
                sw.Write(JsonConvert.SerializeObject(_recepies.Values.ToList(), Formatting.Indented));
                sw.Close();
            }
            else
            {
                LogMessage("CANNOT SAVE, FILEPATH NOT SET");
            }
        }

        private void RunRecepieDiscoveryPhase()
        {
            int currentRecepieCount = _recepies.Count;
            int newRecepieCount = 0;
            LogMessage("Starting recepie discovery phase");
            _discoveryPhaseStarted = true;

            WebClient client = new WebClient();
            string downloadString = client.DownloadString("https://www.gousto.co.uk/cookbook");

            downloadString = downloadString.Split(new string[] { "class=\"pagination" }, StringSplitOptions.None)[1];
            downloadString = downloadString.Split(new string[] { "</li><li><a href=\"" }, StringSplitOptions.None)[downloadString.Split(new string[] { "</li><li><a href=\"" }, StringSplitOptions.None).Length - 2];
            downloadString = downloadString.Split(new string[] { "\">" }, StringSplitOptions.None)[0];
            downloadString = downloadString.Replace("https://www.gousto.co.uk/cookbook?page=", "");

            _RecepiePages = int.Parse(downloadString);           

            for (int i = 1; i <= _RecepiePages; i++)
            {
                LogMessage($"Scanning page {i} of {_RecepiePages}");
                string scrapingPage = client.DownloadString("https://www.gousto.co.uk/cookbook?page=" + i.ToString());
                string[] rawRecepies = scrapingPage.Split(new string[] { "itemgridchild" }, StringSplitOptions.None);
                for (int ii = 1; ii < rawRecepies.Length; ii++)
                {
                    Recepie recepie = new Recepie();
                    recepie.Title = WebUtility.HtmlDecode(rawRecepies[ii].Split(new string[] { "<h3>", "</h3>" }, StringSplitOptions.None)[1]);
                    recepie.Description = WebUtility.HtmlDecode(rawRecepies[ii].Split(new string[] { "cookbook-recipecard-description\">" }, StringSplitOptions.None)[1].Split(new string[] { "</div>" }, StringSplitOptions.None)[0]).Replace("\n\t", "");
                    recepie.RecepieUrl = WebUtility.HtmlDecode(rawRecepies[ii].Split(new string[] { "readmore\"><span><a href=\"" }, StringSplitOptions.None)[1].Split(new string[] { "\">Read more" }, StringSplitOptions.None)[0]);

                    try
                    {
                        recepie.Thubmnail = rawRecepies[ii].Split(new string[] { "source data-srcset=\"" }, StringSplitOptions.None)[1].Split(new string[] { "\" media=\"" }, StringSplitOptions.None)[0];
                        WebClient wc = new WebClient();
                        byte[] bytes = wc.DownloadData(recepie.Thubmnail);

                        recepie.ThumbnailImageBytes = bytes;
                    }
                    catch
                    {
                        recepie.Thubmnail = null;
                    }

                    if (!_recepies.Any(a => a.Key == recepie.RecepieUrl))
                    {
                        recepie.DateAdded = DateTime.UtcNow;
                        _recepies.TryAdd(recepie.RecepieUrl, recepie);
                        newRecepieCount++;
                    }
                    else if(_recepies[recepie.RecepieUrl].DateAdded<DateTime.UtcNow.AddDays(-5))
                    {
                        if (_recepies[recepie.RecepieUrl].Description != recepie.Description)
                            _recepies[recepie.RecepieUrl].Description = recepie.Description;
                        if (_recepies[recepie.RecepieUrl].Title != recepie.Title)
                            _recepies[recepie.RecepieUrl].Title = recepie.Title;
                        if (_recepies[recepie.RecepieUrl].Thubmnail != recepie.Thubmnail)
                            _recepies[recepie.RecepieUrl].Thubmnail = recepie.Thubmnail;
                        if (_recepies[recepie.RecepieUrl].ThumbnailImageBytes != recepie.ThumbnailImageBytes)
                            _recepies[recepie.RecepieUrl].ThumbnailImageBytes = recepie.ThumbnailImageBytes;
                    }
                }
            }

            LogMessage($"{newRecepieCount} new recepies have been found and are being added");

            SaveRecepies(_defaultFilePath);

            _discoveryPhaseStarted = false;

            PeriodicRecepieScrape_Elapsed(null, null);
        }

        private void RunRecepieProductPhase()
        {
            Dictionary<string, Recepie> fullRecepies = new Dictionary<string, Recepie>();

            WebClient client = new WebClient();

            foreach (Recepie recepie in _recepies.Values)
            {
                if (recepie.LastUpdated < DateTime.UtcNow.AddDays(-5) || recepie.Calories == 0)
                {
                    Recepie fullRecepie = ConvertWebStringToRecepie(client.DownloadString(recepie.RecepieUrl), recepie);
                    fullRecepies.Add(fullRecepie.RecepieUrl, fullRecepie);
                }
                else
                {
                    fullRecepies.Add(recepie.RecepieUrl, recepie);
                }

            }
            _recepies = new ConcurrentDictionary<string, Recepie>(fullRecepies);

            SaveRecepies(_defaultFilePath);
        }

        private Recepie ConvertWebStringToRecepie(string webstring, Recepie recepie = null)
        {
            if (recepie == null)
                recepie = new Recepie();
            if (recepie.Ingredients == null)
                recepie.Ingredients = new Dictionary<string, Ingredient>();

            recepie.LastUpdated = DateTimeOffset.UtcNow;
            recepie.Calories = int.Parse(webstring.Split(new string[] { " nutritional-info-table" }, StringSplitOptions.None)[1].Split(new string[] { " kcal" }, StringSplitOptions.None)[1].Split(new string[] { "<br/>" }, StringSplitOptions.None)[1]);
            recepie.Carbs = decimal.Parse(webstring.Split(new string[] { " nutritional-info-table" }, StringSplitOptions.None)[1].Split(new string[] { "Carbohydrate<br>" }, StringSplitOptions.None)[1].Split(new string[] { " g<br" }, StringSplitOptions.None)[0].Split(new string[] { "<td>" }, StringSplitOptions.None)[1]);
            recepie.PrepTime = int.Parse(webstring.Split(new string[] { "Prep Time</p>" }, StringSplitOptions.None)[1]
                .Split(new string[] { "<p class=\"recipehighlight-box-value\">" }, StringSplitOptions.None)[1]
                .Split(new string[] { " min</p>" }, StringSplitOptions.None)[0]);

            recepie.Cuisine = webstring.Split(new string[] { "Cuisine</p>" }, StringSplitOptions.None)[1]
                .Split(new string[] { "<p class=\"recipehighlight-box-value\">" }, StringSplitOptions.None)[1]
                .Split(new string[] { "</p>" }, StringSplitOptions.None)[0];

            string[] ingredients = webstring.Split(new string[] { "indivrecipe-ingredients-text\">" }, StringSplitOptions.None);

            for (int i = 1; i < ingredients.Length; i++)
            {
                Ingredient ingredient = new Ingredient();

                int guineapig = 0;

                if (ingredients[i].Substring(0, 30).ToLower().Contains("tbsp"))
                {
                    ingredient.MeasurmentType = QuantityType.Tbsp;
                    try
                    {
                        ingredient.Quantity = float.Parse(ingredients[i].Split(new string[] { " tbsp ", "tbsp " }, StringSplitOptions.None)[0]);
                    }
                    catch
                    {
                        ingredient.Quantity = (float)FractionToDouble(ingredients[i].Split(new string[] { " tbsp ", "tbsp " }, StringSplitOptions.None)[0]);
                    }
                    ingredient.Name = ingredients[i].Split(new string[] { " tbsp ", "tbsp " }, StringSplitOptions.None)[1]
                        .Split(new string[] { "</figcaption>" }, StringSplitOptions.None)[0];
                }
                else if (ingredients[i].Substring(0, 30).ToLower().Contains("tsp"))
                {
                    ingredient.MeasurmentType = QuantityType.Tsp;
                    try
                    {
                        ingredient.Quantity = float.Parse(ingredients[i].Split(new string[] { " tsp ", "tsp " }, StringSplitOptions.None)[0]);
                    }
                    catch
                    {
                        ingredient.Quantity = (float)FractionToDouble(ingredients[i].Split(new string[] { " tsp ", "tsp " }, StringSplitOptions.None)[0]);
                    }

                    ingredient.Name = ingredients[i].Split(new string[] { " tsp ", "tsp " }, StringSplitOptions.None)[1]
                        .Split(new string[] { "</figcaption>" }, StringSplitOptions.None)[0];
                }
                else if (Regex.IsMatch(ingredients[i].Substring(0, 30), @"^\d{1,}[m][l]"))
                {
                    ingredient.MeasurmentType = QuantityType.ml;
                    ingredient.Quantity = float.Parse(ingredients[i].Split(new string[] { "ml " }, StringSplitOptions.None)[0]);
                    ingredient.Name = ingredients[i].Split(new string[] { "ml " }, StringSplitOptions.None)[1]
                        .Split(new string[] { "</figcaption>" }, StringSplitOptions.None)[0];
                }
                else if (Regex.IsMatch(ingredients[i].Substring(0, 30), @"^\d{1,}[g]"))
                {
                    ingredient.MeasurmentType = QuantityType.g;
                    ingredient.Quantity = float.Parse(ingredients[i].Split(new string[] { "g " }, StringSplitOptions.None)[0]);
                    ingredient.Name = ingredients[i].Split(new string[] { "g " }, StringSplitOptions.None)[1]
                        .Split(new string[] { "</figcaption>" }, StringSplitOptions.None)[0];
                }
                else if (Regex.IsMatch(ingredients[i].Substring(0, 30), @"\d{1,}[g]"))
                {
                    ingredient.MeasurmentType = QuantityType.g;

                    Match m = Regex.Match(ingredients[i].Substring(0, 250), @"\d{1,}[g]", RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        ingredient.Quantity = float.Parse(m.Groups[0].Value.Replace("g", ""));
                        ingredient.Name = ingredients[i].Split(new string[] { "</figcaption>" }, StringSplitOptions.None)[0].Replace(m.Groups[0].Value, "");
                        ingredient.Name = ingredient.Name.Replace("()", "");
                    }
                }
                else if (Regex.IsMatch(ingredients[i].Substring(0, 30), @"\d{1,}[m][l]"))
                {
                    ingredient.MeasurmentType = QuantityType.ml;

                    Match m = Regex.Match(ingredients[i].Substring(0, 250), @"\d{1,}[m][l]", RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        ingredient.Quantity = float.Parse(m.Groups[0].Value.Replace("ml", ""));
                        ingredient.Name = ingredients[i].Split(new string[] { "</figcaption>" }, StringSplitOptions.None)[0].Replace(m.Groups[0].Value, "");
                        ingredient.Name = ingredient.Name.Replace("()", "");
                    }
                }
                else if (Regex.IsMatch(ingredients[i].Substring(0, 30), @"^\d{1,}\/\d{1,}"))
                {
                    ingredient.MeasurmentType = QuantityType.Unit;
                    ingredient.Quantity = (float)FractionToDouble(ingredients[i].Split(new string[] { " " }, StringSplitOptions.None)[0]);
                    ingredient.Name = ingredients[i].Split(new string[] { "</figcaption>" }, StringSplitOptions.None)[0].Replace(ingredients[i].Split(new string[] { " " }, StringSplitOptions.None)[0] + " ", "");
                }
                else if (int.TryParse(ingredients[i].Substring(0, 1), out guineapig))
                {
                    ingredient.MeasurmentType = QuantityType.Unit;
                    ingredient.Quantity = float.Parse(ingredients[i].Split(new string[] { " " }, StringSplitOptions.None)[0]);
                    ingredient.Name = ingredients[i].Split(new string[] { "</figcaption>" }, StringSplitOptions.None)[0].Replace(ingredients[i].Split(new string[] { " " }, StringSplitOptions.None)[0] + " ", "");
                }
                else
                {
                    throw new Exception("No fucking clue, debug time!");
                }

                ingredient.Name = WebUtility.HtmlDecode(ingredient.Name).Replace(" †", "");
                if (recepie.Ingredients.ContainsKey(ingredient.Name))
                    recepie.Ingredients.Remove(ingredient.Name);
                recepie.Ingredients.Add(ingredient.Name, ingredient);


            }

            return recepie;
        }

        public double FractionToDouble(string fraction)
        {
            double result;

            if (double.TryParse(fraction, out result))
            {
                return result;
            }

            string[] split = fraction.Split(new char[] { ' ', '/' });

            if (split.Length == 2 || split.Length == 3)
            {
                int a, b;

                if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
                {
                    if (split.Length == 2)
                    {
                        return (double)a / b;
                    }

                    int c;

                    if (int.TryParse(split[2], out c))
                    {
                        return a + (double)b / c;
                    }
                }
            }

            throw new FormatException("Not a valid fraction.");
        }

        private void PeriodicRecepieScrape_Elapsed(Object source, ElapsedEventArgs e)
        {
            periodicRecepieScrape.Stop();

            Dictionary<string, Recepie> IncompleteRecepies = _recepies.Where(a => (a.Value.Ingredients==null || a.Value.Ingredients.Count == 0) && (a.Value.IgnoreProductCompletely == false||a.Value.LastUpdated<DateTime.UtcNow.AddDays(-5))).ToDictionary(k => k.Key, v => v.Value);

            if (IncompleteRecepies.Count > 0)
            {
                foreach (Recepie incompleteRecepie in IncompleteRecepies.Values)
                {
                    LogMessage($"Recepie {incompleteRecepie.Title} doesn't have ingredients. Attempting to gather them.");

                    WebClient client = new WebClient();
                    Recepie fullRecepie = ConvertWebStringToRecepie(client.DownloadString(incompleteRecepie.RecepieUrl), incompleteRecepie);
                    if (fullRecepie.Ingredients.Count == 0)
                    {
                        fullRecepie.IgnoreProductCompletely = true;
                        LogMessage($"Recepie {incompleteRecepie.Title} STILL doesn't have ingredients. Marking as bad and will re-attempt in 5 days");
                    }
                    _recepies[fullRecepie.RecepieUrl] = fullRecepie;
                }

                SaveRecepies(_defaultFilePath);
            }

            if (_recepies.Count > 0)
            {
                WebClient client = new WebClient();

                

                if (_recepies.Values.ToList()[_recepieScrapeCount].LastUpdated < DateTime.UtcNow.AddDays(-5) || _recepies.Values.ToList()[_recepieScrapeCount].Calories == 0)
                {
                    LogMessage($"Scraping recepie '{_recepies.Values.ToList()[_recepieScrapeCount].Title}' as it's been five days since last scrape.");

                    Recepie fullRecepie = ConvertWebStringToRecepie(client.DownloadString(_recepies.Values.ToList()[_recepieScrapeCount].RecepieUrl), _recepies.Values.ToList()[_recepieScrapeCount]);
                    _recepies[fullRecepie.RecepieUrl] = fullRecepie;

                    SaveRecepies(_defaultFilePath);
                }

                _recepieScrapeCount++;

                if (_recepieScrapeCount > _recepies.Values.Count)
                {
                    _recepieScrapeCount = 0;
                }

                
            }

            Random rnd = new Random();

            if (_recepies.Count == 0)
                periodicRecepieScrape.Interval = 1 * 1000;
            else if (_discoveryPhaseStarted)
            {
                periodicRecepieScrape.Interval = 1 * 1000;
                _waitingToFinish = true;
            }
            else if (!_discoveryPhaseStarted && _waitingToFinish)
            {
                _waitingToFinish = false;
            }
            else
                periodicRecepieScrape.Interval = rnd.Next(300, 900) * 1000;

            periodicRecepieScrape.Start();
        }

        #endregion

        #region logging methods

        public string GetLog()
        {
            StringBuilder sb = new StringBuilder();            

            foreach(KeyValuePair<DateTime,string> entry in logging.OrderByDescending(o => o.Key).ToDictionary(k => k.Key, v => v.Value))
            {
                sb.Append($"[{entry.Key.ToString()}] {entry.Value} {System.Environment.NewLine}");
            }

            return sb.ToString();
        }

        private void LogMessage(string message)
        {
            logging.TryAdd(DateTime.UtcNow, message);

            if(logging.Count>100)
            {
                logging = new ConcurrentDictionary<DateTime, string>(logging.OrderByDescending(o => o.Key).Take(100).ToDictionary(k => k.Key, v => v.Value));
            }
        }

        #endregion

    }
}
