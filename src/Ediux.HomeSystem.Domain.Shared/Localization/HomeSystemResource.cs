using System;
using System.Linq.Expressions;

using Volo.Abp.Localization;

namespace Ediux.HomeSystem.Localization
{
    [LocalizationResourceName("HomeSystem")]
    public class HomeSystemResource
    {
        public class Buttons
        {
            public const string Prefix = nameof(Buttons);

            public const string Delete = Prefix + ":Delete";
            public const string Add = Prefix + ":Add";
            public const string Save = Prefix + ":Save";
            public const string Insert = Prefix + ":Insert";
            public const string Update = Prefix + ":Update";
            public const string Edit = Prefix + ":Edit";
            public const string Cancel = Prefix + ":Cancel";
            public const string Add_Event = Prefix + ":Add_Event";
            public const string Uninstall = Prefix + ":UnInstall";
            public const string Install = Prefix + ":Install";
            public const string Upload = Prefix + ":Upload";
            public const string ReUpload = Prefix + ":ReUpload";
        }

        public const string GeneralError = HomeSystemDomainErrorCodes.GeneralError;
        public const string DataNotFound = HomeSystemDomainErrorCodes.DataNotFound;
        public const string SpecifyDataItemNotFound = HomeSystemDomainErrorCodes.SpecifyDataItemNotFound;
        public const string DataAlreadyExistsError = HomeSystemDomainErrorCodes.DataAlreadyExistsError;
        public const string SpecifyDataItemAlreadyExistsError = HomeSystemDomainErrorCodes.SpecifyDataItemAlreadyExistsError;
        public const string CannotBeNullOrEmpty = HomeSystemDomainErrorCodes.CannotBeNullOrEmpty;

        public class Common
        {
            public const string Prefix = nameof(Common);
            public const string PasswordBook = Prefix + ":" + nameof(PasswordBook);
            public const string DocumentsList = Prefix + ":" + nameof(DocumentsList);
            public const string HomePage = Prefix + ":" + nameof(HomePage);
            public const string SiteName = Prefix + ":" + nameof(SiteName);
            public const string Welcome = Prefix + ":" + nameof(Welcome);
            public const string LongWelcomeMessage = Prefix + ":" + nameof(LongWelcomeMessage);
            public const string PluginsManager = Prefix + ":" + nameof(PluginsManager);
            public const string SimpleUploadDescriptTemplate = Prefix + ":" + nameof(SimpleUploadDescriptTemplate);


            public class Caption
            {
                public const string Prefix = Common.Prefix + ":" + nameof(Caption);
                public const string AddRecord = Prefix + "." + nameof(AddRecord);
                public const string DeleteRecord = Prefix + "." + nameof(DeleteRecord);
                public const string EditRecord = Prefix + "." + nameof(EditRecord);
            }

            public class DTFX
            {
                public const string Prefix = Common.Prefix + ":" + nameof(DTFX);
                public const string Processing = Prefix + "." + nameof(Processing);
                public const string Search = Prefix + "." + nameof(Search);
                public const string Info = Prefix + "." + nameof(Info);
                public const string InfoEmpty = Prefix + "." + nameof(InfoEmpty);
                public const string InfoFiltered = Prefix + "." + nameof(InfoFiltered);
                public const string InfoPostFix = Prefix + "." + nameof(InfoPostFix);
                public const string LoadingRecords = Prefix + "." + nameof(LoadingRecords);
                public const string ZeroRecords = Prefix + "." + nameof(ZeroRecords);
                public const string EmptyTable = Prefix + "." + nameof(EmptyTable);
                public const string LengthMenu = Prefix + "." + nameof(LengthMenu);

                public class Paginate
                {
                    public const string Prefix = DTFX.Prefix + "." + nameof(Paginate);
                    public const string First = Prefix + "." + nameof(First);
                    public const string Previous = Prefix + "." + nameof(Previous);
                    public const string Next = Prefix + "." + nameof(Next);
                    public const string Last = Prefix + "." + nameof(Last);
                }

                public class Aria
                {
                    public const string Prefix = DTFX.Prefix + "." + nameof(Aria);
                    public const string SortAscending = Prefix + "." + nameof(SortAscending);
                    public const string SortDescending = Prefix + "." + nameof(SortDescending);
                }
            }

            public class Messages
            {
                public const string Prefix = Common.Prefix + ":" + nameof(Messages);
                public const string Success = Prefix + "." + nameof(Success);
                public const string ErrorFormat = Prefix + "." + nameof(ErrorFormat);
                public const string Response_Code_Format = Prefix + "." + nameof(Response_Code_Format);
                public const string UnknowError = Prefix + "." + nameof(UnknowError);
            }

            /// <summary>
            /// 欄位的本地化字串
            /// </summary>
            public class Fields
            {
                public const string Prefix = "Field";
                public const string Id = Prefix + ":" + nameof(Id);
                public const string GroupId = Prefix + ":" + nameof(GroupId);
                public const string Calendar_Event_Title = Prefix + ":" + nameof(Calendar_Event_Title);
                public const string StartTime = Prefix + ":" + nameof(StartTime);
                public const string EndTime = Prefix + ":" + nameof(EndTime);
                public const string AllDay = Prefix + ":" + nameof(AllDay);
                public const string Url = Prefix + ":" + nameof(Url);
                public const string StyleSheet = Prefix + ":" + nameof(StyleSheet);
                public const string Editable = Prefix + ":" + nameof(Editable);
                public const string StartEditable = Prefix + ":" + nameof(StartEditable);
                public const string DurationEditable = Prefix + ":" + nameof(DurationEditable);
                public const string Calendar_Event_Icon = Prefix + ": " + nameof(Calendar_Event_Icon);
                public const string Description = Prefix + ":" + nameof(Description);
                public const string CreationTime = Prefix + ":" + nameof(CreationTime);
                public const string CreatorId = Prefix + ":" + nameof(CreatorId);
                public const string LastModifierId = Prefix + ":" + nameof(LastModifierId);
                public const string LastModificationTime = Prefix + ":" + nameof(LastModificationTime);
                public const string SiteName = Prefix + ":" + nameof(SiteName);
                /// <summary>
                /// 登入帳號
                /// </summary>
                public const string LoginAccount = Prefix + ":" + nameof(LoginAccount);
                /// <summary>
                /// 登入密碼
                /// </summary>
                public const string Password = Prefix + ":" + nameof(Password);
                /// <summary>
                /// 網站URL
                /// </summary>
                public const string SiteURL = Prefix + ":" + nameof(SiteURL);
                /// <summary>
                /// 歷史紀錄
                /// </summary>
                public const string IsHistory = Prefix + ":" + nameof(IsHistory);
                /// <summary>
                /// 時間間隔(毫秒)
                /// </summary>
                public const string Timer_Period = Prefix + ":" + nameof(Timer_Period);

                /// <summary>
                /// 網站標語
                /// </summary>
                public const string WelcomeSlogan = Prefix + ":" + nameof(WelcomeSlogan);
            }
        }

        public class Features
        {
            public const string Prefix = nameof(Features);

            public class Files
            {
                public const string Prefix = Features.Prefix + ":" + nameof(Files);
                public const string IsAutoSaveFile = Features.Prefix + ":" + nameof(IsAutoSaveFile);
                public class DTFX
                {
                    public const string Prefix = Files.Prefix + "." + nameof(DTFX);

                    public class Columns
                    {
                        public const string Prefix = DTFX.Prefix + "." + nameof(Columns);

                        public const string Name = Prefix + "." + nameof(Name);
                        public const string ExtName = Prefix + "." + nameof(ExtName);
                        public const string Description = Prefix + "." + nameof(Description);
                        public const string Size = Prefix + "." + nameof(Size);
                        public const string OriginFullPath = Prefix + "." + nameof(OriginFullPath);
                        public const string Creator = Prefix + "." + nameof(Creator);
                        public const string CreatorDate = Prefix + "." + nameof(CreatorDate);
                        public const string Modifier = Prefix + "." + nameof(Modifier);
                        public const string ModifierDate = Prefix + "." + nameof(ModifierDate);
                        public const string ContentType = Prefix + "." + nameof(ContentType);
                        public const string UploadFiles = Prefix + "." + nameof(UploadFiles);
                    }
                }
            }

            public class ProductKeysBook
            {
                public const string Prefix = Features.Prefix + ":" + nameof(ProductKeysBook);

                public class DTFX
                {
                    public const string Prefix = ProductKeysBook.Prefix + "." + nameof(DTFX);

                    public class Columns
                    {
                        public const string Prefix = DTFX.Prefix + "." + nameof(Columns);
                        public const string ProductName = Prefix + "." + nameof(ProductName);
                        public const string ProductKey = Prefix + "." + nameof(ProductKey);
                        public const string Flag_Shared = Prefix + "." + nameof(Flag_Shared);
                        public const string ExtendProperies = Prefix + "." + nameof(ExtendProperies);
                    }
                }
            }

            public class PasswordBook
            {
                public const string Prefix = Features.Prefix + ":" + nameof(PasswordBook);

                public class DTFX
                {
                    public const string Prefix = PasswordBook.Prefix + "." + nameof(DTFX);

                    public class Columns
                    {
                        public const string Prefix = DTFX.Prefix + "." + nameof(Columns);

                        public const string SiteName = Prefix + "." + nameof(SiteName);

                        public const string LoginAccount = Prefix + "." + nameof(LoginAccount);

                        public const string Password = Prefix + "." + nameof(Password);

                        public const string SiteURL = Prefix + "." + nameof(SiteURL);

                        public const string IsHistory = Prefix + "." + nameof(IsHistory);

                        public const string ExtraProperties = Prefix + "." + nameof(ExtraProperties);
                    }
                }
            }

            public class PluginsManager
            {
                public const string Prefix = Features.Prefix + ":" + nameof(PluginsManager);

                public class DTFX
                {
                    public const string Prefix = PluginsManager.Prefix + "." + nameof(DTFX);

                    public class Columns
                    {
                        public const string Prefix = DTFX.Prefix + "." + nameof(Columns);

                        public const string Name = Prefix + "." + nameof(Name);
                        public const string Disabled = Prefix + "." + nameof(Disabled);
                        public const string AssemblyPath = Prefix + "." + nameof(AssemblyPath);
                    }
                }

                public class Buttons
                {
                    public const string Prefix = PluginsManager.Prefix + "." + nameof(Buttons);
                    public const string AddPlugins = Prefix + "." + nameof(AddPlugins);
                }
            }

            public class PersonalCalendar
            {
                public const string Prefix = Features.Prefix + ":" + nameof(PersonalCalendar);
                public class Title
                {
                    public const string Prefix = PersonalCalendar.Prefix + "." + nameof(Title);
                    public const string CreateEvent = Prefix + "." + nameof(CreateEvent);
                    public const string EditEvent = Prefix + "." + nameof(EditEvent);
                    public const string DeleteEvent = Prefix + "." + nameof(DeleteEvent);
                }
            }

            public class Dashboard
            {
                public const string Prefix = Features.Prefix + ":" + nameof(Dashboard);
                public const string Options = Prefix + "." + nameof(Options);
                public const string Options_Label_setDefaultLoad = Prefix + "." + nameof(Options_Label_setDefaultLoad);
            }

            public class MIMETypes
            {
                public const string Prefix = Features.Prefix + ":MIMETypesManager";
                public const string DefaultBinaryFile_Description = Prefix + "." + nameof(DefaultBinaryFile_Description);

                public class DTFX
                {
                    public const string Prefix = MIMETypes.Prefix + "." + nameof(DTFX);

                    public class Columns
                    {
                        public const string Prefix = DTFX.Prefix + "." + nameof(Columns);

                        public const string MIME = Prefix + "." + nameof(MIME);

                        public const string RefenceExtName = Prefix + "." + nameof(RefenceExtName);

                        public const string Description = Prefix + "." + nameof(Description);
                    }
                }
            }
        }

        public class Menu
        {
            public const string Prefix = nameof(Menu);
            public const string ProductKeysBook = Prefix + ":" + nameof(ProductKeysBook);
            public const string PluginsManager = Prefix + ":" + nameof(PluginsManager);
            public const string PasswordBook = Prefix + ":" + nameof(PasswordBook);
            public const string Home = Prefix + ":" + nameof(Home);
            public const string Docs = Prefix + ":" + nameof(Docs);
            public const string Features = Prefix + ":" + nameof(Features);
            public const string MIMETypesManager = Prefix + ":" + nameof(MIMETypesManager);
            public const string PersonalCalendar = Prefix + ":" + nameof(PersonalCalendar);
            public const string Files = Prefix + ":" + nameof(Files);
        }

        public class Widgets
        {
            public const string Prefix = nameof(Widgets);
            public const string WelcomeSloganWidget = Prefix + ":" + nameof(WelcomeSloganWidget);
            public const string ABPHelpWidget = Prefix + ":" + nameof(ABPHelpWidget);
        }

        public class Permissions
        {
            public const string Prefix = nameof(Permissions);
            public const string MIMETypesManager = Prefix + ":" + nameof(MIMETypesManager);
            public const string PersonalCalendar = Prefix + ":" + nameof(PersonalCalendar);
            public class Files
            {
                public const string Prefix = Permissions.Prefix + ":" + nameof(Files);
                public const string Execute = SubAction.Execute;
                public const string CreateNew = SubAction.CreateNew;
                public const string Lists = SubAction.Lists;
                public const string Modify = SubAction.Modify;
                public const string Delete = SubAction.Delete;
                public const string Special = SubAction.Special;
                public const string Widget = SubAction.Widget;
                public const string Options = SubAction.Options;
            }
            public class ProductKeysBook
            {
                public const string Prefix = Permissions.Prefix + ":" + nameof(ProductKeysBook);
                public const string Execute = SubAction.Execute;
                public const string CreateNew = SubAction.CreateNew;
                public const string Lists = SubAction.Lists;
                public const string Modify = SubAction.Modify;
                public const string Delete = SubAction.Delete;
                public const string Special = SubAction.Special;
                public const string Widget = SubAction.Widget;
                public const string Options = SubAction.Options;
            }

            public class PasswordBook
            {
                public const string Prefix = Permissions.Prefix + ":" + nameof(PasswordBook);
                public const string Execute = SubAction.Execute;
                public const string CreateNew = SubAction.CreateNew;
                public const string Lists = SubAction.Lists;
                public const string Modify = SubAction.Modify;
                public const string Delete = SubAction.Delete;
                public const string Special = SubAction.Special;
                public const string Widget = SubAction.Widget;
                public const string Options = SubAction.Options;
            }

            public class Docs
            {
                public const string Prefix = Permissions.Prefix + ":" + nameof(Docs);
                public const string Execute = SubAction.Execute;
                public const string CreateNew = SubAction.CreateNew;
                public const string Lists = SubAction.Lists;
                public const string Modify = SubAction.Modify;
                public const string Delete = SubAction.Delete;
                public const string Special = SubAction.Special;
                public const string Widget = SubAction.Widget;
                public const string Options = SubAction.Options;
            }

            public class Home
            {
                public const string Prefix = Permissions.Prefix + ":" + nameof(Home);
                public const string Execute = SubAction.Execute;
                public const string CreateNew = SubAction.CreateNew;
                public const string Lists = SubAction.Lists;
                public const string Modify = SubAction.Modify;
                public const string Delete = SubAction.Delete;
                public const string Special = SubAction.Special;
                public const string Widget = SubAction.Widget;
                public const string Options = SubAction.Options;
            }

            public class Settings
            {
                public const string Prefix = Permissions.Prefix + ":" + nameof(Settings);
                public const string Execute = SubAction.Execute;
                public const string Special = SubAction.Special;
            }
            public enum SubActionPermission
            {
                Execute = 0,
                Create = 1,
                Lists = 2,
                Modify = 4,
                Delete = 8,
                Special = 16
            }

            /// <summary>
            /// 子動作權限常數
            /// </summary>
            public class SubAction
            {
                public const string Prefix = Permissions.Prefix + ":" + nameof(SubAction);

                /// <summary>
                /// 可執行
                /// </summary>
                public const string Execute = Prefix + "." + nameof(Execute);
                /// <summary>
                /// 新增資料
                /// </summary>
                public const string CreateNew = Prefix + "." + nameof(CreateNew);
                /// <summary>
                /// 列舉
                /// </summary>
                public const string Lists = Prefix + "." + nameof(Lists);
                /// <summary>
                /// 修改資料
                /// </summary>
                public const string Modify = Prefix + "." + nameof(Modify);
                /// <summary>
                /// 刪除資料
                /// </summary>
                public const string Delete = Prefix + "." + nameof(Delete);
                /// <summary>
                /// 特殊權限
                /// </summary>
                public const string Special = Prefix + "." + nameof(Special);

                /// <summary>
                /// Widget權限
                /// </summary>
                public const string Widget = Prefix + "." + nameof(Widget);

                /// <summary>
                /// 設定權限
                /// </summary>
                public const string Options = Prefix + "." + nameof(Options);
            }

            public class PluginsManager
            {
                public const string Prefix = Permissions.Prefix + ":" + nameof(PluginsManager);
                public const string Execute = SubAction.Execute;
                public const string CreateNew = SubAction.CreateNew;
                public const string Lists = SubAction.Lists;
                public const string Modify = SubAction.Modify;
                public const string Delete = SubAction.Delete;
                public const string Special = SubAction.Special;
            }
        }

        public class Settings
        {
            public const string Prefix = nameof(Settings);

            public class WebSettings
            {
                public const string Prefix = Settings.Prefix + ":WebSettingsGroupComponents";
                public const string WebSite = Prefix + "." + nameof(WebSite);
                public const string WelcomeSlogan = Prefix + "." + nameof(WelcomeSlogan);
            }
            public class UserSettigns
            {
                public const string Prefix = Settings.Prefix + ":UserSettingsGroupComponents";
                public const string DashBoard_Widgets = Prefix + "." + nameof(DashBoard_Widgets);
            }

            public class FCMSettings
            {
                public const string Prefix = Settings.Prefix + ":FCMSettingsGroupComponents";

                public const string ServiceKey = Prefix + "." + nameof(ServiceKey);

                public const string ApiKey = Prefix + "." + nameof(ApiKey);

                public const string AuthDomain = Prefix + "." + nameof(AuthDomain);

                public const string ProjectId = Prefix + "." + nameof(ProjectId);

                public const string StorageBucket = Prefix + "." + nameof(StorageBucket);

                public const string MessagingSenderId = Prefix + "." + nameof(MessagingSenderId);

                public const string AppId = Prefix + "." + nameof(AppId);

                public const string MeasurementId = Prefix + "." + nameof(MeasurementId);

                public const string FCMVersion = Prefix + "." + nameof(FCMVersion);

                public const string VAPIdKey = Prefix + "." + nameof(VAPIdKey);
            }

            public class BatchSettings
            {
                public const string Prefix = Settings.Prefix + ":BatchSettingsGroupComponents";
                public const string Timer_Period = Prefix + "." + nameof(Timer_Period);
            }
        }
    }
}