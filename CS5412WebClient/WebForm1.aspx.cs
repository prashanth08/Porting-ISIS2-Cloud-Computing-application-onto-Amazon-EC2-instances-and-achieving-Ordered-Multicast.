using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS5412WebClient
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Stopwatch stopwatch = new Stopwatch();
        long avgtime = 0;
        public const int UPDATE = 0;
        public const int LOOKUP = 1;
        static Semaphore go = new Semaphore(0, 1), dbFull = new Semaphore(0, 1), db_lock = new Semaphore(0, 1);
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "Loaded at " + DateTime.Now.ToString(); 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CS5412_Service.CS5412_WCFClient client = new CS5412_Service.CS5412_WCFClient("BasicHttpBinding_ICS5412_WCF");
            Label1.Text = client.GetMessage(TextBox1.Text);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            CS5412_Service.CS5412_WCFClient client = new CS5412_Service.CS5412_WCFClient("BasicHttpBinding_ICS5412_WCF");
            Label2.Text = "Performing the Get/Set Ratio Tests";
            //int num_read_to_writes = 0;

            //parsing the
            int num_read_to_writes = Convert.ToInt32(TextBox2.Text);

            //0 indicates read, 1 indicates write
            List<int> read_to_write_ratio = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                if (i < num_read_to_writes)
                    read_to_write_ratio.Add(0);
                else
                    read_to_write_ratio.Add(1);
               
            }

            Label2.Text = "R/w = " + num_read_to_writes.ToString() + " <br/>";

            
            String result = "null";
            Stopwatch stopwatch = new Stopwatch();
            List<int> iter = new List<int>();
            //using a random function so that a client can call read or write
            Random rnd = new Random();



                stopwatch.Reset();

                //ratio of reads to updates 
                for (int j = 0; j < 100; j++)
                {
                    List<String> attr_val_pairs = new List<String>();
                    attr_val_pairs.Add((j * 5).ToString() + '-' + (j * 5).ToString());

                    int r = rnd.Next(read_to_write_ratio.Count);

                    if (read_to_write_ratio[r] == 0)
                    {
                        stopwatch.Start();
                        client.Get(j.ToString());
                        stopwatch.Stop();
                    }
                    else
                    {
                        stopwatch.Start();
                        client.Set(j.ToString(), (j * 5).ToString(), (j * 41).ToString());
                        stopwatch.Stop();
                    }
                    iter.Add(stopwatch.Elapsed.Milliseconds);

                }
                // 1 get
                /*
                stopwatch.Start();
                Label1.Text += "Enitity Id: " + i.ToString() + " Attr Id: " + (i * 5).ToString() + " Value: " + cleint.GetData(i.ToString(), (i * 5).ToString()) + "<br/>";
                stopwatch.Stop();
                */
                //Add time fot 100 sets and 1 get
                //times.Add(stopwatch.Elapsed.Milliseconds);
            
            /*for (int repeat = 0; repeat < 5; ++repeat)
            {
                stopwatch.Reset();
                stopwatch.Start();
                result = "pb476";
                //client(repeat.ToString(), (repeat*5).ToString() + "value", (repeat*7).ToString() + "attkey");
                stopwatch.Stop();
                Console.WriteLine("Ticks: " + stopwatch.ElapsedTicks +
                " uS: " + stopwatch.ElapsedMilliseconds * 1000);
                avgtime += stopwatch.ElapsedMilliseconds * 1000;
            }
            for (int n = 0; n < 1; n++)
            {
                
                stopwatch.Reset();
                stopwatch.Start();
                result = client.Get(n.ToString());
                
                stopwatch.Stop();
                Console.WriteLine("Ticks: " + stopwatch.ElapsedTicks +
                " uS: " + stopwatch.ElapsedMilliseconds `* 1000);
                avgtime += stopwatch.ElapsedMilliseconds * 1000;//to measure in microseconds

                stopwatch.Reset();
                

            }*/
            Label2.Text = client.GetMessage(TextBox1.Text);
            Label2.Text = "Avg" + iter.Average();
            Label3.Text = "No of requests per second  " + 1000 / iter.Average();

        }
    }
}