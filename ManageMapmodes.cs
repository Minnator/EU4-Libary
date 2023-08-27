using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib
{
    public partial class ManageMapmodes : Form
    {
        List<ITrigger> _triggers = new();

        public ManageMapmodes()
        {
            InitializeComponent();

            MapmodesTooltip.SetToolTip(VariableName, "Variable names MUST match Attribute names\nThe order determines the position in the equation");
            TriggerScopeList.Items.AddRange(Vars.ScopeNames.ToArray<object>());
        }

        private void MapmodeScope_SelectedValueChanged(object sender, EventArgs e)
        {
            var item = MapmodeScope.SelectedItem;
            if (item == null)
                return;
            item = item.ToString();
            switch (item)
            {
                case "Province":
                    Debug.WriteLine("Setting Attributes Province");
                    TriggerAttributeBox.Items.Clear();
                    TriggerAttributeBox.Items.AddRange(Vars.ProvinceAttributeNames.ToArray<object>());
                    break;
                case "Country":
                    TriggerAttributeBox.Items.Clear();
                    TriggerAttributeBox.Items.AddRange(Vars.CountryAttributeNames.ToArray<object>());
                    break;
                default:
                    return;
            }
        }

        private void AddTriggerButton_Click(object sender, EventArgs e)
        {
            MErrorBox.Clear();
            switch (TriggerTypeBox.SelectedItem)
            {
                case null:
                    MErrorBox.Text += "Select a valid trigger type!\n";
                    return;
                case "MinTrigger":
                    //validate rest in different method.
                    //This switch is used to create the according trigger
                    break;

                default:
                    MErrorBox.Text += "Select a valid trigger type!\n";
                    return;

            }
        }
    }
}
