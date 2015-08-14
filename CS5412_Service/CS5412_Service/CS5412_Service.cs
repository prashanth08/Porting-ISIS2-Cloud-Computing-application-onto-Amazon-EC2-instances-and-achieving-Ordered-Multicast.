using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Isis;
using System.Threading;
using System.Diagnostics;
//4Qot.d-VjAP
//mzAoA.=qMzP


namespace CS5412_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CS5412_WCF" in both code and config file together.
    public class CS5412_Service : ICS5412_WCF
    {
        public static Stopwatch stopwatch = new Stopwatch();
        long avgtime = 0;
        //static List<collection> database = new List<collection>();
        public const int UPDATE = 0;
        public const int LOOKUP = 1;
        public static Group g ;
        public static Dictionary<string, Dictionary<string, string>> entity;
        
        static Semaphore go = new Semaphore(0, 1), dbFull = new Semaphore(0, 1), db_lock = new Semaphore(0, 1);


            //public Dictionary<string, Dictionary<string, string>> entity = new Dictionary<string, Dictionary<string, string>>();

            public static void Isis_Start()
            {
                IsisSystem.Start();
                g = new Group("pb476_cs5412");
                int myRank = 0;
                
                
                g.ViewHandlers += (ViewHandler)delegate(View v)
                {
                    IsisSystem.WriteLine("New View: " + v);
                    myRank = v.GetMyRank();

                    if (v.members.Length == 5)
                        go.Release(1);
                };
                g.Handlers[UPDATE] += (Action<string, string, string>)delegate(string key, string value, string attkey)
                {
                    Dictionary<string, string> attr = new Dictionary<string, string>();
                    if (value == "null")
                    {
                        entity.Remove(key);
                    }
                    else if (entity.ContainsKey(key))
                    {
                        entity[key] = attr;
                        attr[key] = attkey;
                    }
                    else
                    {
                        entity.Add(key, attr);
                    }
                    //collection cp = new collection();
                    CS5412_Service cp = new CS5412_Service();
                    cp.Set(key, value, attkey);
                    IsisSystem.WriteLine("New collection: " + key + "/" + value + "/" + attkey);

                };
                g.Handlers[LOOKUP] += (Action<string>)delegate(string key)
                {
                    Dictionary<string, string> attr = new Dictionary<string, string>();
                    string result = null;
                    //attr = entity;

                    if (entity.ContainsKey(key))
                    {
                        result = attr[key];
                    }

                    
                    IsisSystem.WriteLine("=== Query for arg=" + key + "Result" + result);

                    /*List<string> answer = new List<string>();
                    //foreach (collection tp in database)
                        if (Get(key) == key)
                        {
                            IsisSystem.WriteLine("Including " + Get(key) + "/" + GetType());
                            answer.Add(Get(key));
                        }*/

                    g.Reply(result);
                };
                g.Join();
                IsisSystem.WriteLine("Time elapsed is " + stopwatch.Elapsed);

                IsisSystem.WriteLine("Wait until database is full!");
                dbFull.WaitOne();
                IsisSystem.WriteLine("Database is fully populated!");
            }
            public void Set(string key, string value, string attkey)
            {
                g.OrderedSend(UPDATE, key,value,attkey);
            }

            public void Get(string key)
            {
                g.OrderedQuery(LOOKUP, key);
            }

            List<string> multiatt = new List<string>()
	            {
	                "carrot",
	                "fox",
	                "explorer"
	            };
            public void GetMessage(string name)
            {
                String result = "Hello";
                g.Reply(result);
            }
        
    }
}
