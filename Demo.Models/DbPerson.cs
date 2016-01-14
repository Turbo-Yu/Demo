using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Demo.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Models
{
    [DataContract]
    [BsonIgnoreExtraElements]
    [Table("Persons")]
    public class DbPerson
    {
        /// <summary>
        /// _id
        /// </summary>
        [DataMember]
        [BsonId]
        public String ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [DataMember]
        public Int32 Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public Genders Gender { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        [DataMember]
        public DateTime CreateAt { get; set; }
    }
}
