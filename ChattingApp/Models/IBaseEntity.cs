﻿namespace ChattingApp.Models
{
    public abstract class IBaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
