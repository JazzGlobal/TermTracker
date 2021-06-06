using System;
using System.Diagnostics;
using SQLite;
using Xamarin.Forms;

namespace TermTracker
{
    public partial class TermEdit : ContentPage
    {
        Term term;
        public TermEdit(ref Term term)
        {
            InitializeComponent();
            this.term = term;
            if (term.DisplayName.Trim() != "" && term.DisplayName != null)
            {
                termNameLabel.Text = term.DisplayName;
                termNameInput.Text = term.DisplayName;
            }
            termStartDateInput.Date = term.TermStart;
            termEndDateInput.Date = term.TermEnd;
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            var picker = (DatePicker)sender;
            Debug.WriteLine(picker.Date);
        }

        void OnSubmitButtonClicked(object sender, EventArgs args)
        {
            Debug.WriteLine("Submit button clicked!");
            try
            {
                if (termStartDateInput.Date > termEndDateInput.Date)
                {
                    throw new DateSequenceInvalidException("Term Start Date must occur before Term End Date.");
                }
                if (termNameInput.Text.Trim() != "" && termNameInput.Text != null)
                {
                    term.DisplayName = termNameInput.Text;
                    term.TermStart = termStartDateInput.Date;
                    term.TermEnd = termEndDateInput.Date;
                    SQLiteConnection conn = new SQLiteConnection(MainPage.AndroidPath);
                    Term.UpdateTerm(conn, term);
                    // TODO: Save current term name and start date to database.
                    Navigation.PopAsync();
                }
            } catch (DateSequenceInvalidException dsiEx)
            {
                DisplayAlert("Invalid Date(s)", dsiEx.Message, "OK");
            }
        }
    }
}