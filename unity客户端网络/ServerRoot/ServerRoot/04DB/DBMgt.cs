/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：shiyi
    邮箱: 1659244699@qq.com
    日期：#CreateTime#
	功能：数据库
*****************************************************/


using MySql.Data.MySqlClient;
using PEPortal;


namespace ServerRoot._04DB
{
    public class DBMgt
    {
        public MySqlConnection Connection { get; set; }
        private static DBMgt instance;
        public static DBMgt Instance
        {
            get { if (instance == null) instance = new DBMgt(); return instance; }
        }
        public void Init()
        {
            Connection = new MySqlConnection("server=127.0.0.1; user=root;password=;database=gamedatabase;port=3306;Charset=utf8;");
            Connection.Open();
            PECommon.log("连接数据库");
        }

        public Playerdata QuaryPlayerData(string acct, string password)
        {
            bool isnew = true;
            Playerdata playerdata = null;
            MySqlDataReader reader = null;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("select *from  playerdata where acct=@acct and password=@password", Connection))
                {
                    cmd.Parameters.AddWithValue("acct", acct);
                    cmd.Parameters.AddWithValue("password", password);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        isnew = false;
                        playerdata = new Playerdata();
                        playerdata.id = reader.GetInt32("id");
                        playerdata.name = reader.GetString("name");
                        playerdata.exp = reader.GetInt32("exp");
                        playerdata.power = reader.GetInt32("power");
                        playerdata.coin = reader.GetInt32("coin");
                        playerdata.Lv = reader.GetInt32("lv");
                        playerdata.diamod = reader.GetInt32("diamod");
                        playerdata.hp = reader.GetInt32("hp");
                        playerdata.ad = reader.GetInt32("ad");
                        playerdata.ap = reader.GetInt32("ap");
                        playerdata.addef = reader.GetInt32("addef");
                        playerdata.apdef = reader.GetInt32("apdef");
                        playerdata.dodge = reader.GetInt32("dodge");
                        playerdata.pierce = reader.GetInt32("pierce");
                        playerdata.critical = reader.GetInt32("critical");
                        playerdata.task = reader.GetInt32("task");

                    }
                }

            }

            catch
            { PECommon.log("数据库账号错误"); }
            if (isnew == true)
            {
                playerdata = null;
            }
            return playerdata;
        }
        public Playerdata Registerisnew(Playerdata playerdata, string acct, string password)
        {
            playerdata = null; bool IsNext = false;
            MySqlCommand cmd = null;
            MySqlDataReader r = null;

            using (cmd = new MySqlCommand("select *from playerdata where acct=@acct and password=@password", Connection))
            {
                cmd.Parameters.AddWithValue("acct", acct);
                cmd.Parameters.AddWithValue("password", password);
                r = cmd.ExecuteReader();
                if (r.Read() == false)
                {
                    IsNext = true;
                    playerdata = new Playerdata();
                    playerdata.name = "";
                    playerdata.exp = 0;
                    playerdata.power = 150;
                    playerdata.coin = 5000;
                    playerdata.Lv = 1;
                    playerdata.diamod = 500;
                    playerdata.hp = 2000;
                    playerdata.ad = 275;
                    playerdata.ap = 265;
                    playerdata.addef = 67;
                    playerdata.apdef = 43;
                    playerdata.dodge = 7;
                    playerdata.pierce = 5;
                    playerdata.critical = 2;
                    playerdata.task = 1001;


                }

            }
            if (IsNext)
            {
                playerdata.id = insertnewacctdata(acct, password, playerdata);
            }


            return playerdata;


        }
        public int insertnewacctdata(string acct, string password, Playerdata data)
        {

            using (MySqlCommand cmd = new MySqlCommand("insert into playerdata (id,name,lv,exp,power,coin,diamod,acct,password,hp,ad,ap,addef,apdef,dodge,pierce,critical,task) " +
                  " values (@id,@name,@lv,@exp,@power,@coin,@diamod,@acct,@password,@hp,@ad,@ap,@addef,@apdef,@dodge,@pierce,@critical,@task);", Connection))
            {
                cmd.Parameters.AddWithValue("id", null);
                cmd.Parameters.AddWithValue("name", data.name);
                cmd.Parameters.AddWithValue("lv", data.Lv);
                cmd.Parameters.AddWithValue("exp", data.exp);
                cmd.Parameters.AddWithValue("power", data.power);
                cmd.Parameters.AddWithValue("coin", data.coin);
                cmd.Parameters.AddWithValue("diamod", data.diamod);
                cmd.Parameters.AddWithValue("acct", acct);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Parameters.AddWithValue("hp", data.hp);
                cmd.Parameters.AddWithValue("ad", data.ad);
                cmd.Parameters.AddWithValue("ap", data.ap);
                cmd.Parameters.AddWithValue("addef", data.addef);
                cmd.Parameters.AddWithValue("apdef", data.apdef);
                cmd.Parameters.AddWithValue("dodge", data.dodge);
                cmd.Parameters.AddWithValue("pierce", data.pierce);
                cmd.Parameters.AddWithValue("critical", data.critical);
                cmd.Parameters.AddWithValue("task", data.task);

                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }



        }
        public bool QuaryTheSameName(string name)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("select *from playerdata where name=@name;", Connection))
                {

                    cmd.Parameters.AddWithValue("name", name);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() != false)
                    {


                        return true;
                    }
                    else { return false; }
                }

            }
            catch
            {
                PECommon.log("数据库查询名字失败");
                return false;
            }





        }
        public bool UpdatePlayerdata(int id, Playerdata data)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("update playerdata set name=@name where id=@id;", Connection))
                {
                    cmd.Parameters.AddWithValue("name", data.name);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public ReqLoginin GetAct_and_Password(int id)
        {
            ReqLoginin req= null;
            MySqlDataReader reader = null;
            using(MySqlCommand cmd = new MySqlCommand("select *from playerdata where id=@id", Connection))
            {
                cmd.Parameters.AddWithValue ("id", id);
                reader= cmd.ExecuteReader();
                if(reader.Read() != false)
                {
                    req = new ReqLoginin();
                    req.Act = reader.GetString("acct");
                    req.Passwd = reader.GetString("Password");
                    return req;

                }
                else
                {
                    return null;
                }
            }
        }


    }
}
