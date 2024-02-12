using HoangHH.DesignPattern;
using Sigtrap.Relays;

namespace HoangHH.Manager
{
    public class EventGlobalManager : Singleton<EventGlobalManager>
    {
        public Relay OnCharacterUnTouch { get; } = new();
    }
}
