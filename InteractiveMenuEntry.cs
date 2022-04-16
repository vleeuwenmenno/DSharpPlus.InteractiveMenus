using BotTest.Extensions;
using DSharpPlus.EventArgs;

namespace DSharpPlus.InteractiveMenus;

public class InteractiveMenuEntry
{
    public object[] Parameters { get; set; }
    public DiscordInteractionComponent InteractionComponent { get; set; }
    public Delegate Callback { get; set; }

    public InteractiveMenuEntry(DiscordInteractionComponent discordInteractionComponent, Delegate callback, params object[] parameters)
    {
        Parameters = parameters;
        Callback = callback;
        InteractionComponent = discordInteractionComponent;
    }
}