using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace TMMTMS
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string textboxValueVorname;
        private string textboxValueNachname;
        private string textboxValueHandynummer;
        private string comboboxValueAbteilung;
        private string comboboxValueBereich;
        private string comboboxValuePosition;
        private string textboxValueSeminargruppe;
        private string textboxValueHskuerzel;
        private DateTime datepickerValueGeburtstag;
        private DateTime datepickerValueEintrittsdatum;

        public Window1()
        {
            InitializeComponent();
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

        private void Button_AddMember(object sender, EventArgs e)
        {
            if(AreInputsValid())
            {
                ReadInputs();
                Teammitglied teammitglied = CreateMember();
                bool insertionIsSuccess = teammitglied.StoreMember(teammitglied);

                if (insertionIsSuccess)
                {
                    ClearInputs();
                    MessageBoxHelper.ShowSuccessPopUp("Teammitglied wurde erfolgreich in die Datenbank getragen.");
                }
                else
                {
                    MessageBoxHelper.ShowFailurePopUp("Teammitglied konnte nicht in der Datenbank gespeichert werden.");
                }                
            } 
            else
            {
                MessageBoxHelper.ShowFailurePopUp("Eingabe(n) fehlerhaft oder unvollständig.");
            }
        }

        private void ReadInputs()
        {
            this.textboxValueVorname = txtbox_vorname.Text;
            this.textboxValueNachname = txtbox_nachname.Text;
            this.textboxValueHandynummer = txtbox_handynummer.Text;
            this.textboxValueSeminargruppe = txtbox_seminargruppe.Text;
            this.textboxValueHskuerzel = txtbox_hskuerzel.Text;
            this.datepickerValueGeburtstag = datepicker_geburtstag.SelectedDate.Value;
            this.datepickerValueEintrittsdatum = datepicker_eintrittsdatum.SelectedDate.Value;
            this.comboboxValueAbteilung = InputFormHelper.GetValueFromComboBoxItem(combobox_abteilung.SelectedItem);
            this.comboboxValueBereich = InputFormHelper.GetValueFromComboBoxItem(combobox_bereich.SelectedItem);
            this.comboboxValuePosition = InputFormHelper.GetValueFromComboBoxItem(combobox_rang.SelectedItem);
        }

        private void ClearInputs()
        {
            txtbox_vorname.Clear();
            txtbox_nachname.Clear();
            txtbox_handynummer.Clear();
            combobox_abteilung.SelectedIndex = -1;
            combobox_bereich.SelectedIndex = -1;
            combobox_rang.SelectedIndex = -1;
            txtbox_seminargruppe.Clear();
            txtbox_hskuerzel.Clear();
            datepicker_geburtstag.SelectedDate = DateTime.Now;
            datepicker_eintrittsdatum.SelectedDate = DateTime.Now;
        }

        private Teammitglied CreateMember()
        {
            return new Teammitglied(this.textboxValueVorname, this.textboxValueNachname, this.textboxValueHandynummer,
                this.comboboxValuePosition, this.comboboxValueAbteilung, this.comboboxValueBereich, this.textboxValueSeminargruppe,
                this.textboxValueHskuerzel, this.datepickerValueGeburtstag, this.datepickerValueEintrittsdatum);
        }
        
        private bool AreInputsValid()
        {
            string vornameInput = txtbox_vorname.Text;
            string nachnameInput = txtbox_nachname.Text;
            string handynummerInput = txtbox_handynummer.Text;
            string seminargruppeInput = txtbox_seminargruppe.Text;
            string hskuerzelInput = txtbox_hskuerzel.Text;
            DateTime geburtstagInput = datepicker_geburtstag.SelectedDate.Value;
            DateTime eintrittsdatumInput = datepicker_eintrittsdatum.SelectedDate.Value;
            Object abteilungSelectedItem = combobox_abteilung.SelectedItem;
            Object bereichSelectedItem = combobox_bereich.SelectedItem;
            Object rangSelectedItem = combobox_rang.SelectedItem;

            //Max-Length because of Database Restrictions (see database implementation)
            if (ValidationHelper.IsStringValid(vornameInput, 25) && ValidationHelper.IsStringValid(nachnameInput, 25)
                && ValidationHelper.IsStringValid(handynummerInput, 25) && ValidationHelper.IsStringValid(seminargruppeInput, 9)
                && ValidationHelper.IsStringValid(hskuerzelInput, 8) && ValidationHelper.IsDateValid(geburtstagInput)
                && ValidationHelper.IsDateValid(eintrittsdatumInput) && ValidationHelper.IsComboBoxSelectedItemValid(abteilungSelectedItem)
                && ValidationHelper.IsComboBoxSelectedItemValid(bereichSelectedItem) && ValidationHelper.IsComboBoxSelectedItemValid(rangSelectedItem))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
