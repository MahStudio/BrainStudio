﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPBFIDE.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainF : Page
    {
        private static bool isopen = false;
        private static string VER = "0.0.0.1";

        private static readonly int BUFSIZE = 65535;

        private int[] buf = new int[BUFSIZE];

        private int ptr { get; set; }

        private bool echo { get; set; }
        public static MainF Current;
        public MainF()
        {
            this.InitializeComponent();
            Current = this;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
        }
        public void BrainFuckInterpreter()
        {

            this.ptr = 0;

            this.Reset();

        }
        public void Reset()

        {

            Array.Clear(this.buf, 0, this.buf.Length);

        }

       
        public void Interpret(string s)

        {

            int i = 0;

            int right = s.Length;

            while (i < right)

            {

                switch (s[i])

                {

                    case '>':

                        {

                            this.ptr++;

                            if (this.ptr >= BUFSIZE)

                            {

                                this.ptr = 0;

                            }

                            break;

                        }

                    case '<':

                        {

                            this.ptr--;

                            if (this.ptr < 0)

                            {

                                this.ptr = BUFSIZE - 1;

                            }

                            break;

                        }

                    case '.':

                        {

                            outp.Text += ((char)this.buf[this.ptr]);

                            break;

                        }

                    case '+':

                        {

                            this.buf[this.ptr]++;

                            break;

                        }

                    case '-':

                        {

                            this.buf[this.ptr]--;

                            break;

                        }

                    case '[':

                        {

                            if (this.buf[this.ptr] == 0)

                            {

                                int loop = 1;

                                while (loop > 0)

                                {

                                    i++;

                                    char c = s[i];

                                    if (c == '[')

                                    {

                                        loop++;

                                    }

                                    else

                                    if (c == ']')

                                    {

                                        loop--;

                                    }

                                }

                            }

                            break;

                        }

                    case ']':

                        {

                            int loop = 1;

                            while (loop > 0)

                            {

                                i--;

                                char c = s[i];

                                if (c == '[')

                                {

                                    loop--;

                                }

                                else

                                if (c == ']')

                                {

                                    loop++;

                                }

                            }

                            i--;

                            break;

                        }

                    case ',':

                        {

                            // read a key

                            string key = inp.Text; 
                            this.buf[this.ptr] = (int)Convert.ToChar(key);

                            break;

                        }

                }

                i++;

            }

        }

        public  void runer()
        {
            Interpret(textBox.Text);

        }
        public void cleaner()
        {
            title.Text = string.Empty;
            textBox.Text = string.Empty;
            inp.Text = String.Empty;
            outp.Text = String.Empty;

        }

        private void sandh_Click(object sender, RoutedEventArgs e)
        {
            if (isopen == false)
            {
                rotate.Begin();
                tools.Visibility = Visibility.Visible;
                isopen = true;

            }
            else
            {
                unrotate.Begin();
                tools.Visibility = Visibility.Collapsed;
                isopen = false;
            }
        }

        private void convert_TextChanged(object sender, TextChangedEventArgs e)
        {
            acci.Text = "";
            var a = convert.Text.ToCharArray();
            foreach (var item in a)
            {
                acci.Text += ((int)Convert.ToChar(item)).ToString() + "   ";
            }
        }

        private void doneedit_Click(object sender, RoutedEventArgs e)
        {
            if (editbox.Text != "")
                title.Text = editbox.Text;
        }
    }
}

