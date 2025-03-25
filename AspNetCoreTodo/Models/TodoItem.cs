using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class TodoItem
    {
        public Guid id { get; set; }

        public bool IsDone { get; set; }

        //不可以为Null的引用类型必须明确初始化
        [Required]
        public string Title { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        //DateTimeOffset 是可以为空 可以保存时期/时间/和时区
        public DateTimeOffset? DueAt { get; set; }
    }
}
