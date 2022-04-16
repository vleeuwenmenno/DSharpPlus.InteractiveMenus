using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.InteractiveMenus;

namespace BotTest.Extensions;

public class DiscordInteractionButton : DiscordInteractionComponent{
    /// <summary>Constructs a new button with the specified options.</summary>
    /// <param name="style">The style/color of the button.</param>
    /// <param name="label">The text to display on the button, up to 80 characters. Can be left blank if <paramref name="emoji" />is set.</param>
    /// <param name="disabled">Whether this button should be initialized as being disabled. User sees a greyed out button that cannot be interacted with.</param>
    /// <param name="emoji">The emoji to add to the button. This is required if <paramref name="label" /> is empty or null.</param>
    public DiscordInteractionButton(
        ButtonStyle style,
        string label,
        bool disabled = false,
        DiscordComponentEmoji emoji = null)
    {
        this.Style = style;
        this.Label = label;
        this.Disabled = disabled;
        this.Emoji = emoji;
        this.Type = ComponentType.Button;
    }

    public DiscordComponentEmoji Emoji { get; set; }

    public bool Disabled { get; set; }

    public string Label { get; set; }

    public ComponentType Type { get; set; }

    public ButtonStyle Style { get; set; }
}