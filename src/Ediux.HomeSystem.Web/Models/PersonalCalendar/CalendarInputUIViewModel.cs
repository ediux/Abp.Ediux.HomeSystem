using Ediux.HomeSystem.Models.PersonalCalendar;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Ediux.HomeSystem.Web.Models.PersonalCalendar
{
    public class CalendarInputUIViewModel : CalendarInputViewModel
    {
        [Required]
        [MaxLength(500)]
        [StringLength(500)]
        [DisplayName("Field:Calendar_Event_Title")]
        [DisplayOrder(10000)]
        public override string Title { get => base.Title; set => base.Title = value; }

        [Required]
        [DisplayName("Field:AllDay")]
        [DisplayOrder(10001)]
        public override bool AllDay { get => base.AllDay; set => base.AllDay = value; }

        [DataType(DataType.DateTime)]
        [DisplayName("Field:StartTime")]
        [DisplayOrder(10002)]
        public override DateTime StartTime { get => base.StartTime; set => base.StartTime = value; }

        [DataType(DataType.DateTime)]
        [DisplayName("Field:EndTime")]
        [DisplayOrder(10003)]
        public override DateTime? EndTime { get => base.EndTime; set => base.EndTime = value; }
       
        [TextArea(Rows = 4)]
        [DisplayName("Field:Description")]
        [DisplayOrder(10004)]
        public override string description { get => base.description; set => base.description = value; }

        [MaxLength(2048)]
        [HiddenInput]
        [DisplayOrder(10005)]
        public override string classNames { get => base.classNames; set => base.classNames = value; }

        [HiddenInput]
        [DisplayOrder(10006)]
        public override bool durationEditable { get => base.durationEditable; set => base.durationEditable = value; }

        [HiddenInput]
        [DisplayOrder(10007)]
        public override bool editable { get => base.editable; set => base.editable = value; }

        [HiddenInput]
        [MaxLength(256)]
        [DisplayOrder(10008)]
        public override string GroupId { get => base.GroupId; set => base.GroupId = value; }

        [MaxLength(50)]
        [HiddenInput]
        [DisplayOrder(10009)]
        public override string icon { get => base.icon; set => base.icon = value; }

        [HiddenInput]
        [DisplayOrder(10010)]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [HiddenInput]
        [DisplayOrder(10011)]
        public override bool resourceEditable { get => base.resourceEditable; set => base.resourceEditable = value; }

        [HiddenInput]
        [DisplayOrder(10012)]
        public override bool startEditable { get => base.startEditable; set => base.startEditable = value; }
    }
}
