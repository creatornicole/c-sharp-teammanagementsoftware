using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TMMTMS
{
    public partial class ProtocolWindow : Window
    {
        private string textboxValueBezeichnung;
        private string textboxValueLocation;
        private DateTime datepickerValueDate;
        private string comboboxValueTime;
        private List<string> selectedPresentMembers;
        private List<string> selectedAbsentMembers;
        private string textboxValueProtokollthema;
        private string textboxValueProtokollthemaStichpunkt1;
        private string textboxValueProtokollthemaStichpunkt2;
        private string textboxValueProtokollthemaStichpunkt3;

        private ObservableCollection<string> presentMemberCollection;
        private ObservableCollection<string> absentMemberCollection;

        public ProtocolWindow()
        {
            InitializeComponent();
            InitializeObservableListBoxes();

        }

        /// <summary>
        /// 
        /// Enables Drag Move with Left Mouse Down
        /// 
        /// </summary>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        private void AddMembersToListBox(ObservableCollection<string> collection)
        {
            List<string> members = Datenbank.GetTeammemberNames();
            foreach (string member in members)
            {
                collection.Add(member);
            }
        }

        private void Button_TogglePresentMemberSelection(object sender, EventArgs e)
        {
            //Toggle Functionality
        }

        private void Button_ToggleAbsentMemberSelection(object sender, EventArgs e)
        {
            //Toggle Functionality
        }

        private void Button_AddProtocol(object sender, EventArgs e)
        {
            ReadProtocolInputData();
            Meeting meeting = CreateMeeting();
            Protocol protocol = CreateProtocol(meeting);
            ProtocolTopic protocolTopic = CreateProtocolTopic(protocol);
            
            if (Datenbank.StoreProtocol(meeting, protocol, protocolTopic))
            {
                ClearInputs();
                MessageBoxHelper.ShowSuccessPopUp("Protokolldaten wurden erfolgreich in die Datenbank getragen.");
            }
            else
            {
                MessageBoxHelper.ShowFailurePopUp("Teammitglied konnte nicht in der Datenbank gespeichert werden.");
            }

            //Ask for Export to Word-Doc (und Pdf?)

        }

        private void ReadProtocolInputData()
        {
            this.textboxValueBezeichnung = txtbox_eventbezeichnung.Text;
            this.textboxValueLocation = txtbox_location.Text;
            this.datepickerValueDate = datepicker_eventdatum.SelectedDate.Value;
            this.comboboxValueTime = InputFormHelper.GetValueFromComboBoxItem(combobox_time.SelectedItem);
            this.selectedPresentMembers = InputFormHelper.GetSelectedListBoxItemsAsStrings(listBoxPresentMembers);
            this.selectedAbsentMembers = InputFormHelper.GetSelectedListBoxItemsAsStrings(listBoxAbsentMembers); ;
            this.textboxValueProtokollthema = txtbox_protokollthema1.Text;
            this.textboxValueProtokollthemaStichpunkt1 = txtbox_protokollthema1_stichpunkt1.Text;
            this.textboxValueProtokollthemaStichpunkt2 = txtbox_protokollthema1_stichpunkt2.Text;
            this.textboxValueProtokollthemaStichpunkt3 = txtbox_protokollthema1_stichpunkt3.Text;
        }

        private void ClearInputs()
        {
            txtbox_eventbezeichnung.Clear();
            txtbox_location.Clear();
            txtbox_protokollthema1.Clear();
            txtbox_protokollthema1_stichpunkt1.Clear();
            txtbox_protokollthema1_stichpunkt2.Clear();
            txtbox_protokollthema1_stichpunkt3.Clear();
            datepicker_eventdatum.SelectedDate = DateTime.Now;
            combobox_time.SelectedIndex = -1;
            listBoxPresentMembers.SelectedIndex = -1;
            listBoxAbsentMembers.SelectedIndex = -1;          
        }

        private void InitializeObservableListBoxes()
        {
            //OberservableCollection for ListBox Collection to Add Items from Database
            presentMemberCollection = new ObservableCollection<string>();
            //Bind the ObservableCollection to the ListBox ItemsSource
            listBoxPresentMembers.ItemsSource = presentMemberCollection;

            absentMemberCollection = new ObservableCollection<string>();
            listBoxAbsentMembers.ItemsSource = absentMemberCollection;

            AddMembersToListBox(presentMemberCollection);
            AddMembersToListBox(absentMemberCollection);
        }

        private Meeting CreateMeeting()
        {
            TimeOnly meetingTime = InputFormHelper.GetTimeOnlyFromString(this.comboboxValueTime);
            List<string> presentMembersHsKuerzel = Datenbank.GetHsKuerzelFromTeammemberNames(this.selectedPresentMembers);
            List<string> absentMembersHsKuerzel = Datenbank.GetHsKuerzelFromTeammemberNames(this.selectedAbsentMembers);

            return new Meeting(this.textboxValueBezeichnung, this.datepickerValueDate, meetingTime, this.textboxValueLocation,
                presentMembersHsKuerzel, absentMembersHsKuerzel);
        }

        private Protocol CreateProtocol(Meeting meeting)
        {
            return new Protocol(meeting, meeting.Date);
        }

        private ProtocolTopic CreateProtocolTopic(Protocol protocol)
        {
            List<string> content = new List<string>
            {
                this.textboxValueProtokollthemaStichpunkt1,
                this.textboxValueProtokollthemaStichpunkt2,
                this.textboxValueProtokollthemaStichpunkt3
            };
            return new ProtocolTopic(protocol, this.textboxValueProtokollthema, content);
        }

        private void Button_SwitchToTeammemberListPage(object sender, EventArgs e)
        {
            SwitchWindowHelper.SwitchToTeammemberListPage();
            this.Close();
        }

        private void Button_SwitchToAttendanceListPage(object sender, EventArgs e)
        {
            SwitchWindowHelper.SwitchToAttendanceListPage();
            this.Close();
        }
    }
}
