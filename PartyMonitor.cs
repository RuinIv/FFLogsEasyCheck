using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using Advanced_Combat_Tracker;
using AngleSharp.Parser.Html;
using AngleSharp.Dom.Html;
using Tulpep.NotificationWindow;

namespace FFLogsEasyCheck
{
    public enum Servers
    {
        Adamantoise = 000,
        Balmung = 001,
        Behemoth = 002,
        Brynhildr = 003,
        Cactuar = 004,
        Coeurl = 005,
        Diabolos = 006,
        Excalibur = 007,
        Exodus = 008,
        Faerie = 009,
        Famfrit = 010,
        Gilgamesh = 011,
        Goblin = 012,
        Hyperion = 013,
        Jenova = 014,
        Lamia = 015,
        Leviathan = 016,
        Malboro = 017,
        Mateus = 018,
        Midgardsormr = 019,
        Sargatanas = 020,
        Siren = 021,
        Ultros = 022,
        Zalera = 023,
        //EU
        Cerberus = 100,
        Lich = 101,
        Louisoix = 102,
        Moogle = 103,
        Odin = 104,
        Omega = 105,
        Phoenix = 106,
        Ragnarok = 107,
        Shiva = 108,
        Spriggan = 109,
        Twintania = 110,
        Zodiark = 111,
        //JP
        Aegis = 200,
        Alexander = 201,
        Anima = 202,
        Asura = 203,
        Atomos = 204,
        Bahamut = 205,
        Belias = 206,
        Carbuncle = 207,
        Chocobo = 208,
        Durandal = 209,
        Fenrir = 210,
        Garuda = 211,
        Gungnir = 212,
        Hades = 213,
        Ifrit = 214,
        Ixion = 215,
        Kujata = 216,
        Mandragora = 217,
        Masamune = 218,
        Pandaemonium = 219,
        Ramuh = 220,
        Ridill = 221,
        Shinryu = 222,
        Tiamat = 223,
        Titan = 224,
        Tonberry = 225,
        Typhon = 226,
        Ultima = 227,
        Unicorn = 228,
        Valefor = 229,
        Yojimbo = 230,
        Zeromus = 231,
        // KR
        초코보 = 300,
        카벙클 = 301,
        모그리 = 302,
        톤베리 = 303,
    }

    public enum Regions
    {
        NA = 000,
        EU = 100,
        JP = 200,
        KR = 300,
    }

    public partial class PartyMonitor : UserControl, IActPluginV1
    {
        private string PartyJoinMessageFooter = "joins the party.";
        private const string PartyJoinMessageFooterEn = "joins the party.";
        private const string PartyJoinMessageFooterKr = " 님이 파티에 참가했습니다.";
        private readonly string settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName,
            "Config\\FFlLogsEasyCheck.config.xml");

        private Label lblStatus;
        private SettingsSerializer xmlSettings;
        private List<string> logs = new List<string>();

        internal class RankData
        {
            public string profilePicUrl;
            public string job;
            public int rank;
            public int allStarPoints;

            public RankData(string profilePicUrl, string job, int rank, int allStarPoints)
            {
                this.profilePicUrl = profilePicUrl;
                this.job = job;
                this.rank = rank;
                this.allStarPoints = allStarPoints;
            }

            private RankData() { }
        }

        public PartyMonitor ()
        {
            InitializeComponent();
        }

        object[] servers;
        public void InitPlugin (TabPage pluginScreenSpace, Label pluginStatusText)
        {
            lblStatus = pluginStatusText; // Hand the status label's reference to our local var
            pluginScreenSpace?.Controls.Add(this); // Add this UserControl to the tab ACT provides
            pluginScreenSpace.GotFocus += PluginScreenSpace_GotFocus;
            Dock = DockStyle.Fill; // Expand the UserControl to fill the tab's client space
            xmlSettings = new SettingsSerializer(this); // Create a new settings serializer and pass it this instance
            servers = Enum.GetValues(typeof(Servers)).Cast<object>().ToArray();
            RegionDropdown.Items.AddRange(Enum.GetValues(typeof(Regions)).Cast<object>().ToArray());
            LoadSettings();

            ActGlobals.oFormActMain.OnLogLineRead += OnLogLineReadAsync;
            lblStatus.Text = "Plugin Started";
        }

        private void PluginScreenSpace_GotFocus (object sender, EventArgs e)
        {
            RegionDropdown_SelectedIndexChanged(null, null);
        }

        public void DeInitPlugin ()
        {
            ActGlobals.oFormActMain.OnLogLineRead -= OnLogLineReadAsync;
            SaveSettings();
            lblStatus.Text = "Plugin Exited";
        }

        private void OnLogLineReadAsync (bool isImport, LogLineEventArgs logInfo)
        {
            if(ActGlobals.oFormActMain.InvokeRequired)
	        {
                ActGlobals.oFormActMain.Invoke(new Action(() => Task.Run(() => ParseLogForPartyInfo(logInfo))));
            }
	        else
	        {
                // Faster if invoke is not needed
                Task.Run(() => ParseLogForPartyInfo(logInfo));
            }
        }

        private async Task ParseLogForPartyInfo (LogLineEventArgs logInfo)
        {
            var log = logInfo.logLine;
            //2 for the pos after the ] then the space
            log = log.Substring(log.IndexOf(']') + 2);
            // `/echo DEBUG FFLEC`
            var debugFlag = log.Contains("DEBUG FFLEC");
            if (log.StartsWith("00:1039:") || log.StartsWith("00:2239:") || debugFlag)
            {
                if (log.EndsWith(PartyJoinMessageFooter) || debugFlag)
                {
                    string serverName = "", characterName = "", regionName = "";
                    Servers server = Servers.Adamantoise;
                    if (!debugFlag)
                    {
                        foreach (var ser in servers)
                        {
                            string s = Enum.GetName(typeof(Servers), ser);
                            if (log.Contains(s))
                            {
                                serverName = s;
                                server = (Servers)ser;
                                log = log.Replace(s, "");
                                break;
                            }
                        }
                        if (serverName == "")
                        {
                            if (ServerDropdown.SelectedIndex >= 0)
                            {
                                serverName = Enum.GetName(typeof(Servers), ServerDropdown.SelectedItem);
                                server = (Servers)ServerDropdown.SelectedItem;
                            }
                            else
                            {
                                //TODO we dont have any server information at this point so we cant look them up, tell the user something went wrong and to check their server settings in the plugin ui
                                ShowPopup("Error", "Could not find the server that the new party member belongs to. Check the plugin settings in ACT to make sure your server is set to your logged-in character's home world.");
                                return;
                            }
                        }
                        //Message type header is 8 chars long so we start at 9
                        characterName = log.Substring(8, log.IndexOf(PartyJoinMessageFooter) - 8).Trim();
                    }
                    else
                    {
                        server = Servers.Chocobo;
                        serverName = Enum.GetName(typeof(Servers), server);
                        characterName = "Yoshi'p Sampo";
                    }
                    regionName = Enum.GetName(typeof(Regions), GetRegionFromServer(server));
                    serverName = TranslateServer(serverName);
                    var encodedRegion = Uri.EscapeUriString(regionName);
                    var encodedName = Uri.EscapeUriString(characterName);
                    var encodedServer = Uri.EscapeUriString(serverName);
                    var url = $"https://www.fflogs.com/character/{encodedRegion}/{encodedServer}/{encodedName}";
                    if(regionName == "KR") 
                        url = $"https://ko.fflogs.com/character/{encodedRegion}/{encodedServer}/{encodedName}";
                    string title = $"{characterName} ({serverName} {regionName}) joins the party!";
                    //Add to the ACT window text log
                    AddLineToLog(title + $" ({url})");
                    //Show Notification
                    if (showNotificationBox.Checked)
                    {
                        if (regionName == "KR")
                        {
                            ShowPopup(title, "현재 한국 서버는 팝업 메시지가 완성되지 않았습니다. 클릭해서 웹사이트를  확인해주세요.", null, OnClick: () => Process.Start(url));
                        }
                        else
                        {
                            var rankData = await ScrapeProfileData(url);
                            Image profilePic = null;
                            if (rankData != null)
                                profilePic = DownloadProfilePic(rankData.profilePicUrl);
                            var rankDataCorrupt = rankData == null || string.IsNullOrEmpty(rankData.job) || rankData.rank == -1 || rankData.allStarPoints == -1;
                            var body = rankDataCorrupt ? "Could not retrieve rank data." : $"{rankData.job} - Rank {rankData.rank} ({rankData.allStarPoints})\n\nClick Here for Full Logs!";
                            ShowPopup(title, body, profilePic, OnClick: () => Process.Start(url));
                        }
                    }
                    //Auto Open
                    if (autoOpenLogsBox.Checked)
                    {
                        Process.Start(url);
                    }
                }
            }
        }

        public void AddLineToLog(string line)
        {
            ActGlobals.oFormActMain.Invoke(new Action(() =>
            {
                logs.Add(line);
                if (logs.Count > 1000) logs.RemoveAt(0);
                logTextBox.Lines = logs.ToArray();
                logTextBox.SelectionStart = logTextBox.Text.Length;
                logTextBox.ScrollToCaret();
            }));
        }

        public static void ShowPopup(string title, string body, Image picture = null, Action OnClick = null, Action OnDisposed = null)
        {
            ActGlobals.oFormActMain.Invoke(new Action(() =>
            {
                var popup = new PopupNotifier();
                popup.TitleText = title;
                popup.Delay = 5000;
                popup.ContentText = body; 
                popup.BodyColor = Color.DarkBlue;
                popup.ContentColor = Color.White;
                popup.HeaderColor = Color.DarkGray;
                popup.TitleColor = Color.White;
                popup.TitleFont = new Font("Power Green", 10, FontStyle.Bold);
                popup.ContentFont = new Font("Arial", 8, FontStyle.Regular);
                popup.UseDarkBodyGradient = true;
                popup.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
                popup.GradientPower = 75;
                //p is the popup sending itself back with empty args
                if(OnClick != null)
                    popup.Click += (p, emptyArgs) => { OnClick(); };
                if (OnDisposed != null)
                    popup.Disposed += (p, emptyArgs) => { OnDisposed(); };
                popup.IsRightToLeft = false;
                popup.ShowCloseButton = true;
                if (picture != null)
                {
                    popup.Image = picture;
                    popup.ImagePadding = new Padding(1, 0, 0, 1);
                    popup.ImageSize = new Size(90, 90);
                }
                popup.Popup();
            }));
        }

        private void LoadSettings ()
        {
            // Add any controls you want to save the state of
            //xmlSettings.AddControlSetting(textBox1.Name, textBox1);
            xmlSettings.AddLongSetting("LastSavedContentId");
            xmlSettings.AddControlSetting(RegionDropdown.Name, RegionDropdown);
            xmlSettings.AddControlSetting(ServerDropdown.Name, ServerDropdown);
            xmlSettings.AddControlSetting(showNotificationBox.Name, showNotificationBox);
            xmlSettings.AddControlSetting(autoOpenLogsBox.Name, autoOpenLogsBox);

            if (File.Exists(settingsFile))
            {
                var fs = new FileStream(settingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var xReader = new XmlTextReader(fs);

                try
                {
                    while (xReader.Read())
                        if (xReader.NodeType == XmlNodeType.Element)
                            if (xReader.LocalName == "SettingsSerializer")
                                xmlSettings.ImportFromXml(xReader);
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error loading settings: " + ex.Message;
                }

                xReader.Close();
            }
        }

        private void SaveSettings ()
        {
            var fs = new FileStream(settingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            var xWriter = new XmlTextWriter(fs, Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;
            xWriter.Indentation = 1;
            xWriter.IndentChar = '\t';
            xWriter.WriteStartDocument(true);
            xWriter.WriteStartElement("Config"); // <Config>
            xWriter.WriteStartElement("SettingsSerializer"); // <Config><SettingsSerializer>
            xmlSettings.ExportToXml(xWriter); // Fill the SettingsSerializer XML
            xWriter.WriteEndElement(); // </SettingsSerializer>
            xWriter.WriteEndElement(); // </Config>
            xWriter.WriteEndDocument(); // Tie up loose ends (shouldn't be any)
            xWriter.Flush(); // Flush the file buffer to disk
            xWriter.Close();
        }

        internal async Task<RankData> ScrapeProfileData(string siteUrl)
        {
            try
            {
                var response = await GetDataFromUrl(siteUrl);
                HtmlParser parser = new HtmlParser();
                IHtmlDocument document = parser.Parse(response);
                int rank = -1;
                int asp = -1;
                var pic = document.GetElementById("character-portrait-image")?.Attributes?.FirstOrDefault(a => a.Name == "src")?.Value;
                var job = AddSpacesAfterCapitals(document.GetElementsByClassName("allstar-header-icon")?.FirstOrDefault()?.ClassList?.FirstOrDefault(s => s.Contains("actor-sprite-"))?.Substring(13));
                int.TryParse(document.GetElementsByClassName("header-zone-positions")?.FirstOrDefault()?.GetElementsByClassName("header-rank")?.FirstOrDefault()?.TextContent, out rank);
                int.TryParse(document.GetElementsByClassName("header-zone-points")?.FirstOrDefault()?.GetElementsByClassName("header-rank")?.FirstOrDefault()?.TextContent, out asp);
                var rankData = new RankData(pic, job, rank, asp);
                return rankData;
            }
            catch(OperationCanceledException e)
            {
                //Network related error
                return null;
            }
        }

        internal async Task<Stream> GetDataFromUrl(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                CancellationTokenSource cancellationToken = new CancellationTokenSource();
                HttpResponseMessage request = await httpClient.GetAsync(url);
                cancellationToken.Token.ThrowIfCancellationRequested();

                Stream response = await request.Content.ReadAsStreamAsync();
                cancellationToken.Token.ThrowIfCancellationRequested();
                return response;
            }
        }

        string AddSpacesAfterCapitals(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        private Image DownloadProfilePic(string picUrl)
        {
            using (var wc = new WebClient())
            {
                using (var imgStream = new MemoryStream(wc.DownloadData(picUrl)))
                {
                    var profilePic = Image.FromStream(imgStream);
                    return profilePic;
                }
            }
        }

        private Regions GetRegionFromServer (Servers server)
        {
            var i = (int)server;
            if (i >= (int)Regions.NA && i < (int)Regions.EU) return Regions.NA;
            else if (i >= (int)Regions.EU && i < (int)Regions.JP) return Regions.EU;
            else if (i >= (int)Regions.JP && i < (int)Regions.KR) return Regions.JP;
            return Regions.KR;
        }

        private void RegionDropdown_SelectedIndexChanged (object sender, EventArgs e)
        {
            ServerDropdown.Items.Clear();
            ServerDropdown.ResetText();
            var selection = (Regions)RegionDropdown.SelectedItem;
            switch (selection)
            {
                default:
                    ServerDropdown.SelectedIndex = -1;
                    ServerDropdown.Enabled = false;
                    PartyJoinMessageFooter = PartyJoinMessageFooterEn;
                    break;

                case Regions.NA:
                    ServerDropdown.Enabled = true;
                    ServerDropdown.Items.AddRange(servers.Where(s => (int)s >= (int)Regions.NA && (int)s < (int)Regions.EU).ToArray());
                    PartyJoinMessageFooter = PartyJoinMessageFooterEn;
                    break;

                case Regions.EU:
                    ServerDropdown.Enabled = true;
                    ServerDropdown.Items.AddRange(servers.Where(s => (int)s >= (int)Regions.EU && (int)s < (int)Regions.JP).ToArray());
                    PartyJoinMessageFooter = PartyJoinMessageFooterEn;
                    break;

                case Regions.JP:
                    ServerDropdown.Enabled = true;
                    ServerDropdown.Items.AddRange(servers.Where(s => (int)s >= (int)Regions.JP && (int)s < (int)Regions.KR).ToArray());
                    PartyJoinMessageFooter = PartyJoinMessageFooterEn;
                    break;

                case Regions.KR:
                    ServerDropdown.Enabled = true;
                    ServerDropdown.Items.AddRange(servers.Where(s => (int)s >= (int)Regions.KR).ToArray());
                    PartyJoinMessageFooter = PartyJoinMessageFooterKr;
                    break;
            }
        }

        public string TranslateServer (string server)
        {
            switch (server)
            {
                case "초코보":
                    server = "Chocobo";
                    break;
                case "카벙클":
                    server = "Carbuncle";
                    break;
                case "모그리":
                    server = "Moogle";
                    break;
                case "톤베리":
                    server = "Tonberry";
                    break;
            }
            return server;
        }

        private void ServerDropdown_SelectedIndexChanged (object sender, EventArgs e)
        {

        }

        private void logTextBox_LinkClicked (object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void showNotificationBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void autoOpenLogsBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            logTextBox.Text = "";
        }
    }
}
