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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            bool inputsAreValid = ReadInputs();
            if(!inputsAreValid)
            {
                ShowFailurePopUp("Eingabe(n) fehlerhaft oder unvollständig.");
            } 
            else
            {
                Teammitglied teammitglied = CreateMember();
                bool insertionIsSuccess = teammitglied.StoreMember(teammitglied);

                if (insertionIsSuccess)
                {
                    ClearInputs();
                    ShowSuccessPopUp("Teammitglied wurde erfolgreich in die Datenbank getragen.");
                }
                else
                {
                    ShowFailurePopUp("Teammitglied konnte nicht in der Datenbank gespeichert werden.");
                }
            }
        }

        //TODO: ReadInputs return false if no input!

        private bool ReadInputs()
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
                this.textboxValueVorname = vornameInput;
                this.textboxValueNachname = nachnameInput;
                this.textboxValueHandynummer = handynummerInput;
                this.textboxValueSeminargruppe = seminargruppeInput;
                this.textboxValueHskuerzel = hskuerzelInput;
                this.datepickerValueGeburtstag = geburtstagInput;
                this.datepickerValueEintrittsdatum = eintrittsdatumInput;
                this.comboboxValueAbteilung = GetValueFromComboBox(abteilungSelectedItem);
                this.comboboxValueBereich = GetValueFromComboBox(bereichSelectedItem);
                this.comboboxValuePosition = GetValueFromComboBox(rangSelectedItem);

                return true; 
            }

            return false;
        }

        /// <summary>
        /// 
        /// vorgefertigte Methoden um Value aus ComboBox zu erhalten haben nicht funktioniert - not defined
        /// 
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        private string GetValueFromComboBox(Object selectedItem)
        {
            string comboBoxValue = "";

            if (selectedItem != null)
            {
                string selectedItemAsString = Convert.ToString(selectedItem);
                int trimIndex = selectedItemAsString.IndexOf(' ');

                comboBoxValue = selectedItemAsString.Substring(trimIndex + 1);
            }
            
            return comboBoxValue;
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
            return new Teammitglied(textboxValueVorname, textboxValueNachname, textboxValueHandynummer,
                comboboxValuePosition, comboboxValueAbteilung, comboboxValueBereich, textboxValueSeminargruppe,
                textboxValueHskuerzel, datepickerValueGeburtstag, datepickerValueEintrittsdatum);
        }

        private void ShowSuccessPopUp(string message)
        {
            MessageBox.Show(message, "Erfolgreich", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowFailurePopUp(string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        
    }
}
