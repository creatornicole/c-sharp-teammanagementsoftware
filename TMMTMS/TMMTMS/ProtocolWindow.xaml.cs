﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Microsoft.Office.Interop.Word;

namespace TMMTMS
{
    public partial class ProtocolWindow : System.Windows.Window /* explizite Angabe, da mehrdeutiger Verweis 
                                                                   durch Microsoft.Office.Interop.Word */
                                                                               
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

        private BackgroundWorker backgroundWorkerToStoreProtocol;
        private BackgroundWorker backgroundWorkerToGenerateWordDoc;
        private Meeting meeting;
        private Protocol protocol;
        private ProtocolTopic topic;

        public ProtocolWindow()
        {
            InitializeComponent();
            InitializeObservableListBoxes();
            InitializeBackgroundworkerToStoreProtocol();
            InitializeBackgroundworkerToGenerateWordDoc();
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

        private void InitializeBackgroundworkerToStoreProtocol()
        {
            backgroundWorkerToStoreProtocol = new BackgroundWorker();
            backgroundWorkerToStoreProtocol.DoWork += Backgroundworker_StoreProtocol;
            backgroundWorkerToStoreProtocol.RunWorkerCompleted += Backgroundworker_ClearInputs;
            backgroundWorkerToStoreProtocol.WorkerSupportsCancellation = false;
            backgroundWorkerToStoreProtocol.WorkerReportsProgress = false;
        }

        private void InitializeBackgroundworkerToGenerateWordDoc()
        {
            backgroundWorkerToGenerateWordDoc = new BackgroundWorker();
            backgroundWorkerToGenerateWordDoc.DoWork += Backgroundworker_GenerateWordDoc;
            backgroundWorkerToGenerateWordDoc.WorkerSupportsCancellation = false;
            backgroundWorkerToGenerateWordDoc.WorkerReportsProgress = false;
        }

        private void Backgroundworker_StoreProtocol(object sender, DoWorkEventArgs e)
        {
            Datenbank.StoreProtocol(this.meeting, this.protocol, this.topic);
        }

        private void Backgroundworker_GenerateWordDoc(object sender, DoWorkEventArgs e)
        {
            WordApplicationInteraction.GenerateProtocolAsWordDocument(this.protocol, this.meeting, this.topic);
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

        private bool AreInputsValid()
        {
            string bezeichnungInput = txtbox_eventbezeichnung.Text;
            string locationInput = txtbox_location.Text;
            string protokollthemaInput = txtbox_protokollthema1.Text;            
            DateTime dateInput = datepicker_eventdatum.SelectedDate.Value;
            Object timeSelectedItem = InputFormHelper.GetValueFromComboBoxItem(combobox_time.SelectedItem);

            string stichpunkt1Input = txtbox_protokollthema1_stichpunkt1.Text;
            string stichpunkt2Input = txtbox_protokollthema1_stichpunkt2.Text;
            string stichpunkt3Input = txtbox_protokollthema1_stichpunkt3.Text;
            string protokollInhalt = stichpunkt1Input + " " + stichpunkt2Input + " " + stichpunkt3Input;

            //not needed to be checked because of strict selection
            //this.selectedPresentMembers = InputFormHelper.GetSelectedListBoxItemsAsStrings(listBoxPresentMembers);
            //this.selectedAbsentMembers = InputFormHelper.GetSelectedListBoxItemsAsStrings(listBoxAbsentMembers); ;

            //Max-Length because of Database Restrictions (see database implementation)
            if (ValidationHelper.IsStringValid(bezeichnungInput, 50) && ValidationHelper.IsStringValid(locationInput, 30)
                && ValidationHelper.IsStringValid(protokollthemaInput, 30) && ValidationHelper.IsStringValid(protokollInhalt, 1080)
                && ValidationHelper.IsDateValid(dateInput) && ValidationHelper.IsComboBoxSelectedItemValid(timeSelectedItem))
            {
                return true;
            } 
            else
            {
                return false;
            }
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

        private void Backgroundworker_ClearInputs(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error == null)
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

                MessageBoxHelper.ShowSuccessPopUp("Protokolldaten wurden erfolgreich in die Datenbank getragen.");
            }
            else
            {
                MessageBoxHelper.ShowFailurePopUp("Teammitglied konnte nicht in der Datenbank gespeichert werden.");
            }
        }

        private Meeting CreateMeeting()
        {
            TimeOnly meetingTime = InputFormHelper.GetTimeOnlyFromString(this.comboboxValueTime);
            List<string> presentMembersHsKuerzel = Datenbank.GetHsKuerzelFromTeammemberNames(this.selectedPresentMembers);
            List<string> absentMembersHsKuerzel = Datenbank.GetHsKuerzelFromTeammemberNames(this.selectedAbsentMembers);

            return new Meeting(this.textboxValueBezeichnung, this.datepickerValueDate, meetingTime, this.textboxValueLocation,
                presentMembersHsKuerzel, absentMembersHsKuerzel);
        }

        private Protocol CreateProtocol()
        {
            return new Protocol(this.meeting, meeting.Date);
        }

        private ProtocolTopic CreateProtocolTopic()
        {
            List<string> content = new List<string>
            {
                this.textboxValueProtokollthemaStichpunkt1,
                this.textboxValueProtokollthemaStichpunkt2,
                this.textboxValueProtokollthemaStichpunkt3
            };
            return new ProtocolTopic(this.protocol, this.textboxValueProtokollthema, content);
        }

        private void Button_AddProtocol(object sender, EventArgs e)
        {
            if (AreInputsValid())
            {
                ReadProtocolInputData();
                this.meeting = CreateMeeting();
                this.protocol = CreateProtocol();
                this.topic = CreateProtocolTopic();

                backgroundWorkerToStoreProtocol.RunWorkerAsync();
                backgroundWorkerToGenerateWordDoc.RunWorkerAsync();
            }
            else
            {
                MessageBoxHelper.ShowFailurePopUp("Eingabe(n) fehlerhaft oder unvollständig.");
            }
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
