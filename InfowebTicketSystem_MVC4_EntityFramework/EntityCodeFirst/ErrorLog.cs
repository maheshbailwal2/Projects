//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public  class ErrorLog
    {
        public decimal f_id { get; set; }
        public Nullable<System.Guid> SessionFID { get; set; }
        public string UserIP { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public System.DateTime InsertDate { get; set; }
        public string Url { get; set; }
        public string AdditionalInfo { get; set; }
    }
}