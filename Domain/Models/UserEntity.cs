using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class UserEntity
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsPass { get; set; }
        public string NickName { get; set; }
        public int? Sex { get; set; }
        public string Email { get; set; }
        public string UserAvatar { get; set; }
        public bool? IsBlack { get; set; }
        public DateTime? BlackTime { get; set; }
        public int? TopicTimes { get; set; }
        public int? FollowTimes { get; set; }
    }
}
