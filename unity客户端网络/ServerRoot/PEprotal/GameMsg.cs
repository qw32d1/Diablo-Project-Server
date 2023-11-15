using PENet;
using System;

namespace PEPortal
{
    [Serializable]
    public class GameMsg:PEMsg
    {
        public ReqLoginin reloginin;
        public RspLogin rsqloginin;
        public Resname resname;
        public Rqname rsqname;
        public RsGuide rsguide;
        public RqGuide rqguide;
    }
    [Serializable]
    public class ReqLoginin
    {
        public string Act;
        public string Passwd;
    }
    [Serializable]
    public class RspLogin
    {
        public Playerdata playerdata;
    }
    [Serializable]
    public class Playerdata
    {
        public int id;
        public string name;
        public int Lv;
        public int exp; 
        public int power;
        public int coin;
        public int diamod;
        public int hp;
        public int ad;
        public int ap;
        public int addef;
        public int apdef;
        public int dodge;//闪避概率
        public int pierce;//穿透比率
        public int critical;//暴击概率
        public int task;
    }
    [Serializable]
    public class Resname
    {
        public string rsname;
    }
    [Serializable]
    public class Rqname
    {
        public string rqname;
    }
    [Serializable]
    public class RsGuide
    {
        public int id;
    }
    [Serializable]
    public class RqGuide
    {
        public int coin;
        public int exp;
    }
    public enum ErrorCode
    {
        None=0,
        AcctOnLine,
        WordPasswd,
        NameExists,
        UpdataDBError,
        AccountExist,


    }
    public enum CMD
    {

        None=0,
        //登入编号100
        RqLogin=101,
        RsLogin=102,
        RsName=103,
        RqName=104,
        Register=105,
        RegisterEnter=106,
        RsGuide=200,
        RqGuide=201,


    }
    public class IpCfg
    {
        public const string srvip = "127.0.0.1";
        public const int srvport = 17666;
    }
    
}
