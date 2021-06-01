using System;
using System.Diagnostics;

using Xamarin.Forms;

namespace TermTracker
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermEdit : ContentPage
    {
        Term term;
        public TermEdit(Term term)
        {
            InitializeComponent();
            this.term = term;
            if (term.DisplayName.Trim() != "" && term.DisplayName != null)
            {
                termNameLabel.Text = term.DisplayName;
                termNameInput.Text = term.DisplayName;
            }
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            var picker = (DatePicker)sender;
            Debug.WriteLine(picker.Date);
        }

        void OnSubmitButtonClicked(object sender, EventArgs args)
        {
            Debug.WriteLine("Submit button clicked!");
            if (termNameInput.Text.Trim() != "" && termNameInput.Text != null)
            {
                term.DisplayName = termNameInput.Text;
                term.TermStart = termStartDateInput.Date;
            }
            // TODO: Save current term name and start date to database.
            Navigation.PopAsync();
        }
    }
}