using Ediux.HomeSystem.Plugins.HololivePages.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ediux.HomeSystem.Plugins.HololivePages.Data
{
    /// <summary>
    /// 成員清單
    /// </summary>
    public class Members : AuditedEntity<Guid>
    {
        /// <summary>
        /// 名字(英文)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 姓氏(英文)
        /// </summary>
        public string SureName { get; set; }
        /// <summary>
        /// 中間名(英文)
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// 日本名(藝名)
        /// </summary>
        public string JanpaneseName { get; set; }

        /// <summary>
        /// 中文姓名(藝名)
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 角色年齡
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 出道日期
        /// </summary>
        public DateTime DebutDate { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 角色簡述
        /// </summary>
        public string RoleDescription { get; set; }

        /// <summary>
        /// Live 2D 設計者
        /// </summary>
        public string Live2DDesigner { get; set; }

        /// <summary>
        /// Live 3D 設計者
        /// </summary>
        public string Live3DDesigner { get; set; }

        /// <summary>
        /// 頻道特色
        /// </summary>
        public string ChannelFeatures { get; set; }

        /// <summary>
        /// 角色中之人專長
        /// </summary>
        public string VTOperatorExpertise { get; set; }

        /// <summary>
        /// 站長評論
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 訂閱數
        /// </summary>
        public int Subscriptions { get; set; }

        public Guid DepartmentsId { get; set; }


        public virtual Departments Department { get; set; }

        /// <summary>
        /// 事件簿
        /// </summary>
        public virtual ICollection<MemberEvents> MemberEvents { get; set; }

        /// <summary>
        /// 相簿/創作集
        /// </summary>
        public virtual ICollection<PhotosRefence> Photos { get; set; }

        /// <summary>
        /// 直播影片
        /// </summary>
        public virtual ICollection<YTuberVideoRefence> YouTuberVideos { get; set; }
        public Members()
        {
            Department = new Departments();
            MemberEvents = new HashSet<MemberEvents>();
            Photos = new HashSet<PhotosRefence>();
            YouTuberVideos = new HashSet<YTuberVideoRefence>();

        }



    }
}
