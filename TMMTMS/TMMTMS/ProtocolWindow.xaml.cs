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

        private bool IsMaximized = false;
        /// <summary>
        /// 
        /// Maximize/Normalize on Double Left Click
        /// 
        /// </summary>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Height = 720;
                    this.Width = 1080;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
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

            if(Datenbank.StoreProtocol(meeting, protocol, protocolTopic))
            {
                //clear inputs
                MessageBoxHelper.ShowSuccessPopUp("Protokoll wurde erfolgreich in die Datenbank getragen.");
            }
            else
            {
                MessageBoxHelper.ShowFailurePopUp("Teammitglied konnte nicht in der Datenbank gespeichert werden.");
            }
            
            //save data to database

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
            return new Meeting(this.textboxValueBezeichnung, this.datepickerValueDate, meetingTime, this.textboxValueLocation);
        }
        private Protocol CreateProtocol(Meeting meeting)
        {
            DateTime currentDate = DateTime.Now;
            return new Protocol(meeting, meeting.Date, currentDate);
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
    }
}
