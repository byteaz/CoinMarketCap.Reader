using Core.Business;
using Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Timers;
using System.Json;
using System.IO;
using System.Data;
using Newtonsoft.Json;


namespace CoinMarketCap.Reader
{
    class Program
    {
        public static DBUtil DB = new DBUtil();
        public static string content = string.Empty;
        public static string subcontent = string.Empty;


        static void Main(string[] args)
        {
            // Sleep
            Console.WriteLine(DateTime.Now.ToString() + " - " + "Starting ...");

            DateTime datetime = DateTime.Now;

            if (args.Length == 0)
            {
                ReadCoinData(datetime);
                ReadCoinMarketData(datetime);
            }
            else
            {
                if (args[0]=="CoinData")
                {
                    ReadCoinData(datetime);
                }
                else
                if (args[0]=="CoinMarketData")
                {
                    ReadCoinMarketData(datetime);
                }
            }
        }

        public static void ReadCoinData(DateTime datetime)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - " + "Starting to read main data...");

            string content = String.Empty;
            string subcontent = String.Empty;

            content = Operation.GetURLContent("https://api.coinmarketcap.com/v1/ticker/?limit=2000");

            if (!String.IsNullOrEmpty(content))
            {

                try
                {
                    DataTable table = JsonConvert.DeserializeObject<DataTable>(content);

                    foreach (DataRow row in table.Rows)
                    {
                        string sql = "SELECT id_coin " +
                                     "FROM coin " +
                                     "WHERE symbol=@symbol";

                        SqlParameter[] parameter = new SqlParameter[1];
                        parameter[0] = new SqlParameter();
                        parameter[0].ParameterName = "@symbol";
                        parameter[0].Value = row["symbol"].ToString();

                        DataTable DT = DB.GetData(sql, parameter);

                        if (DT.Rows.Count == 0)
                        {
                            InsertCoinRow(row, datetime);
                            InsertNotification("New coin", row["name"].ToString() + " - " + row["symbol"].ToString() + " / price : " + row["price_usd"].ToString(), datetime);
                        }
                        else
                        {
                            UpdateCoinRow(row, datetime);
                        }

                        // Insert data

                        InsertCoinDataRow(row, datetime);
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(DateTime.Now.ToString() + " - " + "Pasing error...");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now.ToString() + " - " + "Downloading error...");
                Console.ResetColor();
            }
        }

        public static void ReadCoinMarketData(DateTime datetime)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - " + "Starting to Coin Market Data...");
            string content = String.Empty;
            string subcontent = String.Empty;

            string sql = "SELECT id_coin,id,name,symbol " +
                         "FROM coin";

            DataTable DT = DB.GetData(sql, null);

            int count = 0;

            foreach (DataRow row in DT.Rows)
            {
                count++;

                System.Threading.Thread.Sleep(3000);
                Console.WriteLine(DateTime.Now.ToString() + " - " + "Reading " +" " +count.ToString()+ ". " + row["name"].ToString() + " ...");

                content = Operation.GetURLContent("https://coinmarketcap.com/currencies/"+row["id"].ToString()+"/");

                content = content.Replace(@"\n", String.Empty);
                content = content.Replace(System.Environment.NewLine, String.Empty);
                content = content.Replace("  ", String.Empty);
                content = content.Replace("  ", String.Empty);
                content = content.Replace("  ", String.Empty);
                content = content.Replace("  ", String.Empty);
                content = content.Replace("  ", String.Empty);

                subcontent = Operation.ExtractTagData(content, "<tbody>", "", "</tbody>").Trim();
                subcontent = subcontent.Replace("</td>", ":");
                subcontent = subcontent.Replace("</tr>", "#");
                subcontent = Operation.ExtractTextFromHTML(subcontent).Trim();
                subcontent = subcontent.Replace("\n", String.Empty);
                subcontent = subcontent.Replace("\n", String.Empty);


                foreach (string line in subcontent.Split('#'))
                {
                    if (line.Trim().Length > 0)
                    {
                        string[] cell = line.Trim().Split(':');

                        int order = Convert.ToInt32(cell[0].Trim());
                        string source = cell[1].Trim();
                        string pair = cell[2].Trim();
                        double volume_usd_24h = double.Parse(cell[3].Replace("*",String.Empty).Replace("%", String.Empty).Replace("$", String.Empty).Trim(), System.Globalization.CultureInfo.InvariantCulture);
                        float price = float.Parse(cell[4].Replace("*", String.Empty).Replace("%", String.Empty).Replace("$", String.Empty).Trim(), System.Globalization.CultureInfo.InvariantCulture);
                        float volume = float.Parse(cell[5].Replace("*", String.Empty).Replace("%", String.Empty).Replace("$", String.Empty).Trim(), System.Globalization.CultureInfo.InvariantCulture);


                        string sql01 = "SELECT id_coin_market " +
                                       "FROM coin_market " +
                                       "WHERE id=@id AND source=@source AND pair=@pair";

                        SqlParameter[] parameter01 = new SqlParameter[3];
                        parameter01[0] = new SqlParameter();
                        parameter01[0].ParameterName = "@id";
                        parameter01[0].Value = row["id"].ToString();
                        parameter01[1] = new SqlParameter();
                        parameter01[1].ParameterName = "@source";
                        parameter01[1].Value = source;
                        parameter01[2] = new SqlParameter();
                        parameter01[2].ParameterName = "@pair";
                        parameter01[2].Value = pair;

                        DataTable DT01 = DB.GetData(sql01, parameter01);

                        if (DT01.Rows.Count == 0)
                        {
                            InsertCoinMarket(row["id"].ToString(), order, source, pair, volume_usd_24h, price, volume, datetime);

                            InsertNotification("New coin market", row["name"].ToString() + " - " + row["symbol"].ToString() + " / market : " + source + " / pair : " + pair + " / price : " + price.ToString(), datetime);
                        }
                        else
                        {
                            UpdateCoinMarket(Convert.ToInt32(DT01.Rows[0]["id_coin_market"]),row["id"].ToString(), order, source, pair, volume_usd_24h, price, volume, datetime);
                        }

                    }
                }

                // Insert coin market data
                InsertCoinMarketCount(row["id"].ToString(), datetime);
            }
        }

        public static void InsertCoinRow(DataRow row, DateTime datetime)
        {
            string sql = "INSERT INTO coin " +
                                   "(insert_date,id,name,symbol,rank,price_usd,price_btc,volume_usd_24h,market_cap_usd,available_supply,total_supply,percent_change_1h,percent_change_24h,percent_change_7d,last_updated,is_active,is_deleted)" +
                                   "VALUES " +
                                   "(@insert_date,@id,@name,@symbol,@rank,@price_usd,@price_btc,@volume_usd_24h,@market_cap_usd,@available_supply,@total_supply,@percent_change_1h,@percent_change_24h,@percent_change_7d,@last_updated,1,0)";

            SqlParameter[] parameter = new SqlParameter[15];
            parameter[0] = new SqlParameter();
            parameter[0].ParameterName = "@insert_date";
            parameter[0].Value = datetime;
            parameter[1] = new SqlParameter();
            parameter[1].ParameterName = "@id";
            parameter[1].Value = row["id"].ToString();
            parameter[2] = new SqlParameter();
            parameter[2].ParameterName = "@name";
            parameter[2].Value = row["name"].ToString();
            parameter[3] = new SqlParameter();
            parameter[3].ParameterName = "@symbol";
            parameter[3].Value = row["symbol"].ToString();
            parameter[4] = new SqlParameter();
            parameter[4].ParameterName = "@rank";
            parameter[4].Value = Convert.ToInt32(row["rank"]);
            parameter[5] = new SqlParameter();
            parameter[5].ParameterName = "@price_usd";
            parameter[5].Value = row["price_usd"];
            parameter[6] = new SqlParameter();
            parameter[6].ParameterName = "@price_btc";
            parameter[6].Value = row["price_btc"];
            parameter[7] = new SqlParameter();
            parameter[7].ParameterName = "@volume_usd_24h";
            parameter[7].Value = row["24h_volume_usd"];
            parameter[8] = new SqlParameter();
            parameter[8].ParameterName = "@market_cap_usd";
            parameter[8].Value = row["market_cap_usd"];
            parameter[9] = new SqlParameter();
            parameter[9].ParameterName = "@available_supply";
            parameter[9].Value = row["available_supply"];
            parameter[10] = new SqlParameter();
            parameter[10].ParameterName = "@total_supply";
            parameter[10].Value = row["total_supply"];
            parameter[11] = new SqlParameter();
            parameter[11].ParameterName = "@percent_change_1h";
            parameter[11].Value = row["percent_change_1h"];
            parameter[12] = new SqlParameter();
            parameter[12].ParameterName = "@percent_change_24h";
            parameter[12].Value = row["percent_change_24h"];
            parameter[13] = new SqlParameter();
            parameter[13].ParameterName = "@percent_change_7d";
            parameter[13].Value = row["percent_change_7d"];
            parameter[14] = new SqlParameter();
            parameter[14].ParameterName = "@last_updated";
            parameter[14].Value = row["last_updated"];
            try
            {
                DB.InsertUpdate(sql, parameter);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(DateTime.Now.ToString() + " - " + row["name"].ToString() + " : " + "Inserted...");
                Console.ResetColor();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now.ToString() + " - " + row["name"].ToString() + " : " + "Inserted error...");
                Console.ResetColor();
            }
        }

        public static void UpdateCoinRow(DataRow row, DateTime datetime)
        {
            string sql = "UPDATE coin " +
                                     "SET " +
                                     "insert_date=@insert_date," +
                                     "name=@name," +
                                     "symbol=@symbol," +
                                     "rank=@rank," +
                                     "price_usd=@price_usd," +
                                     "price_btc=@price_btc," +
                                     "volume_usd_24h=@volume_usd_24h," +
                                     "market_cap_usd=@market_cap_usd," +
                                     "available_supply=@available_supply," +
                                     "total_supply=@total_supply," +
                                     "percent_change_1h=@percent_change_1h," +
                                     "percent_change_24h=@percent_change_24h," +
                                     "percent_change_7d=@percent_change_7d," +
                                     "last_updated=@last_updated " +
                                     "WHERE " +
                                     "id=@id";

            SqlParameter[] parameter = new SqlParameter[15];
            parameter[0] = new SqlParameter();
            parameter[0].ParameterName = "@insert_date";
            parameter[0].Value = datetime;
            parameter[1] = new SqlParameter();
            parameter[1].ParameterName = "@id";
            parameter[1].Value = row["id"].ToString();
            parameter[2] = new SqlParameter();
            parameter[2].ParameterName = "@name";
            parameter[2].Value = row["name"].ToString();
            parameter[3] = new SqlParameter();
            parameter[3].ParameterName = "@symbol";
            parameter[3].Value = row["symbol"].ToString();
            parameter[4] = new SqlParameter();
            parameter[4].ParameterName = "@rank";
            parameter[4].Value = Convert.ToInt32(row["rank"]);
            parameter[5] = new SqlParameter();
            parameter[5].ParameterName = "@price_usd";
            parameter[5].Value = row["price_usd"];
            parameter[6] = new SqlParameter();
            parameter[6].ParameterName = "@price_btc";
            parameter[6].Value = row["price_btc"];
            parameter[7] = new SqlParameter();
            parameter[7].ParameterName = "@volume_usd_24h";
            parameter[7].Value = row["24h_volume_usd"];
            parameter[8] = new SqlParameter();
            parameter[8].ParameterName = "@market_cap_usd";
            parameter[8].Value = row["market_cap_usd"];
            parameter[9] = new SqlParameter();
            parameter[9].ParameterName = "@available_supply";
            parameter[9].Value = row["available_supply"];
            parameter[10] = new SqlParameter();
            parameter[10].ParameterName = "@total_supply";
            parameter[10].Value = row["total_supply"];
            parameter[11] = new SqlParameter();
            parameter[11].ParameterName = "@percent_change_1h";
            parameter[11].Value = row["percent_change_1h"];
            parameter[12] = new SqlParameter();
            parameter[12].ParameterName = "@percent_change_24h";
            parameter[12].Value = row["percent_change_24h"];
            parameter[13] = new SqlParameter();
            parameter[13].ParameterName = "@percent_change_7d";
            parameter[13].Value = row["percent_change_7d"];
            parameter[14] = new SqlParameter();
            parameter[14].ParameterName = "@last_updated";
            parameter[14].Value = row["last_updated"];
            try
            {
                DB.InsertUpdate(sql, parameter);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(DateTime.Now.ToString() + " - " + row["name"].ToString() + " : " + "Updated...");
                Console.ResetColor();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now.ToString() + " - " + row["name"].ToString() + " : " + "Updateing error...");
                Console.ResetColor();
            }
        }

        public static void InsertNotification(string title, string message, DateTime datetime)
        {
            string sql = "INSERT INTO notification " +
                         "(title,message,insert_date)" +
                         "VALUES " +
                         "(@title,@message,@insert_date)";

            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter();
            parameter[0].ParameterName = "@title";
            parameter[0].Value = title;
            parameter[1] = new SqlParameter();
            parameter[1].ParameterName = "@message";
            parameter[1].Value = message;
            parameter[2] = new SqlParameter();
            parameter[2].ParameterName = "@insert_date";
            parameter[2].Value = datetime;

            DB.InsertUpdate(sql, parameter);
        }

        public static void InsertCoinDataRow(DataRow row, DateTime datetime)
        {
            string sql = "INSERT INTO data " +
                         "(insert_date,id,rank,price_usd,volume_usd_24h,market_cap_usd,available_supply,total_supply,percent_change_1h,percent_change_24h,percent_change_7d)" +
                         "VALUES " +
                         "(@insert_date,@id,@rank,@price_usd,@volume_usd_24h,@market_cap_usd,@available_supply,@total_supply,@percent_change_1h,@percent_change_24h,@percent_change_7d)";

            SqlParameter[] parameter = new SqlParameter[12];
            parameter[0] = new SqlParameter();
            parameter[0].ParameterName = "@insert_date";
            parameter[0].Value = datetime;
            parameter[1] = new SqlParameter();
            parameter[1].ParameterName = "@id";
            parameter[1].Value = row["id"].ToString();
            parameter[2] = new SqlParameter();
            parameter[2].ParameterName = "@rank";
            parameter[2].Value = Convert.ToInt32(row["rank"]);
            parameter[3] = new SqlParameter();
            parameter[3].ParameterName = "@price_usd";
            parameter[3].Value = row["price_usd"];
            parameter[4] = new SqlParameter();
            parameter[4].ParameterName = "@price_btc";
            parameter[4].Value = row["price_btc"];
            parameter[5] = new SqlParameter();
            parameter[5].ParameterName = "@volume_usd_24h";
            parameter[5].Value = row["24h_volume_usd"];
            parameter[6] = new SqlParameter();
            parameter[6].ParameterName = "@market_cap_usd";
            parameter[6].Value = row["market_cap_usd"];
            parameter[7] = new SqlParameter();
            parameter[7].ParameterName = "@available_supply";
            parameter[7].Value = row["available_supply"];
            parameter[8] = new SqlParameter();
            parameter[8].ParameterName = "@total_supply";
            parameter[8].Value = row["total_supply"];
            parameter[9] = new SqlParameter();
            parameter[9].ParameterName = "@percent_change_1h";
            parameter[9].Value = row["percent_change_1h"];
            parameter[10] = new SqlParameter();
            parameter[10].ParameterName = "@percent_change_24h";
            parameter[10].Value = row["percent_change_24h"];
            parameter[11] = new SqlParameter();
            parameter[11].ParameterName = "@percent_change_7d";
            parameter[11].Value = row["percent_change_7d"];

            try
            {
                DB.InsertUpdate(sql, parameter);
            }
            catch
            {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine(DateTime.Now.ToString() + " - " + row["name"].ToString() + " : " + "Data inserted error...");
               Console.ResetColor();
           }
        }

        public static void InsertCoinMarket(string id, int order, string source, string pair, double volume_usd_24h, float price, float volume, DateTime datetime)
        {
            string sql = "INSERT INTO coin_market " +
                         "(id,[order],source,pair,volume_usd_24h,price,volume,insert_date)" +
                         "VALUES " +
                         "(@id,@order,@source,@pair,@volume_usd_24h,@price,@volume,@insert_date)";

            SqlParameter[] parameter = new SqlParameter[8];
            parameter[0] = new SqlParameter();
            parameter[0].ParameterName = "@id";
            parameter[0].Value = id;
            parameter[1] = new SqlParameter();
            parameter[1].ParameterName = "@order";
            parameter[1].Value = order;
            parameter[2] = new SqlParameter();
            parameter[2].ParameterName = "@source";
            parameter[2].Value = source;
            parameter[3] = new SqlParameter();
            parameter[3].ParameterName = "@pair";
            parameter[3].Value = pair;
            parameter[4] = new SqlParameter();
            parameter[4].ParameterName = "@volume_usd_24h";
            parameter[4].Value = volume_usd_24h;
            parameter[5] = new SqlParameter();
            parameter[5].ParameterName = "@price";
            parameter[5].Value = price;
            parameter[6] = new SqlParameter();
            parameter[6].ParameterName = "@volume";
            parameter[6].Value = volume;
            parameter[7] = new SqlParameter();
            parameter[7].ParameterName = "@insert_date";
            parameter[7].Value = datetime;

            try
            {
                DB.InsertUpdate(sql, parameter);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now.ToString() + " - " + "Coin market inserting error...");
                Console.ResetColor();
            }
        }

        public static void InsertCoinMarketCount(string id, DateTime datetime)
        {
            string sql = "INSERT INTO coin_market_count " +
                         "(id,market_count,market_count_usd,insert_date)" +
                         "VALUES " +
                         "(@id,"+
                         "(SELECT COUNT(id_coin_market) FROM coin_market WHERE coin_market.id=@id)," +
                         "(SELECT COUNT(id_coin_market) FROM coin_market WHERE coin_market.id=@id AND LOWER(coin_market.pair) LIKE '%usd%' AND LOWER(coin_market.pair) NOT LIKE '%usdt%')," +
                         "@insert_date)";

            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter();
            parameter[0].ParameterName = "@id";
            parameter[0].Value = id;
            parameter[1] = new SqlParameter();
            parameter[1].ParameterName = "@insert_date";
            parameter[1].Value = datetime;

            try
            {
                DB.InsertUpdate(sql, parameter);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now.ToString() + " - " + "Coin market count inserting error...");
                Console.ResetColor();
            }
        }

        public static void UpdateCoinMarket(int id_coin_market, string id, int order, string source, string pair, double volume_usd_24h, float price, float volume, DateTime datetime)
        {
            string sql = "UPDATE coin_market " +
                         "SET [order]=@order,volume_usd_24h=@volume_usd_24h,price=@price,volume=@volume " +
                         "WHERE "+
                         "id_coin_market=@id_coin_market";

            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter();
            parameter[0].ParameterName = "@order";
            parameter[0].Value = order;
            parameter[1] = new SqlParameter();
            parameter[1].ParameterName = "@volume_usd_24h";
            parameter[1].Value = volume_usd_24h;
            parameter[2] = new SqlParameter();
            parameter[2].ParameterName = "@price";
            parameter[2].Value = price;
            parameter[3] = new SqlParameter();
            parameter[3].ParameterName = "@volume";
            parameter[3].Value = volume;
            parameter[4] = new SqlParameter();
            parameter[4].ParameterName = "@id_coin_market";
            parameter[4].Value = id_coin_market;

            try
            {
                DB.InsertUpdate(sql, parameter);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now.ToString() + " - " + "Coin market updating error...");
                Console.ResetColor();
            }
        }

    }
}
