using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BromoAirlines
{
    internal class Support
    {

        public static int akunId;

        public static void msi(string text)
        {
            MessageBox.Show(text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void msw(string text)
        {
            MessageBox.Show(text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void mse(string text)
        {
            MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void msq(string text)
        {
            MessageBox.Show(text, "Question", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        public static void clearField(Control control)
        {
            foreach (var clear in control.Controls)
            {
                if (clear is TextBox)
                {
                    ((TextBox)clear).Text = string.Empty;
                }

                if (clear is ComboBox)
                {
                    ((ComboBox)clear).SelectedIndex = 0;
                }

                if (clear is DateTimePicker)
                {
                    ((DateTimePicker)clear).Value = DateTime.Now;
                }

                if (clear is NumericUpDown)
                {
                    ((NumericUpDown)clear).Value = 0;
                }
            }
        }

    }
}
