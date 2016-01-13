using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Demo.Core.Converter.ConvertJson
{
    /// <summary>
    /// 未标记 DataContract,DataMember 类解析成JsonString形式
    /// </summary>
    public class JsonPropertiesResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.Ignored = false;
            return property;
        }

        protected override List<MemberInfo> GetSerializableMembers(Type objecType)
        {
            return objecType.GetProperties().Where(x => x.GetIndexParameters().Length == 0).Cast<MemberInfo>().ToList();
        }
    }
}
