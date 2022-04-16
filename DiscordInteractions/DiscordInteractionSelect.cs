using DSharpPlus.Entities;

namespace DSharpPlus.InteractiveMenus;

public class DiscordInteractionSelect : DiscordInteractionComponent
{
    public DiscordInteractionSelect(
        string placeholder,
        IEnumerable<DiscordSelectComponentOption> options,
        bool disabled = false,
        int minOptions = 1,
        int maxOptions = 1)
    {
        this.Options = (IReadOnlyList<DiscordSelectComponentOption>) options.ToArray<DiscordSelectComponentOption>();
        this.Placeholder = placeholder;
        this.Disabled = disabled;
        this.MinimumSelectedValues = minOptions;
        this.MaximumSelectedValues = maxOptions;
    }

    public IReadOnlyList<DiscordSelectComponentOption> Options { get; set; }
    public string Placeholder { get; set; }
    public bool Disabled { get; set; }
    public int MinimumSelectedValues { get; set; }
    public int MaximumSelectedValues { get; set; }
}