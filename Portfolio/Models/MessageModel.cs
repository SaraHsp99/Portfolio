using Portfolio.Core.Entities.ClassBases;

namespace Portfolio.Web.Models;
public class MessageModel
{
    public Result Result { get; set; }
    public MessageModel()
    {
        Result = new Result();
    }
}

