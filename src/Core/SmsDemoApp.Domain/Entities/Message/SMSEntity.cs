using SmsDemoApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Domain.Entities.Message;

public class SMSEntity : BaseEntity<long>
{
    public int CustomerId { get; set; } 
    public string Text { get; set; } = string.Empty;
    public string ReciverPhoneNumber { get; set; } = string.Empty;
    public MessageDelivaryStatus Status { get; set; }

}
public enum MessageDelivaryStatus
{
    Pending,
    Success,
    Fail,
    NoMoney
}