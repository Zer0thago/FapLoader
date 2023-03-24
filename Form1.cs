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

                var mediaCountText = doc.DocumentNode.SelectSingleNode("/html/body/div/div[2]/div/div[3]/div[2]/div[2]/div/text()");
                var likesCount = doc.DocumentNode.SelectSingleNode("/html/body/div/div[2]/div/div[3]/div[2]/div[2]/div[2]/text()");
                guna2HtmlLabel3.Text = "Likes: " + likesCount?.InnerText?.Trim();
                guna2HtmlLabel2.Text = "Media: " + Convert.ToInt32(mediaCountText?.InnerText?.Trim());

                guna2ProgressBar1.Maximum = Convert.ToInt32(mediaCountText?.InnerText?.Trim());

                if (link == "https://fapello.com/" + name + "/" || link == "https://fapello.com/" + name)
                {

                    stopwatch.Start();
                    string firstTwo = name.Substring(0, 2);
                    string first = firstTwo[0].ToString();
                    string second = firstTwo[1].ToString();
                    guna2Button1.Text = "Stop";

                    await Task.Run(async () =>
                    {

                        Directory.CreateDirectory(downloadPath);

                        for (int i = 1; i < Convert.ToInt32(mediaCountText?.InnerText?.Trim()) + 1; i++)
                        {
                            if (guna2Button1.Text == "Download")
                            {
                                break;
                            }
                            using (WebClient client = new WebClient())
                            {
                                try
                                {

                                    byte[] data = await client.DownloadDataTaskAsync($"https://fapello.com/content/{first}/{second}/{name}/1000/{name}_{i.ToString("D4")}.jpg");

                                    string imagePath = Path.Combine(downloadPath, $"{name}_000{i}.jpg");

                                    File.WriteAllBytes(imagePath, data);



                                    using (MemoryStream ms = new MemoryStream(data))
                                    {
                                        if (showimage == true)
                                        {
                                            Image image = Image.FromStream(ms);
                                            guna2PictureBox1.BeginInvoke(new Action(() => guna2PictureBox1.Image = image));

                                        }
                                        guna2ProgressBar1.Invoke(new Action(() => guna2ProgressBar1.Value += 1)); ;

                                    }

                                    if (i == Convert.ToInt32(mediaCountText?.InnerText?.Trim()))
                                    {
                                        break;
                                    }

                                }
                                catch (Exception)
                                {


                                }

                                try
                                {
                                    string imagePath = Path.Combine(downloadPath, $"{name}_000{i}.jpg");
                                    byte[] data2 = await client.DownloadDataTaskAsync($"https://fapello.com/content/{first}/{second}/{name}/2000/{name}_{i.ToString("D4")}.jpg");
                                    File.WriteAllBytes(imagePath, data2);


                                    using (MemoryStream ms = new MemoryStream(data2))
                                    {
                                        if (showimage == true)
                                        {
                                            Image image = Image.FromStream(ms);
                                            guna2PictureBox1.BeginInvoke(new Action(() => guna2PictureBox1.Image = image));

                                        }
                                        guna2ProgressBar1.Invoke(new Action(() => guna2ProgressBar1.Value += 1)); ;
                                    }

                                    if (i == Convert.ToInt32(mediaCountText?.InnerText?.Trim()))
                                    {
                                        break;
                                    }

                                }
                                catch (Exception)
                                {


                                }


                                try
                                {
                                    string imagePath = Path.Combine(downloadPath, $"{name}_000{i}.jpg");
                                    byte[] data2 = await client.DownloadDataTaskAsync($"https://fapello.com/content/{first}/{second}/{name}/3000/{name}_{i.ToString("D4")}.jpg");
                                    File.WriteAllBytes(imagePath, data2);


                                    using (MemoryStream ms = new MemoryStream(data2))
                                    {
                                        if (showimage == true)
                                        {
                                            Image image = Image.FromStream(ms);
                                            guna2PictureBox1.BeginInvoke(new Action(() => guna2PictureBox1.Image = image));

                                        }
                                        guna2ProgressBar1.Invoke(new Action(() => guna2ProgressBar1.Value += 1)); ;
                                    }

                                    if (i == Convert.ToInt32(mediaCountText?.InnerText?.Trim()))
                                    {
                                        break;
                                    }

                                }
                                catch (WebException) { }

                                try
                                {
                                    string imagePath = Path.Combine(downloadPath, $"{name}_000{i}.jpg");
                                    byte[] data2 = await client.DownloadDataTaskAsync($"https://fapello.com/content/{first}/{second}/{name}/4000/{name}_{i.ToString("D4")}.jpg");
                                    File.WriteAllBytes(imagePath, data2);


                                    using (MemoryStream ms = new MemoryStream(data2))
                                    {
                                        if (showimage == true)
                                        {
                                            Image image = Image.FromStream(ms);
                                            guna2PictureBox1.BeginInvoke(new Action(() => guna2PictureBox1.Image = image));

                                        }
                                        guna2ProgressBar1.Invoke(new Action(() => guna2ProgressBar1.Value += 1)); ;
                                    }

                                    if (i == Convert.ToInt32(mediaCountText?.InnerText?.Trim()))
                                    {
                                        break;
                                    }

                                }
                                catch (WebException) { }

                                try
                                {
                                    string imagePath = Path.Combine(downloadPath, $"{name}_000{i}.jpg");
                                    byte[] data2 = await client.DownloadDataTaskAsync($"https://fapello.com/content/{first}/{second}/{name}/5000/{name}_{i.ToString("D4")}.jpg");
                                    File.WriteAllBytes(imagePath, data2);


                                    using (MemoryStream ms = new MemoryStream(data2))
                                    {
                                        if (showimage == true)
                                        {
                                            Image image = Image.FromStream(ms);
                                            guna2PictureBox1.BeginInvoke(new Action(() => guna2PictureBox1.Image = image));

                                        }
                                        guna2ProgressBar1.Invoke(new Action(() => guna2ProgressBar1.Value += 1)); ;
                                    }

                                    if (i == Convert.ToInt32(mediaCountText?.InnerText?.Trim()))
                                    {
                                        break;
                                    }

                                }
                                catch (WebException) { }
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
            guna2HtmlLabel6.Text = stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
            decimal percentage = (decimal)guna2ProgressBar1.Value / guna2ProgressBar1.Maximum;
            guna2HtmlLabel1.Text = $"{percentage:P}";

            if (guna2ProgressBar1.Value == guna2ProgressBar1.Maximum)
            {
                stopwatch.Stop();
                guna2HtmlLabel5.Text = "done! Duration: " + stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
                guna2HtmlLabel5.Visible = true;
            }

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
    }

    public class MyData
    {
        public string Name { get; set; }
    }
}
