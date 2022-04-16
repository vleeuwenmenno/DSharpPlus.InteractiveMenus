using System.Reflection;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace DSharpPlus.InteractiveMenus;

public class InteractiveMenuProvider
{
    private readonly DiscordClient discord;

    internal List<InteractiveMenu> _menus;

    public InteractiveMenuProvider(DiscordClient discord)
    {
        this.discord = discord;
        _menus = new List<InteractiveMenu>();
    }

    public void HandleCallback(ComponentInteractionCreateEventArgs eventArgs)
    {
        if (!_menus.Any(x => x.actions.Any(y => y.Key == eventArgs.Id)))
            return;

        InvokeAction(eventArgs);
    }

    private void InvokeAction(ComponentInteractionCreateEventArgs eventArgs)
    {
        var menuEntry = _menus.SingleOrDefault(x => x.actions.Any(y => y.Key == eventArgs.Id))?.actions.Single(x => x.Key == eventArgs.Id).Value;
        var parameters = new object[menuEntry.Callback.Method.GetParameters().Length];
        
        if (menuEntry == null)
            throw new Exception("Attempted to invoke unknown action!");

        parameters = FindParameter(menuEntry.Callback.Method.GetParameters(), typeof(ComponentInteractionCreateEventArgs), eventArgs, parameters);
        parameters = FindParameter(menuEntry.Callback.Method.GetParameters(), typeof(DiscordSelectComponentOption), GetOptionFromEvent(eventArgs), parameters);
        
        // Add dynamic parameters and remove any null objects.
        parameters = parameters.Concat(menuEntry.Parameters).ToArray();
        parameters = parameters.Where(x => x != null).ToArray();

        // Try invoking with parameters otherwise invoke without any parameters.
        try
        {
            menuEntry.Callback.DynamicInvoke(parameters.ToArray());
        }
        catch (ArgumentException e)
        {
            menuEntry.Callback.DynamicInvoke();
        }
    }

    private DiscordSelectComponentOption? GetOptionFromEvent(ComponentInteractionCreateEventArgs eventArgs)
    {
        if (eventArgs.Values.Length == 0)
            return null;
        
        var menuAction = _menus.Select(x => x.actions.SingleOrDefault(y => y.Key == eventArgs.Id)).First().Value;

        if (menuAction.InteractionComponent is not DiscordInteractionSelect interactionComponent)
            throw new Exception("Attempted to fetch option from button?");

        return interactionComponent.Options.SingleOrDefault(x => x.Value == eventArgs.Values.First());
    }

    private static object[] FindParameter(ParameterInfo[] parameterInfos, Type type, object parameterData, object[] parameters)
    {
        var parameter = parameterInfos.SingleOrDefault(x => x.ParameterType == type);
        
        if (parameter != null)
            parameters[parameterInfos.ToList().IndexOf(parameter)] = parameterData;

        return parameters;
    }
}