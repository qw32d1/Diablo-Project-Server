/****************************************************
    文件：#SCRIPTNAME#.cs
	作者：shiyi
    邮箱: 1659244699@qq.com
    日期：#CreateTime#
	功能：导航模块
*****************************************************/


using ServerRoot._03Cache;

namespace ServerRoot._02_System.GuideSvc
{
    public  class GuideSvs
    {

        private  static GuideSvs instance;
        public static GuideSvs Instance
        {
            get
            {
                if (instance == null) { instance = new GuideSvs(); }
                return instance;
            }

        }
        public CacheSvc Casvc;
        public void Init()
        {
            Casvc = CacheSvc.Instance;

        }
    }
}
