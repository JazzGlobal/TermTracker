using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace TermTracker
{
    public partial class MainPage : ContentPage
    {

        // Read this in via the database in final version.
        ObservableCollection<Term> terms = new ObservableCollection<Term>();
        public ObservableCollection<Term> Terms = new ObservableCollection<Term>();
        public MainPage()
        {
            terms.Add(new Term("Term 1", DateTime.Now));
            terms.Add(new Term("Term 2", DateTime.Now));
            Reload();
            InitializeComponent();
            TermList.ItemsSource = Terms;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Reload();
        }
        private async void termLvItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListView listView = (ListView)sender;
            Term selectedItem = (Term)listView.SelectedItem;
            string result = await DisplayActionSheet("View / Edit Term", "Cancel", null, new string[] { "View", "Edit" });

            // Pass the selected term's data to the next form (view or edit form).
            switch (result)
            {
                case "View":
                    Debug.WriteLine($"Viewing the {selectedItem.DisplayName.ToUpper()} term!");
                    await Navigation.PushAsync(new TermView(selectedItem));
                    break;
                case "Edit":
                    Debug.WriteLine($"Editing the {selectedItem.DisplayName.ToUpper()} term!");
                    await Navigation.PushAsync(new TermEdit(ref selectedItem));
                    break;
            }
        }

        private void OnClickAddTerm(object sender, EventArgs e)
        {
            // Add new term.
        }

        private void OnClickReload(object sender, EventArgs e)
        {
            Console.WriteLine("Reloaded Terms!");
            Reload();
        }

        private void Reload()
        {
            Terms.Clear();
            foreach (var term in terms)
            {
                Terms.Add(term);
            }
        }
    }
}