//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Location.Model.LocationTables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    [DataContract]
    public partial class DevPos
    {
        [Key]
        [DataMember]
        public string DevID { get; set; }
        [DataMember]
        public float PosX { get; set; }
        [DataMember]
        public float PosY { get; set; }
        [DataMember]
        public float PosZ { get; set; }
        [DataMember]
        public float RotationX { get; set; }
        [DataMember]
        public float RotationY { get; set; }
        [DataMember]
        public float RotationZ { get; set; }
        [DataMember]
        public float ScaleX { get; set; }
        [DataMember]
        public float ScaleY { get; set; }
        [DataMember]
        public float ScaleZ { get; set; }
    }
}
