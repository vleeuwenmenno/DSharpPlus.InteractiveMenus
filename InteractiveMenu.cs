using BotTest.Extensions;
using DSharpPlus.Entities;

namespace DSharpPlus.InteractiveMenus;

public class InteractiveMenu
{
    private DiscordMessageBuilder _builder;
    internal Dictionary<string, InteractiveMenuEntry> actions;
    
    public DiscordMessageBuilder Register(InteractiveMenuProvider provider)
    {
        provider._menus.Add(this);
        return _builder;
    }

    public InteractiveMenu()
    {
        _builder = new DiscordMessageBuilder();
        actions = new Dictionary<string, InteractiveMenuEntry>();
    }
    
    public InteractiveMenu AddMenuEntries(IEnumerable<InteractiveMenuEntry> buttonsAndActions)
    {
        var components = new List<DiscordComponent>();
        
        foreach (InteractiveMenuEntry entry in buttonsAndActions)
        {
            var id = Guid.NewGuid().ToString();
            
            actions.Add(id, entry);
            
            switch (entry.InteractionComponent)
            {
                case DiscordInteractionButton button:
                {
                    components.Add(new DiscordButtonComponent(button.Style, id, button.Label, button.Disabled, button.Emoji));
                    break;
                }
                
                case DiscordInteractionSelect select:
                {
                    components.Add(new DiscordSelectComponent(id, @select.Placeholder, @select.Options, @select.Disabled, @select.MinimumSelectedValues, @select.MaximumSelectedValues));
                    break;
                }
            }
        }

        _builder.AddComponents(components);
        return this;
    }
}