using Ediux.HomeSystem.Models.DTOs.Files;
using Ediux.HomeSystem.Plugins.HololivePages.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions
{
    public class HoloMemberDto : AuditedEntityDto<Guid>, ITransientDependency
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

        public string SexDescription
        {
            get
            {
                switch (Sex)
                {
                    case Sex.Female:
                        return "女性";
                    case Sex.Male:
                        return "男性";
                    default:
                    case Sex.Unknow:
                        return "不公開";
                }

            }
        }

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

        public string Creator { get; set; }

        public string Modifier { get; set; }

        public Guid? MainAvatarImageId { get; set; }

        public string MainAvatarImageUrl
        {
            get {
                return MainAvatarImageId.HasValue ? $"/api/simpleupload/download/{MainAvatarImageId}" : string.Empty;
            }
        }

        public virtual ICollection<FileStoreDTO> Photos { get; set; }

        public virtual ICollection<HoloMemberEventDto> Events { get; set; }

        public virtual ICollection<YTuberVideoDto> Videos { get; set; }

        public HoloMemberDto()
        {
            Photos = new List<FileStoreDTO>();
            Events = new List<HoloMemberEventDto>();
            Videos = new List<YTuberVideoDto>();
        }
    }
}
