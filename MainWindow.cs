using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;
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
      private Point _lastMouseLocation;

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
         Gui.RenderBorders(Color.FromArgb(255,0,0,0));
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
         if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right || Vars.DebugMapWithBorders == null)
            return;
         var x = e.X + DisplayRect.X;
         var y = e.Y + DisplayRect.Y;
         //Verify that it is in bounds
         if (x < 0 || x >= Vars.DebugMapWithBorders.Width || y < 0 || y >= Vars.DebugMapWithBorders.Height)
            return;
         //Get Color an verify there is an entry for it
         var color = Vars.DebugMapWithBorders.GetPixel((int)x, (int)y);
         if (!Vars.ColorIds.ContainsKey(color))
            return;

         switch (e.Button)
         {
            case MouseButtons.Left when ModifierKeys == Keys.Control:
               {
                  var p = Vars.Provinces[Vars.ColorIds[color]];
                  Vars.SelectedProvinces.Add(p);
                  Gui.RenderSelection(p, Color.FromArgb(255, 255, 255, 255));
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
                  
                  var currentProvince = Vars.Provinces[Vars.ColorIds[color]];
                  if (Vars.SelectedProvinces.Count == 1 && Vars.SelectedProvinces[0].Equals(currentProvince))
                  {
                     Vars.SelectedProvinces.Clear();
                     return;
                  }
                  Vars.SelectedProvinces.Clear();
                  Util.NextProvince(currentProvince);
                  Vars.SelectedProvinces.Add(currentProvince);
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
         if (_lastMouseLocation == new Point(e.X, e.Y)) return;

         _cooldownTimer.Interval = 400;
         _cooldownTimer.Tick += CooldownTimer_Tick!;
         _cooldownTimer.Start();

         _lastMouseLocation = e.Location;

         GenerateMouseTooltip(new Point(e.X, e.Y));
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
         var pixelColor = Vars.DebugMapWithBorders.GetPixel(p.X + (int)DisplayRect.X, p.Y + (int)DisplayRect.Y);

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
         if (Vars.SelectedProvinces.Count > 1)
            return;
         var areaName = Vars.SelectedProvinces[0].Area;
         if (!Vars.Areas.TryGetValue(areaName, out var obj))
            return;
         Gui.SelectProvinceCollection(obj!.GetProvinces());
      }
   }
}