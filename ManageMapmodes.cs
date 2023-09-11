using EU4_Parse_Lib.Interfaces;
using System.Diagnostics;
using System.Text.RegularExpressions;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Triggers;
using Newtonsoft.Json.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EU4_Parse_Lib
{
    public partial class ManageMapmodes : Form
    {
        private IMapMode? _currentMapMode;
        private Province _province = new(Color.FromArgb(255, 0, 0, 0));
        private Country _country = new("AND");
        private List<ITrigger> _triggers = new();

        public ManageMapmodes()
        {
            InitializeComponent();


            MapmodesTooltip.SetToolTip(TriggerNameBox, "To edit an existing trigger choose its name.");
            TriggerScopeList.Items.AddRange(Vars.ScopeNames.ToArray<object>());
            AttributeToScopeTT.SetToolTip(TriggerAttributeBox, "List will be generated once a scope is chosen!");
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
                    ColAtributeBox.Items.Clear();
                    ColAtributeBox.Items.AddRange(Vars.ProvinceAttributeNames.ToArray<object>());
                    break;
                case "Country":
                    TriggerAttributeBox.Items.Clear();
                    TriggerAttributeBox.Items.AddRange(Vars.CountryAttributeNames.ToArray<object>());
                    ColAtributeBox.Items.Clear();
                    ColAtributeBox.Items.AddRange(Vars.CountryAttributeNames.ToArray<object>());
                    TriggerAttributeBox.Refresh();
                    ColAtributeBox.Refresh();
                    break;
                default:
                    return;
            }
        }



        private void AddTriggerButton_Click(object sender, EventArgs e)
        {
            MErrorBox.Clear();

            #region Trigger Name Validation
            if (string.IsNullOrEmpty(TriggerNameBox.Text))
            {
                MErrorBox.Text += "Enter a non whitespace Trigger Name\n";
                return;
            }

            if (_triggers.Any(trigger => trigger.Name.Equals(TriggerNameBox.Text)))
            {
                MErrorBox.Text += $"Trigger name [ {TriggerNameBox.Text} ] is already used\n";
                return;
            }

            #endregion

            #region Trigger Type Validation
            if (string.IsNullOrWhiteSpace((string)TriggerTypeBox.SelectedItem))
            {
                MErrorBox.Text += $"Trigger type [ {TriggerTypeBox.Text} ] is not valid\n";
                return;
            }
            #endregion

            #region Trigger Value Validation
            if (string.IsNullOrWhiteSpace(TriggerValueBox.Text))
            {
                MErrorBox.Text += $"Trigger value [ {TriggerValueBox.Text} ] is not valid\n";
                return;
            }
            #endregion

            #region Trigger Scope Validation
            if (string.IsNullOrWhiteSpace(TriggerScopeList.Text))
            {
                MErrorBox.Text += $"Trigger scope [ {TriggerScopeList.Text} ] is not valid\n";
                return;
            }
            #endregion

            #region Trigger Attribute Validation
            var attribute = TriggerAttributeBox.SelectedItem;
            if (string.IsNullOrWhiteSpace((string)attribute))
            {
                MErrorBox.Text += $"Trigger attribute [ {TriggerAttributeBox.Text} ] is not valid\n";
                return;
            }
            if (!Enum.TryParse(TriggerAttributeBox.Text, out Attribute _))
            {
                MErrorBox.Text += $"Trigger attribute [ {TriggerAttributeBox.Text} ] does not exist in the current scope\n";
                return;
            }
            #endregion

            #region Trigger Value Validation

            var item = MapmodeScope.SelectedItem;
            if (item == null)
            {
                MErrorBox.Text += $"Map mode scope [ {MapmodeScope.Text} ] is not valid\n";
                return;
            }
            item = item.ToString();
            switch (item)
            {
                case "Province":
                    if (!ValueTypeEqualsAttributeType())
                        return;
                    break;
                case "Country":
                    if (!ValueTypeEqualsAttributeType())
                        return;
                    break;
                default:
                    MErrorBox.Text += $"Can not validate trigger value: Invalid scope: {item}\n";
                    return;
            }

            #endregion

            // Generate Variables
            _ = Enum.TryParse(TriggerAttributeBox.Text, out Attribute attr);
            object val = TriggerValueBox.Text;
            var scope = Enum.Parse<Scope>(TriggerScopeList.SelectedItem.ToString()!);

            // Create according trigger
            switch (TriggerTypeBox.SelectedItem)
            {
                case null:
                    MErrorBox.Text += "Select a valid trigger type!\n";
                    return;
                case "MinTrigger":
                    _triggers.Add(new MinTrigger(attr, int.Parse(TriggerValueBox.Text), scope, IsNegatedCheckBox.Checked, TriggerNameBox.Text));
                    break;
                case "MaxTrigger":
                    _triggers.Add(new MaxTrigger(attr, int.Parse(TriggerValueBox.Text), scope, IsNegatedCheckBox.Checked, TriggerNameBox.Text));
                    break;
                case "EqualsTrigger":
                    _triggers.Add(new EqualsTrigger(attr, TriggerValueBox.Text, scope, IsNegatedCheckBox.Checked, TriggerNameBox.Text));
                    break;

                default:
                    MErrorBox.Text += "Select a valid trigger type!\n";
                    return;

            }
            AvailableTriggersList.Items.Clear();
            ExistingTriggersInMM.Items.Clear();
            FinalTriggersListBox.Items.Clear();
            foreach (var trigger in _triggers)
            {
                ExistingTriggersInMM.Items.Add($"[{trigger.TName}] -> [{trigger.Name}]");
                AvailableTriggersList.Items.Add(trigger);
                FinalTriggersListBox.Items.Add(trigger);
            }
        }

        private void LoadTriggerToInterface(ITrigger trigger)
        {
            TriggerNameBox.Text = trigger.Name;
            IsNegatedCheckBox.Checked = trigger.IsNegated;
            TriggerTypeBox.Text = trigger.TName;
            TriggerValueBox.Text = trigger.Value.ToString();
            var scopeIndex = ExistingTriggersInMM.FindString(trigger.Scope.ToString());
            if (scopeIndex == ListBox.NoMatches)
                return;
            TriggerScopeList.SelectedIndex = scopeIndex;
            var attrIndex = ExistingTriggersInMM.FindString(trigger.Attribute.ToString());
            if (attrIndex == ListBox.NoMatches)
                return;
            TriggerScopeList.SelectedIndex = attrIndex;
        }        
        
        private void LoadTriggerToInterface(IComplexTrigger trigger)
        {
            TriggerNameBox.Text = trigger.Name;
            IsNegatedCheckBox.Checked = trigger.IsNegated;
            TriggerTypeBox.Text = trigger.TName;
            AvailableTriggersList.Items.Clear();
            ExistingTriggersInMM.Items.Clear();
            FinalTriggersListBox.Items.Clear();
            foreach (var tri in trigger.Triggers)
            {
                ExistingTriggersInMM.Items.Add($"[{tri.TName}] -> [{tri.Name}]");
                AvailableTriggersList.Items.Add(tri);
                FinalTriggersListBox.Items.Add(tri);
            }
        }

        private bool ValueTypeEqualsAttributeType()
        {
            switch (TriggerTypeBox.Text)
            {
                case "MinTrigger":
                case "MaxTrigger":
                    if (int.TryParse(TriggerValueBox.Text, out _))
                        return true;
                    MErrorBox.Text +=
                        $"Trigger value [ {TriggerValueBox.Text} ] is not of the type needed for the selected trigger [ int ]\n";
                    return false;
                case "EqualsTrigger":
                    return true;
                case "OrTrigger":
                case "AndTrigger":
                    return true;
                default:
                    MErrorBox.Text +=
                        $"Trigger value [ {TriggerValueBox.Text} ] is not of the type needed for the selected trigger [ default.type ]\n";
                    return false;
            }
        }

        private void AvailableTriggersList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (AvailableTriggersList.SelectedIndex == -1)
                return;
            var temp = AvailableTriggersList.Items[AvailableTriggersList.SelectedIndex];
            SelectedSubTriggersList.Items.Add(temp);
        }

        private void SelectedSubTriggersList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (AvailableTriggersList.SelectedIndex == -1)
                return;
            SelectedSubTriggersList.Items.Remove(SelectedSubTriggersList.SelectedItem);
        }

        private void TriggerTypeBox_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (TriggerTypeBox.SelectedItem)
            {
                case "MinTrigger":
                case "MaxTrigger":
                case "EqualsTrigger":
                    SubTriggerGroup.Enabled = false;
                    TriggerValueBox.Enabled = true;
                    TriggerScopeList.Enabled = true;
                    TriggerAttributeBox.Enabled = true;
                    break;
                case "OrTrigger":
                case "AndTrigger":
                    SubTriggerGroup.Enabled = true;
                    TriggerValueBox.Enabled = false;
                    TriggerScopeList.Enabled = false;
                    TriggerAttributeBox.Enabled = false;
                    break;
            }
        }

        private void MapModeNameBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (MapModeNameBox.SelectedItem is null)
                return;
            if (Vars.MapModes.TryGetValue(MapModeNameBox.SelectedItem.ToString()!, out var value))
            {
                _currentMapMode = value;
            }
        }

        private void SaveMapmodeButton_Click(object sender, EventArgs e)
        {

        }

        private void AttributeToScopeTT_Popup(object sender, PopupEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MapmodeScope.Text) || string.IsNullOrWhiteSpace(TriggerScopeList.Text))
                e.Cancel = true;
        }

        private void FinalTriggersListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            FinalTriggersListBox.SelectedIndex = FinalTriggersListBox.IndexFromPoint(e.X, e.Y);
            FinalTriggersListBox.Items.Remove(FinalTriggersListBox.SelectedItem);
        }

        private void ResetListButton_Click(object sender, EventArgs e)
        {
            FinalTriggersListBox.Items.Clear();
            foreach (var trigger in _triggers)
            {
                FinalTriggersListBox.Items.Add(trigger);
            }
        }

        private void MapModeTypeSelection_Click(object sender, EventArgs e)
        {
            if (UseColorTableButton.Checked)
            {
                ColorTableBox.Enabled = true;
                GradianColorBox.Enabled = false;
                TriggerBox.Enabled = false;
            }
            if (UseGradiantButton.Checked)
            {
                ColorTableBox.Enabled = false;
                GradianColorBox.Enabled = true;
                TriggerBox.Enabled = false;
            }
            if (UseTriggerButton.Checked)
            {
                ColorTableBox.Enabled = false;
                GradianColorBox.Enabled = false;
                TriggerBox.Enabled = true;
            }
            if (UseOneColroPerValueButton.Checked)
            {
                ColorTableBox.Enabled = false;
                GradianColorBox.Enabled = false;
                TriggerBox.Enabled = false;
            }
        }

        private void DeleteTriggerButton_Click(object sender, EventArgs e)
        {
            if (ExistingTriggersInMM.SelectedIndex == -1)
                return;
            foreach (var trigger in _triggers.Where(
                         trigger => trigger.Name.Equals(ExistingTriggersInMM.SelectedItem)))
            {
                _triggers.Remove(trigger);
            }
            ExistingTriggersInMM.Items.Remove(ExistingTriggersInMM.SelectedItem);
        }

        private void TriggerScopeList_SelectedValueChanged(object sender, EventArgs e)
        {
            var item = TriggerScopeList.SelectedItem;
            if (item == null)
                return;
            item = item.ToString();
            switch (item)
            {
                case "Province":
                    Debug.WriteLine("Setting Attributes Province");
                    TriggerAttributeBox.Items.Clear();
                    TriggerAttributeBox.Items.AddRange(Vars.ProvinceAttributeNames.ToArray<object>());
                    ColAtributeBox.Items.Clear();
                    ColAtributeBox.Items.AddRange(Vars.ProvinceAttributeNames.ToArray<object>());
                    break;
                case "Country":
                    TriggerAttributeBox.Items.Clear();
                    TriggerAttributeBox.Items.AddRange(Vars.CountryAttributeNames.ToArray<object>());
                    ColAtributeBox.Items.Clear();
                    ColAtributeBox.Items.AddRange(Vars.CountryAttributeNames.ToArray<object>());
                    TriggerAttributeBox.Refresh();
                    ColAtributeBox.Refresh();
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// sets a tooltip to show the contents of the item as the box is quite small and the items quite long
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinalTriggersListBox_MouseHover(object sender, EventArgs e)
        {
            // Get the mouse position relative to the ListBox.
            var mousePosition = FinalTriggersListBox.PointToClient(MousePosition);
            var itemIndex = FinalTriggersListBox.IndexFromPoint(mousePosition);

            if (itemIndex >= 0 && itemIndex < FinalTriggersListBox.Items.Count)
            {
                FTLTooltip.SetToolTip(FinalTriggersListBox, FinalTriggersListBox.Items[itemIndex].ToString());
            }
            else
                FTLTooltip.SetToolTip(FinalTriggersListBox, null);
        }

        private void ExistingTriggersInMM_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Util.PrintListToConsole(_triggers);
            foreach (var trigger in _triggers)
            {
                Debug.WriteLine(trigger.ToString());
                LoadTriggerToInterface(trigger);
            }
            Debug.WriteLine("------------");
        }
    }
}
