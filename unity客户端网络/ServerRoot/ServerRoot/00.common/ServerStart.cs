/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：shiyi
    邮箱: 1659244699@qq.com
    日期：#CreateTime#
	功能：服务器入口
*****************************************************/
public class ServerStart
{
    static void Main(string[] args)
    {
        Server_Root.Instance.Init();    
        while (true) { Server_Root.instance.Update(); }
    }
}

