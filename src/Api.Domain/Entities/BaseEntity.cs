using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        

        private DateTime? createAt;
        public DateTime? CreateAt
        {
            get { return createAt; }
            set { createAt = (value == null ? DateTime.UtcNow : value) ; }
        }
        
        public DateTime? UpdateAt { get; set; }        
    }
}