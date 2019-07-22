using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PublisherService
{
    public interface IPublish
    {
        string SendMessage(string msg);
    }
}
