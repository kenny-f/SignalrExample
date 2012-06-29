using log4net.signalr;
using log4net.SignalR;
using Machine.Specifications;
using ConsoleApplication1;

namespace Tests
{
    [Subject("Type Extensions")]
    public class GetHubNameFromTypeSpecs
    {
        It shouldbe = () => typeof (SignalrConsoleAppenderHub).GetHubName().ShouldEqual("signalrConsoleAppenderHub");
        It should = () => typeof (Chat).GetHubName().ShouldEqual("chat");

    }
}