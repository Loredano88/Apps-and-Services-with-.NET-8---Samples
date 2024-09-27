namespace Northwind.Maui.Blazor.Client.Views;

public partial class EmployeesPage : ContentPage
{
	public EmployeesPage()
	{
		InitializeComponent();
	}

    private async void CopyToClipboardButton_Clicked(object sender, EventArgs e)
    {
        await Clipboard.Default.SetTextAsync(NotesTextBox.Text);
    }

    private async void PasteFromClipboardButton_Clicked(object sender, EventArgs e)
    {
        if (Clipboard.HasText)
        {
            NotesTextBox.Text = await Clipboard.Default.GetTextAsync();
        }
    }
}