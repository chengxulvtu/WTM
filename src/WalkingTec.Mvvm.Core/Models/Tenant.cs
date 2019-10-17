using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WalkingTec.Mvvm.Core
{
    public class Tenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 租户数据库连接字符串
        /// </summary>
        [Required]
        public string ConnectionString { get; set; }


        /// <summary>
        /// 租户主机名称，支持多个，以 ; 分割
        /// </summary>
        [Required]
        public string HostName { get; set; }

        /// <summary>
        /// 租户Logo
        /// </summary>
        public string Logo { get; set; }
    }
}
