using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanInput
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox_URL_Show.Text = "https://agent.sh.189.cn";
        }

        private void BTN_OPEN_URL_Click(object sender, EventArgs e)
        {
            webb.Navigate(textBox_URL_Show.Text);
        }

        private void webb_NewWindow(object sender, CancelEventArgs e)
        {
            //打开新窗口的方式是在已有的窗口内打开
            webb.Url = new Uri(((WebBrowser)sender).StatusText);
            e.Cancel = true;

        }

        private void webb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // 页面跳转后改变地址栏地址
           // webb.Dispose();
            textBox_URL_Show.Text = webb.Url.ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 释放WEB资源
            webb.Dispose();
        }

        private void BTN_In_Lib_Click(object sender, EventArgs e)
        {
            webb.Navigate("https://agent.sh.189.cn/MTMSNEW/LoginOA.faces");
        }

        private void BTN_GET_STR_Click(object sender, EventArgs e)
        {
            // <input name="cTelListQuery:orgName" class="input1" id="cTelListQuery:orgName" type="text"/>
            HtmlDocument htmlDoc = webb.Document;
            HtmlElement btnElement = htmlDoc.All["cTelListQuery:orgName"];
            HtmlElement btnSubmit  = htmlDoc.All["cTelListQuery:_idJsp5"];
            if (btnElement != null)
            {
                btnElement.SetAttribute("value", "A000006948A05B");
               // btnElement.AttachEventHandler("onclick", new EventHandler(HtmlBtnClose_Click));
               if(btnSubmit != null )
                btnSubmit.InvokeMember("click");
                //btnElement.click += new HtmlElementEventHandler(HtmlBtnClose_Click);
            }
        }

        private void BTN_GET_RETURE_Click(object sender, EventArgs e)
        {
            //  < span class="input10" style="text-align: center; width: 260px; font-size: 18px;">
            //文字 - 录入失败：864207036043376串号不在天翼总库或者不为空闲状态

            // <span class="input12" style="text-align: center; width: 260px; font-size: 18px;">
            // A0000069489865: 串号录入成功

            foreach (HtmlElement he in webb.Document.GetElementsByTagName("span"))
            {
                string str = he.GetAttribute("className");

                if (str == "item")
                {
                    textBoxGetReturn.Text += he.InnerHtml;
                }
                else if (str == "input10")
                {
                    textBoxGetReturn.Text = "input10 ";
                    textBoxGetReturn.Text += he.InnerHtml;
                }
                else if (str == "input12")
                {
                    textBoxGetReturn.Text = "input12 ";
                    textBoxGetReturn.Text += he.InnerHtml;
                }

            }
        }
    }
}
