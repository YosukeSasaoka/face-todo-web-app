using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace faceTodoApplication.Models
{
    public class Todo
    {
        public int ID { get; set; }
        [DisplayName("名前")]
        public string Name { get; set; }
        [DisplayName("Mail")]
        public string Mail { get; set; }
        [DisplayName("パスワード")]
        public string Password { get; set; }
        [DisplayName("GoogleToken")]
        public string GoogleToken { get; set; }
        [DisplayName("TodoTitle")]
        public string TodoTitle { get; set; }
        [DisplayName("PersonID")]
        public string PersonId { get; set; }
    }
}
