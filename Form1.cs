using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SchuleIstCool
{
    public partial class Form1 : Form
    {
        bool showimage;
        public Form1()
        {
            InitializeComponent();
            showimage = true;
        }
        public Stopwatch stopwatch = new Stopwatch();
        private async void guna2Button1_Click(object sender, EventArgs e)
        {

            if (guna2Button1.Text == "Stop")
            {
                guna2Button1.Text = "Download";
                guna2ProgressBar1.Value = 0;
                guna2PictureBox1.Image = SchuleIstCool.Properties.Resources._207_2073352_open_hidden_icon_png;
            }
            else
            {
               

                var link = guna2TextBox1.Text;
                string[] linkParts = link.Split('/');
                string name = linkParts[linkParts.Length - 2];
                string downloadPath = Path.Combine(Environment.CurrentDirectory, "downloads", name);

                var clientt = new HttpClient();
                var response = await clientt.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);

                string xpath = "/html/body/div/div[2]/div/div[4]/div[1]/a/@href";
                string href = doc.DocumentNode.SelectSingleNode(xpath)?.GetAttributeValue("href", null);
                string pattern = @"/(\d+)/?$"; 
                Match match = Regex.Match(href, pattern);
                string number = match.Groups[1].Value;

                var mediaCountText = number;
                var likesCount = doc.DocumentNode.SelectSingleNode("/html/body/div/div[2]/div/div[3]/div[2]/div[2]/div[2]/text()");

                var username = doc.DocumentNode.SelectSingleNode("/html/body/div/div[2]/div/div[3]/div[2]/h2/text()");
                guna2HtmlLabel3.Text = "Likes: " + likesCount?.InnerText?.Trim();
                guna2HtmlLabel2.Text = "Media: " + Convert.ToInt32(mediaCountText);
                guna2HtmlLabel8.Text = username?.InnerText?.Trim();

                guna2ProgressBar1.Maximum = Convert.ToInt32(mediaCountText);

                string xpathProfilePic = "/html/body/div/div[2]/div/div[3]/div[1]/div/a/img";
                HtmlNode profilePicNode = doc.DocumentNode.SelectSingleNode(xpathProfilePic);
                string profilePicUrl = profilePicNode?.GetAttributeValue("src", null);

                if (!string.IsNullOrEmpty(profilePicUrl))
                {
                    using (WebClient client = new WebClient())
                    {
                        byte[] imageData = await client.DownloadDataTaskAsync(profilePicUrl);
                        using (MemoryStream mss = new MemoryStream(imageData))
                        {
                            Image image = Image.FromStream(mss);
                            guna2CirclePictureBox1.Image = image;
                        }
                    }
                }



                if (link == "https://fapello.com/" + name + "/" || link == "https://fapello.com/" + name)
                {

                    stopwatch.Start();
                    string firstTwo = name.Substring(0, 2);
                    string first = firstTwo[0].ToString();
                    string second = firstTwo[1].ToString();
                    guna2Button1.Text = "Stop";
                    Directory.CreateDirectory(downloadPath);

                    await Task.Run(async () =>
                    {
                        for (int i = 1; i <= Convert.ToInt32(mediaCountText); i++)
                        {
                            if (guna2Button1.Text == "Download")
                            {
                                break;
                            }
                            using (WebClient client = new WebClient())
                            {
                                guna2ProgressBar1.Value += 1;
                                int picturesDownloaded = 0;
                                DateTime startTime = DateTime.Now;
                                try
                                {
                                    string baseUrl = guna2TextBox1.Text.TrimEnd('/');
                                    string pageUrl = $"{baseUrl}/{i}/";
                                    string xpathImage = "/html/body/div/div[2]/div/div[3]/div/div[2]/a/img";
                                    string xpathVideo = "//source[@type='video/mp4']";
                                    HtmlWeb web = new HtmlWeb();
                                    var docc = web.Load(pageUrl);
                                    HtmlNode imgNode = docc.DocumentNode.SelectSingleNode(xpathImage);
                                    HtmlNode videoNode = docc.DocumentNode.SelectSingleNode(xpathVideo);
                                    string imageUrl = imgNode?.GetAttributeValue("src", "");
                                    string videoUrl = videoNode?.GetAttributeValue("src", "").Replace("cdn.", "");

                                    byte[] imageData = null;
                                    string imagePath = "";

                                    if (!string.IsNullOrEmpty(imageUrl))
                                    {
                                        imageData = await client.DownloadDataTaskAsync(imageUrl);
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            Image image = Image.FromStream(ms);
                                            guna2PictureBox1.BeginInvoke(new Action(() => guna2PictureBox1.Image = image));
                                        }
                                        imagePath = Path.Combine(downloadPath, $"{name}_{i:D4}" + Path.GetExtension(imageUrl));
                                        File.WriteAllBytes(imagePath, imageData);
                                    }

                                    picturesDownloaded++;
                                    DateTime endTime = DateTime.Now;
                                    TimeSpan elapsedTime = endTime - startTime;
                                    double secondsPerPicture = elapsedTime.TotalSeconds / picturesDownloaded;

                                    if (!string.IsNullOrEmpty(videoUrl))
                                    {
                                        videoUrl = videoUrl.Replace("cdn.", "");
                                        byte[] videoData = await client.DownloadDataTaskAsync(videoUrl);
                                        string videoPath = Path.Combine(downloadPath, $"{name}_{i:D4}" + Path.GetExtension(videoUrl));
                                        File.WriteAllBytes(videoPath, videoData);
                                    }

                                    guna2HtmlLabel7.BeginInvoke(new Action(() =>
                                    {
                                        guna2HtmlLabel7.Text = $"{secondsPerPicture:F2}MPS";
                                    }));

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                            }
                        }
                    });

                }
                else
                {
                    MessageBox.Show("Invalid Link!");
                }

                Console.WriteLine(name);
            }
            
        }

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {
            decimal percentage = (decimal)guna2ProgressBar1.Value / guna2ProgressBar1.Maximum;

            guna2HtmlLabel1.BeginInvoke(new Action(() =>
            {
                guna2HtmlLabel1.Text = $"{percentage:P}";
            }));

            guna2HtmlLabel6.BeginInvoke(new Action(() =>
            {
                guna2HtmlLabel6.Text = stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
            }));

            guna2ProgressBar1.BeginInvoke(new Action(() =>
            {
                if (guna2ProgressBar1.Value == guna2ProgressBar1.Maximum)
                {
                    stopwatch.Stop();
                    guna2HtmlLabel5.Text = "done! Duration: " + stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
                    guna2HtmlLabel5.Visible = true;
                }
            }));

           

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void deleteButton_Click(object sender, EventArgs e) 
        { 

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
            

            if(showimage == false)
            {
                showimage = true;
                guna2Button2.Text = "Hide";

                
            }
            else
            {
                guna2PictureBox1.Image = SchuleIstCool.Properties.Resources._207_2073352_open_hidden_icon_png;
                showimage = false;
                guna2Button2.Text = "Show";
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private int startHeight = 268;
        private int endHeight = 15;
        private bool isExpanding = false;

        private async void guna2Panel5_Click(object sender, EventArgs e)
        {
            int durationMs = 500; // duration of the animation in milliseconds

            if (!isExpanding)
            {
                // The panel is currently collapsed, so expand it
                startHeight = guna2Panel4.Height;
                endHeight = 268;
                isExpanding = true;
            }
            else
            {
                startHeight = guna2Panel4.Height;
                endHeight = 15;
                isExpanding = false;
            }

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 16; // update interval in milliseconds (approximately 60 fps)
            timer.Tick += (s, ev) =>
            {
                int elapsedTime = (int)timer.Tag; // elapsed time since the animation started
                int newHeight = Interpolate(startHeight, endHeight, elapsedTime, durationMs); // interpolate the height
                guna2Panel4.Height = newHeight; // set the new height
                if (elapsedTime >= durationMs)
                {
                    timer.Stop(); // stop the timer when the animation is complete
                }
                else
                {
                    elapsedTime += timer.Interval;
                    timer.Tag = elapsedTime;
                }
            };
            timer.Tag = 0;
            timer.Start();

            //Wait for the animation to complete before continuing
            await Task.Delay(durationMs);
        }

        // Interpolate the value of a variable between two values based on elapsed time and duration
        private int Interpolate(int startValue, int endValue, int elapsedTime, int duration)
        {
            float t = (float)elapsedTime / duration;
            t = Math.Max(0, Math.Min(1, t)); // clamp t to the range [0, 1]
            t = EasingFunction(t); // apply an easing function to t to create a smoother animation
            int value = (int)(startValue + (endValue - startValue) * t);
            return value;
        }

        // An easing function that maps t from [0, 1] to [0, 1] with a smooth curve
        private float EasingFunction(float t)
        {
            return t * t * (3 - 2 * t);
        }
    }

    public class MyData
    {
        public string Name { get; set; }
    }
}
