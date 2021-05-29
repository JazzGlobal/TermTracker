using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TermTracker
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Term> terms = new ObservableCollection<Term>();
        public ObservableCollection<Term> Terms  { get { return terms; } }
        public MainPage()
        {
            terms.Add(new Term("Term 1", DateTime.Now));
            terms.Add(new Term("Term 2", DateTime.Now));
            InitializeComponent();
            TermList.ItemsSource = terms; 
        }

        private void termLvItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            var selectedItem = listView.SelectedItem;
            Console.WriteLine($"Tapped: {((Term) selectedItem).DisplayName}");
        }
        private void OnClickReload(object sender, EventArgs e)
        {
            Console.WriteLine("Reloaded Terms!");
        }
    }
    public class Term
    {
        public string DisplayName {get; set;}
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; }

        public string FormattedTermTitle { get {return $"{DisplayName}\n{TermStart.ToString("dd-MM-yyyy")} - {TermEnd.ToString("dd-MM-yyyy")}"; } }

        public Term(string displayName, DateTime termStart)
        {
            DisplayName = displayName;
            TermStart = termStart;
            TermEnd = termStart.AddMonths(6);
        }
    }
}
