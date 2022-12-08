using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Trinh_duyet.Linked_List;

namespace Trinh_duyet
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Browser0.ScriptErrorsSuppressed = true;
        }



        //LinkedList<string> url_List = new LinkedList<string>();
        List url_List = new List();
        private bool getUrl = true;
        private int indexTab = 1;

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
                    WebBrowser web = (WebBrowser)selected.Controls[selected.Tag + ""];
                    web.Url = new Uri(url);
                   // webBrowser1_DocumentCompleted(sender, e);
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
            List tempo_List = FilterUrlSelectedTab(selected.Tag + " ");
            Node_Url currentUrl = tempo_List.FindLast(selected.Tag + " " + url);
            if (currentUrl.next == null)
            {
                MessageBox.Show("The forward URL does not exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Node_Url nextUrl = currentUrl.next;
                string[] url_details = nextUrl.url.Split(' ');
                WebBrowser web = (WebBrowser)selected.Controls[selected.Tag + ""];
                web.Url = new Uri(url_details[1]);
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
                WebBrowser web = (WebBrowser)selected.Controls[selected.Tag + ""];
                web.Url = new Uri(url);
                //url_List.AddLast(selected.Tag + " " + url);
                //webBrowser1_DocumentCompleted(sender, e);
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
            //List tempo_List = new List();
            //Node_Url i = url_List.head;
            //bool m = false;
            //while (i != null)
            //{
            //    m = i.url.Contains(selected.Tag + " ");
            //    if (m)
            //    {
            //        tempo_List.AddLast(i.url);
            //    }
            //    i = i.next;
            //}
            //Node_Url currentUrl = tempo_List.FindLast(selected.Tag + " " + url);

            List tempo_List = FilterUrlSelectedTab(selected.Tag + " ");
            Node_Url currentUrl = tempo_List.FindLast(selected.Tag + " " + url);
            //Node_Url k = tempo_List.head;
            //while (k != null)
            //{
            //    MessageBox.Show(k.url);
            //    k = k.next;
            //}

            if (currentUrl.prev == null)
            {
                MessageBox.Show("The previous URL does not exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Node_Url prevUrl = currentUrl.prev;
                string[] url_details = prevUrl.url.Split(' ');
                WebBrowser web = (WebBrowser)selected.Controls[selected.Tag + ""];
                web.Url = new Uri(url_details[1]);
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
            WebBrowser web = (WebBrowser)selected.Controls[selected.Tag + ""];
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

        void CloseCurrentTab()
        {
            if (tabControl.TabPages.Count == 1)
            {
                tabControl.TabPages.Clear();
                //tabControl.TabPages.RemoveAt(tabControl.SelectedIndex);
                newTab();
            }
            else
            {
                tabControl.TabPages.RemoveAt(tabControl.SelectedIndex);

            }

        }

        void newTab()
        {
            var selectedTab = tabControl.SelectedTab;
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
            selectedTab.Tag = "Browser" + indexTab;
            
            //Console.WriteLine(tabControl.SelectedIndex);
            //toolstrip.Parent = selectedTab;



            browser1.Parent = selectedTab;
            browser1.Dock = DockStyle.Fill;
            browser1.Name = "Browser" + indexTab;
            browser1.ScriptErrorsSuppressed = true;
            browser1.Navigate("https://www.google.com");
            browser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            toolStrip1.Parent = selectedTab;

            indexTab += 1;
        }

        private void tabControl_Click(object sender, EventArgs e)
        {
            getUrl = true;
            var selectedTab = tabControl.SelectedTab;
            if (selectedTab.Text == "Add")
            {
                //var tab = new tabpage();
                //var browser1 = new webbrowser();
                //linkedlist<string> url_list1 = new linkedlist<string>();
                //var toolstrip = new system.windows.forms.toolstrip();
                //tab.text = "add";
                //toolstrip.items = toolstrip1;

                //add tab to tabcontrol
                //tabcontrol.tabpages.add(tab);
                //tabcontrol.selectedtab = tab;
                //tab.name = "add";

                //selectedtab.text = "new tab";
                //selectedtab.tag = tabcontrol.selectedindex;
                //console.writeline(tabcontrol.selectedindex);
                //toolstrip.parent = selectedtab;



                //browser1.parent = selectedtab;
                //browser1.dock = dockstyle.fill;
                //browser1.name = "webbrowser1";
                //browser1.scripterrorssuppressed = true;
                //browser1.navigate("https://www.google.com");
                //browser1.navigated += new system.windows.forms.webbrowsernavigatedeventhandler(this.webbrowser1_navigated);
                //toolstrip1.parent = selectedtab;
                newTab();
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
            string x = selectedTab.Tag + "";
            if (selectedTab == null)
                return null;

            if (selectedTab.Controls.ContainsKey(x))
            {
                return (WebBrowser)selectedTab.Controls[x]; 
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
                //if (url_List != null)
                //    MessageBox.Show(url_List.head.url);
                url_List.AddLast(selected.Tag + " " + url);
                tbxUrl.Text = url;
                webBrowser1_DocumentCompleted(sender, e);
            }
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButton1.DropDownItems.Clear();
            Node_Url i = url_List.head;
            while (i != null)
            {
                toolStripButton1.DropDownItems.Add(i.url);
                i = i.next;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var selected = tabControl.SelectedTab;
            selected.Tag = "Browser0";
            //selected.Controls.ContainsKey(x);
            WebBrowser web = GetCurrentBrowser();
            web.Navigate("https://www.google.com");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CloseCurrentTab();
            var selected = tabControl.SelectedTab;
            toolStrip1.Parent = selected;
        }

        private List FilterUrlSelectedTab(string x)
        {
            var selected = tabControl.SelectedTab;
            List tempo_List = new List();
            Node_Url i = url_List.head;
            
            bool m = false;
            while (i != null)
            {
                m = i.url.Contains(selected.Tag + " ");
                if (m)
                {
                    tempo_List.AddLast(i.url);
                }
                i = i.next;
            }
            return tempo_List;
        }
    }
}
