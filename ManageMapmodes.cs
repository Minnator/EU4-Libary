using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using Emgu.CV.Cuda;
using Emgu.CV.Reg;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;
using EU4_Parse_Lib.MapModes;
using EU4_Parse_Lib.Triggers;

namespace EU4_Parse_Lib
{
   public partial class ManageMapmodes : Form
   {
      private IMapMode? _currentMapMode;
      private List<ITrigger> _triggers = new();
      private Dictionary<string, Color> _colorTable = new();
      private MType _type = MType.Gradient;
      private bool _overrideMapMode = false;

      public ManageMapmodes()
      {
         InitializeComponent();
         GradColorPreview.BackColor = Color.FromArgb(255, 0, 19, 86);
         TableColorPreview.BackColor = Color.FromArgb(255, 0, 155, 155);

         ColorTableBox.Enabled = false;
         GradianColorBox.Enabled = true;
         TriggerBox.Enabled = false;

         MapmodesTooltip.SetToolTip(TriggerNameBox, "To edit an existing trigger choose its name.");
         TriggerScopeList.Items.AddRange(Vars.ScopeNames.ToArray<object>());
         AttributeToScopeTT.SetToolTip(TriggerAttributeBox, "List will be generated once a scope is chosen!");
      }

      private void MapModeScope_SelectedValueChanged(object sender, EventArgs e)
      {
         var item = MapModeScope.SelectedItem;
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
               OneColorPerValueAttributeBox.Items.Clear();
               OneColorPerValueAttributeBox.Items.AddRange(Vars.ProvinceAttributeNames.ToArray<object>());
               GradAttributeBox.Items.Clear();
               GradAttributeBox.Items.AddRange(Vars.ProvinceAttributeNames.ToArray<object>());
               break;
            case "Country":
               TriggerAttributeBox.Items.Clear();
               TriggerAttributeBox.Items.AddRange(Vars.CountryAttributeNames.ToArray<object>());
               ColAtributeBox.Items.Clear();
               ColAtributeBox.Items.AddRange(Vars.CountryAttributeNames.ToArray<object>());
               OneColorPerValueAttributeBox.Items.Clear();
               OneColorPerValueAttributeBox.Items.AddRange(Vars.CountryAttributeNames.ToArray<object>());
               GradAttributeBox.Items.Clear();
               GradAttributeBox.Items.AddRange(Vars.CountryAttributeNames.ToArray<object>());
               TriggerAttributeBox.Refresh();
               ColAtributeBox.Refresh();
               break;
            default:
               return;
         }
      }

      private void RefreshMapModeNameList()
      {
         MapModeNameBox.Items.Clear();
         foreach (var mapMode in Vars.MapModes.Values)
         {
            MapModeNameBox.Items.Add(mapMode.Name);
         }
      }

      private void AddTriggerButton_Click(object sender, EventArgs e)
      {
         MErrorBox.Clear();
         SelectedSubTriggersList.Items.Clear();

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

         var item = MapModeScope.SelectedItem;
         if (item == null)
         {
            MErrorBox.Text += $"Map mode scope [ {MapModeScope.Text} ] is not valid\n";
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
               if (!int.TryParse(TriggerValueBox.Text, out var value))
               {
                  MErrorBox.Text += $"[{TriggerValueBox.Text}] is not of type [int]!";
                  return;
               }
               _triggers.Add(new MinTrigger(attr, value, scope, IsNegatedCheckBox.Checked, TriggerNameBox.Text));
               break;
            //case "MaxTrigger":
            //    _triggers.Add(new MaxTrigger(attr, int.Parse(TriggerValueBox.Text), scope, IsNegatedCheckBox.Checked, TriggerNameBox.Text));
            //    break;
            case "EqualsTrigger":
               _triggers.Add(new EqualsTrigger(attr, TriggerValueBox.Text, scope, IsNegatedCheckBox.Checked, TriggerNameBox.Text));
               break;
            case "OrTrigger":
               var tri = ItemsToTriggers();
               Debug.WriteLine(tri.Count);
               foreach (var trigger in tri)
               {
                  Debug.WriteLine(trigger.ToString());
               }
               _triggers.Add(new OrTrigger(tri, IsNegatedCheckBox.Checked, TriggerNameBox.Text));
               break;
            case "AndTrigger":
               _triggers.Add(new AndTrigger(ItemsToTriggers(), IsNegatedCheckBox.Checked, TriggerNameBox.Text));
               break;
            default:
               MErrorBox.Text += "Select a valid trigger type!\n";
               return;

         }

         UpdateTriggerLists();
      }

      private void UpdateTriggerLists()
      {

         AvailableTriggersList.Items.Clear();
         ExistingTriggersInMM.Items.Clear();
         FinalTriggersListBox.Items.Clear();
         TriggerNameBox.Items.Clear();
         foreach (var trigger in _triggers)
         {
            ExistingTriggersInMM.Items.Add($"[{trigger.TName}] -> [{trigger.Name}]");
            AvailableTriggersList.Items.Add(trigger);
            FinalTriggersListBox.Items.Add(trigger);
            TriggerNameBox.Items.Add(trigger.Name);
         }
      }

      private List<ITrigger> ItemsToTriggers()
      {
         List<ITrigger> triggers = new();
         foreach (var item in SelectedSubTriggersList.Items)
         {
            ITrigger? first = null;
            foreach (var trigger in _triggers)
            {
               if (!Equals(trigger.ToString(), item.ToString()))
                  continue;
               first = trigger;
               break;
            }
            triggers.Add(first!);
         }
         return triggers;
      }

      private void LoadMapMode(IMapMode mapMode)
      {
         switch (mapMode.Type)
         {
            case MType.Gradient:
               _type = MType.Gradient;
               MinValueBox.Text = mapMode.Min.ToString();
               MaxValueBox.Text = mapMode.Max.ToString();
               NullValueBox.Text = mapMode.Null.ToString();
               GradColorBox.Text = $"{mapMode.NullColor.R},{mapMode.NullColor.G},{mapMode.NullColor.B}";
               GradAttributeBox.SelectedItem = mapMode.Attribute;
               ColorTableBox.Enabled = false;
               GradianColorBox.Enabled = true;
               TriggerBox.Enabled = false;
               OneColorPerValueBox.Enabled = false;
               break;
            case MType.ColorTable:
               _type = MType.ColorTable;
               ColAtributeBox.SelectedItem = mapMode.Attribute;
               ColortablePreview.Items.Clear();
               foreach (var color in mapMode.ColorTable)
               {
                  var item = new ListViewItem($"[{color.Key}] -> [{color.Value.R}/{color.Value.G}/{color.Value.B}]");
                  ColortablePreview.Items.Add(item);
               }
               _colorTable = mapMode.ColorTable;
               ColValueBox.Clear();
               ColColorBox.Clear();
               ColorTableBox.Enabled = true;
               GradianColorBox.Enabled = false;
               TriggerBox.Enabled = false;
               OneColorPerValueBox.Enabled = false;
               break;
            case MType.OneColorPerValue:
               _type = MType.OneColorPerValue;
               OneColorPerValueAttributeBox.SelectedItem = mapMode.Attribute;
               ColorTableBox.Enabled = false;
               GradianColorBox.Enabled = false;
               TriggerBox.Enabled = false;
               OneColorPerValueBox.Enabled = true;
               break;
            case MType.TriggerList:
               _type = MType.TriggerList;
               TriggerNameBox.Items.Clear();
               TriggerTypeBox.SelectedItem = null;
               TriggerValueBox.Clear();
               TriggerScopeList.SelectedItem = null;
               TriggerAttributeBox.SelectedItem = null;
               IsNegatedCheckBox.Checked = false;
               AvailableTriggersList.Items.Clear();
               ExistingTriggersInMM.Items.Clear();
               FinalTriggersListBox.Items.Clear();
               foreach (var tri in mapMode.Triggers)
               {
                  ExistingTriggersInMM.Items.Add($"[{tri.TName}] -> [{tri.Name}]");
                  AvailableTriggersList.Items.Add(tri);
                  FinalTriggersListBox.Items.Add(tri);
               }
               _triggers = mapMode.Triggers;
               ColorTableBox.Enabled = false;
               GradianColorBox.Enabled = false;
               TriggerBox.Enabled = true;
               OneColorPerValueBox.Enabled = false;
               break;
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
            //case "MaxTrigger":
            //    if (int.TryParse(TriggerValueBox.Text, out _))
            //        return true;
            //    MErrorBox.Text +=
            //        $"Trigger value [ {TriggerValueBox.Text} ] is not of the type needed for the selected trigger [ int ]\n";
            //    return false;
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
            //case "MaxTrigger":
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
            LoadMapMode(value);
         }

      }

      private void SaveMapModeButton_Click(object sender, EventArgs e)
      {
         MErrorBox.Clear();
         if (!Enum.TryParse(MapModeScope.Text, out Scope scope))
         {
            MErrorBox.Text += $"[{MapModeScope.Text}] is no legal scope!";
            _overrideMapMode = false;
            return;
         }

         var validName = VerifyMapmodeName();
         if (!validName.Key)
            return;

         Attribute attr;
         switch (_type)
         {
            case MType.Gradient:
               if (!int.TryParse(MinValueBox.Text, out var min) ||
                   !int.TryParse(MaxValueBox.Text, out var max) ||
                   !int.TryParse(NullValueBox.Text, out var nul))
               {
                  MErrorBox.Text += "Either [min] [max] [null] are not a number!";
                  _overrideMapMode = false;
                  return;
               }

               var col = Util.ParseColorFromString(GradColorBox.Text);
               if (!col.Key)
               {
                  MErrorBox.Text += $"[{GradColorBox.Text}] is in wrong color format: [r/g/b] or [r,g,b]";
                  _overrideMapMode = false;
                  return;
               }

               if (!Enum.TryParse(GradAttributeBox.Text, out attr))
               {
                  MErrorBox.Text += $"[{GradAttributeBox.Text}] is no legal attribute!";
                  _overrideMapMode = false;
                  return;
               }

               _currentMapMode = new GradientMapMode(validName.Value, scope, _type, attr, OnlyLandProvincesCheckBox.Checked, min, max, nul, true, true);

               break;
            case MType.ColorTable:
               if (!Enum.TryParse(ColAtributeBox.Text, out attr))
               {
                  MErrorBox.Text += $"[{ColAtributeBox.Text}] is no legal attribute!";
                  _overrideMapMode = false;
                  return;
               }

               if (_colorTable.Count < 1)
               {
                  MErrorBox.Text += "There must be at least one entry!";
                  _overrideMapMode = false;
                  return;
               }

               _currentMapMode = new ColorTableMapMode(validName.Value, scope, _type, attr,
                   OnlyLandProvincesCheckBox.Checked, false, _colorTable, true);

               break;
            case MType.OneColorPerValue:
               if (!Enum.TryParse(OneColorPerValueAttributeBox.Text, out attr))
               {
                  MErrorBox.Text += $"[{OneColorPerValueAttributeBox.Text}] is no legal attribute!";
                  _overrideMapMode = false;
                  return;
               }
               _currentMapMode = new OneColorPerValueMapMode(validName.Value, scope, _type, attr,
                   OnlyLandProvincesCheckBox.Checked, false, true);
               Debug.WriteLine(_currentMapMode);
               break;
            case MType.TriggerList:
               if (!Enum.TryParse(TriggerAttributeBox.Text, out attr))
               {
                  MErrorBox.Text += $"[{TriggerAttributeBox.Text}] is no legal attribute!";
                  _overrideMapMode = false;
                  return;
               }
               if (FinalTriggersListBox.Items.Count < 1)
               {
                  MErrorBox.Text += "There must be at least one entry in the final trigger list!";
                  _overrideMapMode = false;
                  return;
               }
               List<ITrigger> triggers = new();
               Match match;
               foreach (var item in FinalTriggersListBox.Items)
               {
                  match = Regex.Match(item.ToString()!, @"(.*):");
                  if (!match.Success)
                     continue;
                  triggers.Add(_triggers.Find(obj => obj.Name == match.Groups[1].Value));
               }

               _currentMapMode = new TriggerListMapmode(validName.Value, scope, _type, attr,
                   OnlyLandProvincesCheckBox.Checked, triggers, true);

               break;
            default:
               MErrorBox.Text = $"Illegal mapmodetype [{_type}]!";
               return;
         }

         if (_currentMapMode == null)
         {
            MErrorBox.Text = "CRITICAL ERROR OCCURRED! Mapmode could not be created. Please verify all input values once more and try again!";
            return;
         }

         if (Vars.MapModes.ContainsKey(_currentMapMode.Name) || !_overrideMapMode)
            return;
         Vars.MapModes.Remove(_currentMapMode.Name);
         Vars.MapModes.Add(validName.Value, _currentMapMode);

         RefreshMapModeNameList();

         //if (Vars.RenderCreatedMapModes) //TODO make this a setting
         //{
         //    _currentMapMode.RenderMapMode();
         //}

         //Vars.MapMode = _currentMapMode;

         //Save all mapmodes to JSON
         SaveMapModesToJson();

         _overrideMapMode = false;
      }

      private KeyValuePair<bool, string> VerifyMapmodeName()
      {
         if (string.IsNullOrWhiteSpace(MapModeNameBox.Text))
         {
            MErrorBox.Text = $"Map mode name [{MapModeNameBox.Text}] is empty or illegal!";
            return new KeyValuePair<bool, string>(false, string.Empty);
         }
         if (Vars.MapModes.ContainsKey(MapModeNameBox.Text))
         {
            var result = MessageBox.Show("This Map Mode name is already in use. Do you want to override it with the current information entered?", "Dublicate Map Mode name", MessageBoxButtons.OKCancel);

            if (result == DialogResult.Cancel)
            {
               MErrorBox.Text = $"Map mode name [{MapModeNameBox.Text}] is already in use!";
               return new KeyValuePair<bool, string>(false, string.Empty);
            }
         }

         Debug.WriteLine(_overrideMapMode);
         _overrideMapMode = true;

         return new KeyValuePair<bool, string>(true, MapModeNameBox.Text);
      }

      private void AttributeToScopeTT_Popup(object sender, PopupEventArgs e)
      {
         if (string.IsNullOrWhiteSpace(MapModeScope.Text) || string.IsNullOrWhiteSpace(TriggerScopeList.Text))
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
            OneColorPerValueBox.Enabled = false;
         }
         if (UseGradiantButton.Checked)
         {
            ColorTableBox.Enabled = false;
            GradianColorBox.Enabled = true;
            TriggerBox.Enabled = false;
            OneColorPerValueBox.Enabled = false;
         }
         if (UseTriggerButton.Checked)
         {
            ColorTableBox.Enabled = false;
            GradianColorBox.Enabled = false;
            TriggerBox.Enabled = true;
            OneColorPerValueBox.Enabled = false;
         }
         if (UseOneColroPerValueButton.Checked)
         {
            ColorTableBox.Enabled = false;
            GradianColorBox.Enabled = false;
            TriggerBox.Enabled = false;
            OneColorPerValueBox.Enabled = true;
         }
      }

      private void DeleteTriggerButton_Click(object sender, EventArgs e)
      {
         if (ExistingTriggersInMM.SelectedIndex < 0)
            return;
         foreach (var trigger in _triggers.Where(trigger => $"[{trigger.TName}] -> [{trigger.Name}]".Equals(ExistingTriggersInMM.SelectedItem)))
         {
            _triggers.Remove(trigger);
            SelectedSubTriggersList.Items.Clear();
            break;
         }

         UpdateTriggerLists();
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
         var mousePosition = ExistingTriggersInMM.PointToClient(MousePosition);
         var itemIndex = ExistingTriggersInMM.IndexFromPoint(mousePosition);

         if (itemIndex >= 0 && itemIndex < ExistingTriggersInMM.Items.Count)
         {

            Debug.WriteLine(_triggers[itemIndex]);
            LoadTriggerToInterface(_triggers[itemIndex]);
         }
      }

      private void TriggerNameBox_TextChanged(object sender, EventArgs e)
      {
         foreach (var trigger in _triggers)
         {
            var triggerName = trigger.Name;
            var boxName = TriggerNameBox.Text;
            if (!triggerName.Equals(boxName))
               continue;
            ModifyTriggerButton.Enabled = true;
            return;
         }
         ModifyTriggerButton.Enabled = false;
      }

      private void SelectedSubTriggersList_MouseClick(object sender, MouseEventArgs e)
      {
         /*
         Debug.WriteLine(e.Button);
         if (e.Button != MouseButtons.Right)
             return;
         Debug.WriteLine("RMB pressed");
         var clickedIndex = SelectedSubTriggersList.IndexFromPoint(e.X, e.Y);
         Debug.WriteLine($"Index: {clickedIndex}");
         if (clickedIndex == ListBox.NoMatches)
             return;
         Debug.WriteLine($"Removing {SelectedSubTriggersList.Items[clickedIndex]}");
         SelectedSubTriggersList.Items.RemoveAt(clickedIndex);
         */
      }

      private void ModifyTriggerButton_Click(object sender, EventArgs e)
      {
         _triggers.RemoveAll(trigger => trigger.Name == TriggerNameBox.Text);
         AddTriggerButton_Click(sender, e);
      }

      private void ColAttributeBox_SelectedValueChanged(object sender, EventArgs e)
      {
         _colorTable.Clear();
      }

      /// <summary>
      /// verifies all entered parameters and then continuous to add it to the color table
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void AddToColorTable_Click(object sender, EventArgs e)
      {
         MErrorBox.Clear();
         var res = Util.ValidateAttribute(ColAtributeBox.Text);
         if (!res.Key)
         {
            MErrorBox.Text += res.Value;
            return;
         }

         var resCol = Util.ParseColorFromString(ColColorBox.Text);
         if (!resCol.Key)
         {
            MErrorBox.Text += $"[{ColColorBox.Text}] does not match format: [r/g/b] or [r,g,b]";
            return;
         }

         /* Deprecated more generalized method used.
         var colMatch = Regex.Match(ColColorBox.Text, @"(?<r>[0-9]{1,3})\/(?<g>[0-9]{1,3})\/(?<b>[0-9]{1,3})");
         if (!colMatch.Success)
         {
             MErrorBox.Text += $"[{ColColorBox.Text}] does not match format: [r/g/b]";
             return;
         }

         var colVal = Color.FromArgb(255, int.Parse(colMatch.Groups["r"].ToString()),
             int.Parse(colMatch.Groups["g"].ToString()), int.Parse(colMatch.Groups["b"].ToString()));
         */

         if (_colorTable.ContainsKey(ColValueBox.Text))
         {
            MErrorBox.Text += $"Value [{ColValueBox.Text}] is already defined as [{ColColorBox.Text}]\n";
            return;
         }

         _colorTable.Add(ColValueBox.Text, resCol.Value);

         ColortablePreview.Items.Clear();
         foreach (var color in _colorTable)
         {
            var item = new ListViewItem($"[{color.Key}] -> [{color.Value.R}/{color.Value.G}/{color.Value.B}]");
            ColortablePreview.Items.Add(item);
         }
      }

      private void ColorTablePreview_MouseClick(object sender, MouseEventArgs e)
      {
         if ((e.Button & MouseButtons.Right) == 0)
            return;

         var hitTestInfo = ColortablePreview.HitTest(e.Location);

         if (hitTestInfo.Item == null)
            return;

         var index = hitTestInfo.Item.Index;
         var keyToRemove = ColortablePreview.Items[index].Text;
         var match = Regex.Match(keyToRemove, @"\[(?<key>.*)\]\s-");

         if (match.Success)
         {
            ColortablePreview.Items.RemoveAt(index);
            _colorTable.Remove(match.Groups["key"].Value.Trim());
         }
         else
         {
            MErrorBox.Text += "Failed to remove the clicked item. Try restarting the application. If the issue persists, file a support ticket.\n";
         }
      }

      private void ClearMErrorLog_Click(object sender, EventArgs e)
      {
         MErrorBox.Clear();
      }

      private void UseColorTableButton_CheckedChanged(object sender, EventArgs e)
      {
         _type = MType.ColorTable;
      }

      private void UseGradientButton_CheckedChanged(object sender, EventArgs e)
      {
         _type = MType.Gradient;
      }

      private void UseOneColorPerValueButton_CheckedChanged(object sender, EventArgs e)
      {
         _type = MType.OneColorPerValue;
      }

      private void UseTriggerButton_CheckedChanged(object sender, EventArgs e)
      {
         _type = MType.TriggerList;
      }

      private static void SaveMapModesToJson()
      {
         List<IMapMode> userMapModes = new();
         List<IMapMode> genericMapModes = new();

         foreach (var mapMode in Vars.MapModes.Values)
         {
            if (mapMode.UserDefinedMapMode)
               userMapModes.Add(mapMode);
            else
               genericMapModes.Add(mapMode);
         }

         Debug.WriteLine($"Saving user map modes to: {Path.Combine(Vars.DataPath, "userMapModes.json")}");
         Debug.WriteLine($"Saving generic map modes to: {Path.Combine(Vars.DataPath, "genericMapModes.json")}");
         Saving.SaveListToJson(userMapModes, Path.Combine(Vars.DataPath, "userMapModes.json"));
         Saving.SaveListToJson(genericMapModes, Path.Combine(Vars.DataPath, "genericMapModes.json"));
      }

      private void ColColorBox_TextChanged(object sender, EventArgs e)
      {
         var kvp = Util.ParseColorFromString(ColColorBox.Text);
         if (!kvp.Key)
         {
            TableColorPreview.BackColor = Color.Transparent;
            return;
         }
         TableColorPreview.BackColor = kvp.Value;
      }

      private void GradColorBox_TextChanged(object sender, EventArgs e)
      {
         var kvp = Util.ParseColorFromString(GradColorBox.Text);
         if (!kvp.Key)
         {
            GradColorPreview.BackColor = Color.Transparent;
            return;
         }
         GradColorPreview.BackColor = kvp.Value;
      }

      private void ManageMapModes_FormClosing(object sender, FormClosingEventArgs e)
      {
         Gui.PopulateMainWindowMapModes();
      }

      private void ManageMapModes_Load(object sender, EventArgs e)
      {
         RefreshMapModeNameList();
      }

      private void TriggerValueBox_TextChanged(object sender, EventArgs e)
      {
         if (TriggerTypeBox == null || !TriggerTypeBox.SelectedItem.Equals("MinTrigger"))
            return;
         if (!int.TryParse(TriggerValueBox.Text, out _))
         {
            TriggerValueBox.Text = string.Empty;
         }
      }
   }
}
