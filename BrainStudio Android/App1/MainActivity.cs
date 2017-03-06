using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace BFIDE
{
    [Activity(Label = "BrainStudio", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private static bool isopen = false;
        private static string VER = "0.0.0.1";

        private static readonly int BUFSIZE = 65535;

        private int[] buf = new int[BUFSIZE];

        private int ptr { get; set; }

        private bool echo { get; set; }
        protected override void OnCreate(Bundle bundle)
        {
            MobileCenter.Start("3e4249fb-a7c0-4ab9-8dcd-ff5b872120c6",
                    typeof(Analytics), typeof(Crashes));
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var editText = FindViewById<EditText>(Resource.Id.editText);
            
                
            var button = FindViewById<Button>(Resource.Id.button2);
            button.Click += delegate
            {
                Interpret(">"+editText.Text);
                
            };

        }
        public async void Interpret(string s)

        {
            try
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
                                var outp = FindViewById<TextView>(Resource.Id.textView);
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
                                var inp = FindViewById<EditText>(Resource.Id.inpText);
                                string key = inp.Text;
                                this.buf[this.ptr] = (int)Convert.ToChar(key);

                                break;

                            }

                    }

                    i++;

                }
            }
            catch
            {
                
            }

        }
    }
}

