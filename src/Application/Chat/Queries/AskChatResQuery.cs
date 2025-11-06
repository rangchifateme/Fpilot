using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chat.Queries;

public class AskChatResQuery { 
    public string Answer { get; set; } = "";

    public List<string> Citations { get; set; } = new(); 

    public string SessionId { get; set; } = ""; 
}