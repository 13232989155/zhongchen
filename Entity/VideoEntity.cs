using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{

    ///<summary>
    ///视频
    ///</summary>
    [SugarTable("t_video")]
    public partial class VideoEntity
    {
        public VideoEntity()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>         
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int videoId { get; set; }

        /// <summary>
        /// Desc:视频标题
        /// Default:
        /// Nullable:False
        /// </summary>           

        [Required(ErrorMessage = "不能为空")]
        public string title { get; set; }

        /// <summary>
        /// Desc:分类名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string typeName { get; set; }


        /// <summary>
        /// Desc:封面图片
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string coverImage { get; set; }

        /// <summary>
        /// Desc:视频地址
        /// Default:
        /// Nullable:False
        /// </summary>          
        [Required(ErrorMessage = "不能为空")]
        [Url(ErrorMessage = "格式不正确")]
        public string URL { get; set; }

        /// <summary>
        /// Desc:关联的产品或食谱ID
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public string bill { get; set; }

        /// <summary>
        /// Desc:创建者ID
        /// Default:-1
        /// Nullable:False
        /// </summary>           
        public int adminId { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:DateTime.Now
        /// Nullable:False
        /// </summary>           
        public DateTime createDate { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:DateTime.Now
        /// Nullable:False
        /// </summary>           
        public DateTime modifyDate { get; set; }
    }
}
