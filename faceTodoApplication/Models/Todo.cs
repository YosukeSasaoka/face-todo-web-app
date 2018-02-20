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
        [DisplayName("URL")]
        public string URL { get; set; }
    }
}
