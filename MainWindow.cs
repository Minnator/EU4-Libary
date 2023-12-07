using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.MapModes;
using Timer = System.Windows.Forms.Timer;

namespace EU4_Parse_Lib
{
   public partial class MainWindow : Form
   {
      private const int MoveAmount = 50;
      public Rectangle DisplayRect;
      public RectangleF DisplayRectF;
      private int _maxXOffset;
      private int _maxYOffset;

      private bool _isMouseOverPictureBox;
      private readonly Timer _cooldownTimer = new();
      private int _lastHoveredProvince = 1;

      public MainWindow()
      {
         Vars.MainWindow = this;
         Vars.MainWindow.Visible = false;
         InitializeComponent();

         var loadingScreen = Gui.ShowForm<LoadingScreen>();
         Vars.LoadingForm = loadingScreen;
      }

      public void INIT()
      {
         //Center Map on Europe
         CalculateMaxOffsets();
         ResetDisplayRect();
         UpdateDisplayedImage();

         //Load custom or default values
         Loading.LoadUserData();
         Loading.LoadJSONData();

         // Initialize the cooldown timer
         _cooldownTimer.Interval = 400; // Set the interval to 400 milliseconds
         _cooldownTimer.Tick += CooldownTimer_Tick!;
         //Gui.RenderBorders(Color.FromArgb(255, 0, 0, 0));
         GpuGui.Start(Color.Black);
         //Gui.DrawBorderAroundRegions();
         Loading.LoadWithStopWatch("Random Colors", Loading.FillRandomColorsList);
         Loading.WriteDebugFiles();

         //Gui.UpdateBorder();

         Vars.MapModes.Add("Default Map Mode", new DefaultMapMode("Default Map Mode"));
         Vars.MapModes.Add("Test", new GradientMapMode("Test", Scope.Province, MType.Gradient, Attribute.Id, true, 1, 1234, 1, true, false));

         Gui.PopulateMainWindowMapModes();

         Vars.DebugMapWithBorders = new Bitmap(Vars.Map!);
         Vars.MainWindow!.Visible = true;
         Vars.MainWindow = Gui.ShowForm<MainWindow>();

         if (Vars.MapModes.TryGetValue("Default Map Mode", out var mapMode))
         {
            Vars.MapMode = mapMode;
         }
      }

      private void CalculateMaxOffsets()
      {
         if (Vars.Map == null)
            return;
         _maxXOffset = Vars.Map.Width - Map.Width;
         _maxYOffset = Vars.Map.Height - Map.Height;
      }

      private void ResetDisplayRect()
      {
         if (Vars.Map == null)
            return;
         var centerX = (Vars.Map.Width - Map.Width + 400) / 2;
         var centerY = (Vars.Map.Height - Map.Height - 1000) / 2;

         DisplayRect = new Rectangle(centerX, centerY, Map.Width, Map.Height);
         DisplayRectF = new RectangleF(centerX, centerY, Map.Width, Map.Height);
      }

      public void UpdateDisplayedImage()
      {
         if (Vars.Map == null)
            return;

         Map.Image?.Dispose(); // Dispose of the previous image

         Map.Image = Vars.Map.Clone(DisplayRect, Vars.Map.PixelFormat);

         //Map.Image = Vars.Map.Clone(DisplayRectF, Vars.Map.PixelFormat);

      }

      public void MoveBitmap(int dx, int dy)
      {
         var newX = DisplayRect.X + dx;
         var newY = DisplayRect.Y + dy;

         // Ensure the new position stays within bounds
         newX = Math.Max(0, Math.Min(_maxXOffset, newX));
         newY = Math.Max(0, Math.Min(_maxYOffset, newY));

         DisplayRect.X = newX;
         DisplayRect.Y = newY;

         UpdateDisplayedImage();

      }

      #region Map Movement via buttons

      private void RightButton_Click(object sender, EventArgs e)
      {
         MoveBitmap(MoveAmount * StepsizeMove.Value, 0);
      }

      private void DownButton_Click(object sender, EventArgs e)
      {
         MoveBitmap(0, MoveAmount * StepsizeMove.Value);
      }

      private void LeftButton_Click(object sender, EventArgs e)
      {
         MoveBitmap(-MoveAmount * StepsizeMove.Value, 0);
      }

      private void UpButton_Click(object sender, EventArgs e)
      {
         MoveBitmap(0, -MoveAmount * StepsizeMove.Value);
      }

      #endregion

      private void zoomTrackBar_ValueChanged(object sender, EventArgs e)
      {
      }

      private void Map_MouseClick(object sender, MouseEventArgs e)
      {
         if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right || Vars.BorderlessMap == null)
            return;
         var x = e.X + DisplayRect.X;
         var y = e.Y + DisplayRect.Y;
         //Verify that it is in bounds
         if (x < 0 || x >= Vars.BorderlessMap.Width || y < 0 || y >= Vars.BorderlessMap.Height)
            return;
         //Get Color an verify there is an entry for it
         var color = Vars.BorderlessMap.GetPixel(x, y);
         if (!Vars.ColorIds.ContainsKey(color))
            return;

         var currentProvince = Vars.Provinces[Vars.ColorIds[color]];
         switch (e.Button)
         {
            case MouseButtons.Left when ModifierKeys == Keys.Control:
               {
                  switch (Vars.MapSelectionMode)
                  {
                     case SelectionMode.Province:
                        if (Vars.SelectedProvinces.Contains(currentProvince))
                        {
                           Gui.RenderSelection(currentProvince, Color.FromArgb(255, 0, 0, 0));
                           Vars.SelectedProvinces.Clear();
                           return;
                        }
                        Vars.SelectedProvinces.Add(currentProvince);
                        Gui.RenderSelection(currentProvince, Color.FromArgb(255, 255, 255, 255));
                        break;
                     case SelectionMode.Area:
                        var area = Vars.Areas[currentProvince.Area];
                        if (Vars.SelectedProvinces.Contains(currentProvince))
                        {
                           var sb = new StringBuilder();
                           foreach (var prov in area.Provinces)
                           {
                              sb.Append($"{prov} ");
                           }
                           foreach (var areaProvince in area.Provinces)
                           {
                              Gui.RenderSelection(Vars.Provinces[areaProvince], Color.FromArgb(255, 0, 0, 0));
                              Vars.SelectedProvinces.Remove(Vars.Provinces[areaProvince]);
                           }
                           return;
                        }

                        foreach (var areaProvince in area.Provinces)
                        {
                           Vars.SelectedProvinces.Add(Vars.Provinces[areaProvince]);
                        }
                        Util.AddCollectionToSelectedProvinces(Vars.SelectedProvinces);
                        break;
                  }
                  break;
               }
            case MouseButtons.Left:
               {
                  if (Vars.MapMode!.Name.Equals("Default Map Mode"))
                  {
                     foreach (var pro in Vars.SelectedProvinces)
                        Gui.RenderSelection(pro, Color.FromArgb(255, 0, 0, 0));
                  }
                  else
                  {
                     Gui.UpdateProvinceBorder(ref Vars.SelectedProvinces); //Unselect all provinces
                  }

                  switch (Vars.MapSelectionMode)
                  {
                     case SelectionMode.Province:
                        if (Vars.SelectedProvinces.Count == 1 && Vars.SelectedProvinces[0].Equals(currentProvince))
                        {
                           Vars.SelectedProvinces.Clear();
                           return;
                        }
                        Vars.SelectedProvinces.Clear();
                        Gui.RenderSelection(currentProvince, Color.FromArgb(255, 255, 255, 255));
                        Vars.SelectedProvinces.Add(currentProvince);
                        break;

                     case SelectionMode.Area:
                        var area = Vars.Areas[currentProvince.Area];
                        //return if the same area is clicked again
                        foreach (var pro in Vars.SelectedProvinces)
                        {
                           Gui.RenderSelection(pro, Color.FromArgb(255, 0, 0, 0));
                        }
                        if (Vars.SelectedProvinces.Contains(currentProvince))
                        {
                           Vars.SelectedProvinces.Clear();
                           return;
                        }

                        Gui.SelectProvinceCollection(area.Provinces);
                        break;
                  }

                  break;
               }
         }

         //I can't find the memory leak here
         GC.Collect();
      }

      private void Map_MouseHover(object sender, EventArgs e)
      {
         if (sender is not PictureBox pictureBox)
            return;
         var clientMouseLocation = pictureBox.PointToClient(Cursor.Position);

         GenerateMouseTooltip(new Point(clientMouseLocation.X, clientMouseLocation.Y));
      }


      private void Map_MouseEnter(object sender, EventArgs e)
      {
         _isMouseOverPictureBox = true;
      }

      private void Map_MouseLeave(object sender, EventArgs e)
      {
         _isMouseOverPictureBox = false;
      }

      private void Map_MouseMove(object sender, MouseEventArgs e)
      {
         if (!_isMouseOverPictureBox)
            return;

         GenerateMouseTooltip(new Point(e.X, e.Y));

         var cursorColor = Vars.BorderlessMap!.GetPixel(e.X + DisplayRect.X, e.Y + DisplayRect.Y);
         /*
         if (cursorColor == Vars.HoverCursorProvinceColor)
            return;
         if (!Vars.ColorIds.TryGetValue(cursorColor, out var col))
            return;
         var p = Vars.Provinces[Vars.ColorIds[cursorColor]];
         if (Vars.SelectedProvinces.Contains(p))
            return;

         //Debug.WriteLine($"Found Province {col}");
         if (Vars.ColorIds.TryGetValue(Vars.HoverCursorProvinceColor, out var id) && !Vars.SelectedProvinces.Contains(Vars.Provinces[id]))
         {
            if (Vars.MapMode!.Name.Equals("Default Map Mode"))
            {
               Gui.RenderSelection(Vars.Provinces[id], Color.FromArgb(255, 0, 0, 0));
            }
            else
            {
               List<Province> pList = new() { Vars.Provinces[id] };
               Gui.UpdateProvinceBorder(ref pList);
            }
         }
         Gui.RenderSelection(p, Color.Aqua);
         Vars.HoverCursorProvinceColor = cursorColor;
         */

         //New Highlighting
         //Get Id and check if it is already highlighted
         if (!Vars.ColorIds.TryGetValue(cursorColor, out var hoverId) || hoverId == _lastHoveredProvince)
            return;
         var currentProvince = Vars.Provinces[hoverId];
         List<int> provincesToClear = new();


         //Get all ids of provinces that are currently highlighted 
         switch (Vars.MapSelectionMode)
         {
            case SelectionMode.Province:
               if (!Vars.SelectedProvinces.Contains(Vars.Provinces[_lastHoveredProvince]))
                  provincesToClear.Add(_lastHoveredProvince);
               break;
            case SelectionMode.Area:
               if (!Vars.Areas.TryGetValue(currentProvince.Area, out var area))
                  return;
               if (area.Provinces.Count > 0 && !Vars.SelectedProvinces.Contains(Vars.Provinces[area.Provinces[0]]))
                  provincesToClear = area.Provinces;
               Debug.WriteLine($"Added {provincesToClear.Count} to deselect");
               break;
            case SelectionMode.Region:
               if (!Vars.Areas.TryGetValue(currentProvince.Area, out var area2))
                  return;
               if (!Vars.Regions.TryGetValue(area2.Region, out var region))
                  return;
               foreach (var regionArea in region.Areas)
               {
                  if (!Vars.Areas.TryGetValue(regionArea, out var areReg))
                     continue;
                  provincesToClear.AddRange(areReg.Provinces);
               }
               break;
            case SelectionMode.Owner:
               if (!Vars.Countries.TryGetValue(currentProvince.Owner, out var owner))
                  return;
               foreach (var province in Vars.Provinces.Values)
               {
                  if (province.Owner.Equals(owner.Tag))
                     provincesToClear.Add(province.Id);
               }
               break;
         }

         //Clear previous selections
         if (Vars.MapMode!.Name.Equals("Default Map Mode") && provincesToClear.Count > 0)
         {
            foreach (var i in provincesToClear)
            {
               //Gui.RenderSelection(Vars.Provinces[i], Color.FromArgb(255, 0, 0, 0));
               GpuGui.RenderSelection(Vars.Provinces[i].BorderPixels, Color.Black); //Why this so slow?
            }
         }
         else if (provincesToClear.Count > 0)
         {
            List<Province> provinces = new();
            foreach (var i in provincesToClear)
            {
               provinces.Add(Vars.Provinces[i]);
            }
            var sb = new StringBuilder();
            foreach (var province in provinces)
            {
               sb.Append($"{province.Id} ");
            }
            //Debug.WriteLine($"Updating borders for {provinces.Count} | {sb.ToString()}");
            Gui.UpdateProvinceBorder(ref provinces);
         }


         //Do not highlight other provinces if the current province is selected
         if (Vars.SelectedProvinces.Contains(Vars.Provinces[hoverId]))
            return;



         //Highlight new provinces
         List<Point> borderPoints = new();
         switch (Vars.MapSelectionMode)
         {
            case SelectionMode.Province:
               var tempPoint = new Point[Vars.Provinces[hoverId].BorderPixel.length];
               Array.Copy(Vars.BorderArray!, Vars.Provinces[hoverId].BorderPixel.start, tempPoint, 0, Vars.Provinces[hoverId].BorderPixel.length);
               borderPoints.AddRange(tempPoint);
               break;

            case SelectionMode.Area:
               if (!Vars.Areas.TryGetValue(currentProvince.Area, out var area))
                  return;

               foreach (var areaProvince in area.Provinces)
               {
                  var points = new Point[Vars.Provinces[areaProvince].BorderPixel.length];
                  Array.Copy(Vars.BorderArray!, Vars.Provinces[areaProvince].BorderPixel.start, points, 0, Vars.Provinces[areaProvince].BorderPixel.length);
                  borderPoints.AddRange(points);
               }

               break;
            case SelectionMode.Region:
               if (!Vars.Areas.TryGetValue(currentProvince.Area, out var areaRegion))
                  return;
               if (!Vars.Regions.TryGetValue(areaRegion.Name, out var region))
                  return;
               foreach (var reg in region.Areas)
               {
                  if (!Vars.Areas.TryGetValue(reg, out var regArea))
                     continue;
                  foreach (var prov in regArea.Provinces)
                  {
                     var regTempPoints = new Point[Vars.Provinces[prov].BorderPixel.length];
                     Array.Copy(Vars.BorderArray!, Vars.Provinces[prov].BorderPixel.start, regTempPoints, 0, Vars.Provinces[prov].BorderPixel.length);
                     borderPoints.AddRange(regTempPoints);
                  }
               }
               break;
         }

         Gui.ChangePointCollectionColor(borderPoints, Vars.HoverCursorProvinceColor);
         _lastHoveredProvince = hoverId;
      }

      private void CooldownTimer_Tick(object sender, EventArgs e)
      {
         _cooldownTimer.Stop();
         _cooldownTimer.Tick -= CooldownTimer_Tick!;
      }

      /// <summary>
      /// Gets data of the pixel at the mouse position and creates a tooltip with it
      /// </summary>
      /// <param name="p"></param>
      private void GenerateMouseTooltip(Point p)
      {
         if (Vars.DebugMapWithBorders == null)
            return;
         var pixelColor = Vars.BorderlessMap!.GetPixel(p.X + (int)DisplayRect.X, p.Y + (int)DisplayRect.Y);

         if (!Vars.ColorIds.TryGetValue(pixelColor, out var id))
            return;
         var area = Vars.Provinces[id].Area;
         _tt.SetToolTip(Vars.MainWindow!.Map, $"{Loading.GetLoc($"PROV{id}")} [{id}]\nArea: {area}");
      }

      /// <summary>
      /// Displays the Readme with some basic instructions on the use of this product
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void Help_MenuItem_Click(object sender, EventArgs e)
      {
         Vars.WebBrowserForm = Gui.ShowForm<WebBrowserForm>();
         Util.ConvertAndDisplayInWebBrowser(Path.Combine(Vars.DataPath, "README.md"));
      }

      private void ManageMapModesMenu_Click(object sender, EventArgs e)
      {
         Vars.ManageMapmodes = Gui.ShowForm<ManageMapmodes>();
      }

      private void ZoomInButton_Click(object sender, EventArgs e)
      {
         Vars.ZoomFactor += Vars.ZoomIncrements;
         DisplayRectF = CenterScaledRectangleOnCanvas(DisplayRectF, Map.ClientRectangle);

         UpdateDisplayedImage();
      }




      public RectangleF CenterScaledRectangleOnCanvas(RectangleF rect, RectangleF pictureBox)
      {
         var scaled = GetScaledRect(rect, Vars.ZoomFactor);
         rect.Location = new PointF((pictureBox.Width - scaled.Width) / 2,
            (Map.Height - scaled.Height) / 2);
         return rect;
      }

      public RectangleF GetScaledRect(RectangleF rect, float scaleFactor) =>
         new RectangleF(rect.Location,
            new SizeF(rect.Width * scaleFactor, rect.Height * scaleFactor));

      private void ZoomOutButton_Click(object sender, EventArgs e)
      {

      }

      public void OnMapModeSelection(object sender, EventArgs e)
      {
         var button = (Button)sender;
         if (!Vars.MapModes.TryGetValue(button.Text, out var mapMode))
            return;
         if (Vars.MapMode == mapMode)
            return;
         Vars.MapMode = mapMode;
         Gui.ChangeMapMode();
      }

      private void MainWindow_Load(object sender, EventArgs e)
      {
         Visible = false;
      }

      private void MainRightClickMenu_Opening(object sender, CancelEventArgs e)
      {
         //Conditions to disable some options if requirements are not met
         if (Vars.SelectedProvinces.Count > 1)
         {
            OpenCountryFileContext.Enabled = false;
            OpenProvinceFileContext.Enabled = false;
            SelectAreaContext.Enabled = false;
            SelectRegionContext.Enabled = false;
            SelectTradenodeContext.Enabled = false;
            SelectSuperRegionContext.Enabled = false;
            SelectContinentContext.Enabled = false;
         }
         else
         {
            OpenCountryFileContext.Enabled = true;
            OpenProvinceFileContext.Enabled = true;
            SelectAreaContext.Enabled = true;
            SelectRegionContext.Enabled = true;
            SelectTradenodeContext.Enabled = true;
            SelectSuperRegionContext.Enabled = true;
            SelectContinentContext.Enabled = true;
         }
      }

      private void MainWindow_KeyDown(object sender, KeyEventArgs e)
      {
         //Debug.WriteLine($"Pressed: {e.KeyCode}");
         switch (e.KeyCode)
         {
            case Keys.W:
               MoveBitmap(0, -MoveAmount * StepsizeMove.Value);
               break;
            case Keys.A:
               MoveBitmap(-MoveAmount * StepsizeMove.Value, 0);
               break;
            case Keys.S:
               MoveBitmap(0, MoveAmount * StepsizeMove.Value);
               break;
            case Keys.D:
               MoveBitmap(MoveAmount * StepsizeMove.Value, 0);
               break;
         }

         //Map Modes Short Cuts
         if (Vars.MapModeKeyMap.TryGetValue(e.KeyCode, out var button) && button != null)
         {
            button.PerformClick();
            Debug.WriteLine($"Pressed: {e.KeyCode} and pushed: {button}");
         }
      }

      private void StatisticsTollStripMenuItem_Click(object sender, EventArgs e)
      {

      }

      private void ReloadLocalizationToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Loading.LoadAllLocalization();
      }

      private void ClearCacheToolStripMenuItem_Click(object sender, EventArgs e)
      {
         GC.Collect();
      }

      private void KeyMappingToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Vars.SettingsForm = Gui.ShowForm<SettingsForm>();
      }

      private void TooltipToolStripMenuItem_Click(object sender, EventArgs e)
      {
      }

      private void SelectAreaContext_Click(object sender, EventArgs e)
      {
         if (Vars.SelectedProvinces.Count != 1)
            return;
         var areaName = Vars.SelectedProvinces[0].Area;
         if (!Vars.Areas.TryGetValue(areaName, out var obj))
            return;
         Gui.SelectProvinceCollection(obj!.GetProvinces());
      }

      private void AlwaysRenderProvinceOutline_Click(object sender, EventArgs e)
      {
         if (AlwayRenderProvinceOutlineSetting.Checked)
         {
            AlwayRenderProvinceOutlineSetting.Checked = false;
            Vars.DrawOutlineInMapModes = false;
         }
         else
         {
            AlwayRenderProvinceOutlineSetting.Checked = true;
            Vars.DrawOutlineInMapModes = true;
         }
         Gui.ChangeMapMode();
      }

      private void SelectOwnerContext_Click(object sender, EventArgs e)
      {
         if (Vars.SelectedProvinces.Count != 1)
            return;
         if (Vars.Countries.TryGetValue(Vars.SelectedProvinces[0].Owner, out var country))
         {
            foreach (var provinceId in country.Provinces)
            {
               Vars.SelectedProvinces.Add(Vars.Provinces[provinceId]);
            }
            Gui.SelectProvinceCollection(country.Provinces);
         }
      }

      private void ProvinceCoresContext_Paint(object sender, PaintEventArgs e)
      {
         if (Vars.SelectedProvinces.Count != 1)
            return;
         for (var i = 100; i < 120; i++)
         {
            Vars.SelectedProvinces[0].Cores.Add($"{i}");
         }
         foreach (var core in Vars.SelectedProvinces[0].Cores)
         {
            ProvinceCoresContext.Items.Add(core);
         }
      }

      private void CoreSelectionConfirmContext_Click(object sender, EventArgs e)
      {
         var selectedTag = ProvinceCoresContext.Text;
         if (!Util.VerifyTag(selectedTag))
            return;
         List<int> idsToSelect = new();
         foreach (var province in Vars.Provinces)
         {
            if (province.Value.Cores.Contains(selectedTag))
            {
               idsToSelect.Add(province.Value.Id);
               Vars.SelectedProvinces.Add(province.Value);
            }
         }
         Gui.SelectProvinceCollection(idsToSelect);
      }

      private void ProvinceSelectionModeMenuItem_Click(object sender, EventArgs e)
      {
         Vars.MapSelectionMode = SelectionMode.Province;
         ProvinceSelectionModeMenuItem.Checked = true;
         AreaSelectionModeMenuItem.Checked = false;
         RegionSelectionModeMenuItem.Checked = false;
         OwnerSelectionModeMenuItem.Checked = false;
      }

      private void AreaSelectionModeMenuItem_Click(object sender, EventArgs e)
      {
         Vars.MapSelectionMode = SelectionMode.Area;
         ProvinceSelectionModeMenuItem.Checked = false;
         AreaSelectionModeMenuItem.Checked = true;
         RegionSelectionModeMenuItem.Checked = false;
         OwnerSelectionModeMenuItem.Checked = false;
      }

      private void RegionSelectionModeMenuItem_Click(object sender, EventArgs e)
      {
         Vars.MapSelectionMode = SelectionMode.Region;
         ProvinceSelectionModeMenuItem.Checked = false;
         AreaSelectionModeMenuItem.Checked = false;
         RegionSelectionModeMenuItem.Checked = true;
         OwnerSelectionModeMenuItem.Checked = false;
      }

      private void OwnerSelectionModeMenuItem_Click(object sender, EventArgs e)
      {
         Vars.MapSelectionMode = SelectionMode.Owner;
         ProvinceSelectionModeMenuItem.Checked = false;
         AreaSelectionModeMenuItem.Checked = false;
         RegionSelectionModeMenuItem.Checked = false;
         OwnerSelectionModeMenuItem.Checked = true;
      }

      private void debugToolStripMenuItem_Click(object sender, EventArgs e)
      {
         GpuGui.Start(Color.Black);
         UpdateDisplayedImage();
      }
   }
}