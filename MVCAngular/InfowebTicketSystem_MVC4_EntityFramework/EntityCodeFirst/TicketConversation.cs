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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TicketConversation : BaseEntity
    {
        public System.Guid ID { get; set; }

        public System.Guid TicketID { get; set; }
        [ForeignKey("TicketID")]
        public virtual Ticket Ticket { get; set; }

        public string Message { get; set; }
      
        [StringLength(1000)]
        public string Attachment { get; set; }


        public bool Staff { get; set; }

    }
}
