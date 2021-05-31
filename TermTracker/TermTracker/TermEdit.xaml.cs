using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
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
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            var picker = (DatePicker)sender;
            Debug.WriteLine(picker.Date);
        }

        void OnSubmitButtonClicked (object sender, EventArgs args)
        {
            Debug.WriteLine("Submit button clicked!");
            if(termNameInput.Text.Trim() != "" && termNameInput.Text != null)
            {
                term.DisplayName = termNameInput.Text;
                term.TermStart = termStartDateInput.Date;
            }
            // TODO: Save current term name and start date to database.
            Navigation.PopAsync();
        }
    }
}