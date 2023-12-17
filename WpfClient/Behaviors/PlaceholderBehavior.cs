using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

public class PlaceholderBehavior : Behavior<TextBox>
{
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(
            nameof(Placeholder), typeof(string), typeof(PlaceholderBehavior), new PropertyMetadata(default(string)));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        SetPlaceholder();
        AssociatedObject.GotFocus += (sender, e) => ClearPlaceholder();
        AssociatedObject.LostFocus += (sender, e) => SetPlaceholder();
    }

    private void ClearPlaceholder()
    {
        if (AssociatedObject.Text == Placeholder)
        {
            AssociatedObject.Text = "";
        }
    }

    private void SetPlaceholder()
    {
        if (string.IsNullOrEmpty(AssociatedObject.Text))
        {
            AssociatedObject.Text = Placeholder;
        }
    }
}