﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trinh_duyet
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        LinkedList<string> url_List = new LinkedList<string>();
        private bool getUrl = true;

        public bool Validate(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        private void tbxUrl_KeyDown(object sender, KeyEventArgs e)
        {
            getUrl = true;
            string url = FormatUrl(tbxUrl.Text.Trim());
            var selected = tabControl.SelectedTab;
            if (e.KeyCode == Keys.Enter)
            {
                if (Validate(url))
                {
                    //url_List.AddLast(selected.Tag + " " + url);
                    //webBrowser1.Url = new Uri(url);
                    WebBrowser web = (WebBrowser)selected.Controls["webBrowser1"];
                    web.Url = new Uri(url);
                    
                }
                else
                {
                    MessageBox.Show("Website not found", "Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

     

        private void btnNext_Click(object sender, EventArgs e)
        {
            getUrl = false;
            //LinkedListNode<string> currentUrl = url_List.First;
            var selected = tabControl.SelectedTab;
            string url = FormatUrl(tbxUrl.Text.Trim());
            LinkedListNode<string> currentUrl = url_List.FindLast(selected.Tag + " " + url);
            if (currentUrl.Next == null)
            {
                MessageBox.Show("The forward URL does not exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                LinkedListNode<string> nextUrl = currentUrl.Next;
                string[] url_details = nextUrl.Value.Split(' ');
                webBrowser1.Url = new Uri(url_details[1]);
                tbxUrl.Text = url_details[1];
                webBrowser1_DocumentCompleted(sender, e);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getUrl = true;
            string url = FormatUrl(tbxUrl.Text.Trim());
            var selected = tabControl.SelectedTab;
            if (Validate(url))
            {
                //webBrowser1.Url = new Uri(url);
                WebBrowser web = (WebBrowser)selected.Controls["webBrowser1"];
                web.Url = new Uri(url);
                url_List.AddLast(selected.Tag + " " + url);
                //selected.Tag = url;
            }
            else
            {
                MessageBox.Show("Website not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            getUrl = false;
            //LinkedListNode<string> currentUrl = url_List.Last;
            var selected = tabControl.SelectedTab;
            string url = FormatUrl(tbxUrl.Text.Trim());
            LinkedListNode<string> currentUrl = url_List.FindLast(selected.Tag + " " + url);
            if (currentUrl.Previous == null)
            {
                MessageBox.Show("The previous URL does not exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                LinkedListNode<string> prevUrl = currentUrl.Previous;
                string[] url_details = prevUrl.Value.Split(' ');
                webBrowser1.Url = new Uri(url_details[1]);
                tbxUrl.Text = url_details[1];
                webBrowser1_DocumentCompleted(sender, e);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            getUrl = false;
            var selected = tabControl.SelectedTab;
            WebBrowser web = (WebBrowser)selected.Controls["webBrowser1"];
            string url = tbxUrl.Text.Trim();
            web.Refresh();
            webBrowser1_DocumentCompleted(sender, e);
        }

        private void webBrowser1_DocumentCompleted(object sender, EventArgs e)
        {
            var selected = tabControl.SelectedTab;
            WebBrowser web = (WebBrowser)selected.Controls["webBrowser1"];
            selected.Text = web.Document.Title;
        }

        //private void tabPage2_Click(object sender, EventArgs e)
        //{
        //    NTabPage np = new NTabPage();

        //    np.Text = "New Tab";
        //    np.PageMode = "Custom Tab Prop String";

        //    //tcTabControl.TabPages.Add(np);
        //    tabControl.TabPages.Add(np);

        //    np = null;
        //}

        private void tabControl_Click(object sender, EventArgs e)
        {
            getUrl = true;
            var selectedTab = tabControl.SelectedTab;
            if (selectedTab.Text == "Add")
            {
                var tab = new TabPage();
                var browser1 = new WebBrowser();
                //LinkedList<string> url_List1 = new LinkedList<string>();
                //var toolstrip = new System.Windows.Forms.ToolStrip();
                tab.Text = "Add";
                //toolstrip.Items = toolStrip1;

                // add tab to tabcontrol
                tabControl.TabPages.Add(tab);
                tabControl.SelectedTab = tab;
                //tab.Name = "Add";

                selectedTab.Text = "New Tab";
                selectedTab.Tag = tabControl.SelectedIndex;
                //Console.WriteLine(tabControl.SelectedIndex);
                //toolstrip.Parent = selectedTab;

                

                browser1.Parent = selectedTab;
                browser1.Dock = DockStyle.Fill;
                browser1.Name = "webBrowser1";
                browser1.ScriptErrorsSuppressed = true;
                browser1.Navigate("https://www.google.com");
                browser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
                toolStrip1.Parent = selectedTab;
            }
            toolStrip1.Parent = selectedTab;

            tabControl.SelectedTab = selectedTab;

            //var browser = GetCurrentBrowser();
            //if (browser != null)
            //{
            //    if (browser.Url != null)
            //    {
            //        string url = browser.Url.ToString();
            //        webBrowser1.Url = new Uri(url);
            //        tbxUrl.Text = url;
            //    }
            //    else
            //        tbxUrl.Text = "";
            //}
        }

        string FormatUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            if (!url.Contains("https"))
                url = string.Format("https://{0}", url);

            return url;
        }

        WebBrowser GetCurrentBrowser()
        {
            var selectedTab = tabControl.SelectedTab;
            if (selectedTab == null)
                return null;

            if (selectedTab.Controls.ContainsKey("webBrowser1"))
            {
                return (WebBrowser)selectedTab.Controls["webBrowser1"]; ;
            }

            return null;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

            //var browser = GetCurrentBrowser();
            //if (browser != null)
            //{
            //    if (browser.Url != null)
            //    {
            //        string url = browser.Url.ToString();
            //        webBrowser1.Url = new Uri(url);
            //        tbxUrl.Text = url;
            //    }
            //    else
            //        tbxUrl.Text = "";
            //}
            var selected = tabControl.SelectedTab;
            WebBrowser web = GetCurrentBrowser();
            if (web != null)
            {
                if (web.Url != null)
                {
                    tbxUrl.Text = web.Url.AbsoluteUri;
                    webBrowser1_DocumentCompleted(sender, e);
                }
                else
                {
                    tbxUrl.Text = "";
                    toolStrip1.Parent = selected;
                }
            }
        }

        private void tbxUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (getUrl)
            {
                WebBrowser web = GetCurrentBrowser();
                string url = web.Url.ToString();
                var selected = tabControl.SelectedTab;
                url_List.AddLast(selected.Tag + " " + url);
                tbxUrl.Text = url;
            }
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButton1.DropDownItems.Clear();
            foreach (string i in url_List)
            {
                toolStripButton1.DropDownItems.Add(i);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WebBrowser web = GetCurrentBrowser();
            web.Navigate("https://www.google.com");
        }
    }
}
