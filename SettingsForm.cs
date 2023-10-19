using System.Diagnostics;

namespace EU4_Parse_Lib;


public partial class SettingsForm : Form
{
    private Keys? _newKey = null;

    public SettingsForm()
    {
        InitializeComponent();
    }

    private void SettingsForm_Load(object sender, EventArgs e)
    {
        SettingsTreeView.ExpandAll();
    }

    private void SettingsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        CreateKeyBindsListView();
    }

    private void ClearSettingsMenu()
    {
        keyBindsView.Visible = false;

    }

    private void CreateKeyBindsListView()
    {
        var selectedNode = SettingsTreeView.SelectedNode;
        keyBindsView.Visible = false;
        keyBindsView.View = View.List;
        keyBindsView.Items.Clear();

        switch (selectedNode.Name)
        {
            case "KeybindsSettingsNode":
                ClearSettingsMenu();
                keyBindsView.Visible = true;

                foreach (var kvp in Vars.MapModeKeyMap)
                {
                    var tag = kvp.Value?.Tag?.ToString() ?? "unassigned";
                    keyBindsView.Items.Add($"{kvp.Key} - {tag}");
                }
                break;
        }
    }

    private void ConfirmSettingsButton_Click(object sender, EventArgs e)
    {


    }

    private void SettingsForm_KeyUp(object sender, KeyEventArgs e)
    {

    }

    private void SettingsForm_KeyDown(object sender, KeyEventArgs e)
    {
        _newKey = e.KeyCode;
        Debug.WriteLine(_newKey.ToString());
        e.SuppressKeyPress = true;
        newKeyBindBox.Text = _newKey.ToString();
    }

    private void AssignKeyButton_Click(object sender, EventArgs e)
    {
        if (SettingsTreeView.SelectedNode == null || keyBindsView.SelectedItems == null || _newKey == null)
            return;
        if (SettingsTreeView.SelectedNode.Name == "KeybindsSettingsNode")
        {
            if (Vars.MapModeKeyMap.ContainsKey((Keys)_newKey))
            {
                MessageBox.Show($"The key [{_newKey}] is already taken by [{Vars.MapModeKeyMap[(Keys)_newKey]}]\nPlease Choose another one.", "Dublicate Key", MessageBoxButtons.OK);
                return;
            }
            if (Vars.MapMovementKeyMap.ContainsKey((Keys)_newKey))
            {
                MessageBox.Show($"The key [{_newKey}] is already taken by [{Vars.MapMovementKeyMap[(Keys)_newKey]}]\nPlease Choose another one.", "Dublicate Key", MessageBoxButtons.OK);
                return;
            }

            Dictionary<Keys, Button?> temp = new(Vars.MapModeKeyMap.Count);

            foreach (var kvp in Vars.MapModeKeyMap)
            {
                if (keyBindsView.SelectedItems[0].Text.StartsWith(kvp.Key.ToString()))
                {
                    temp.TryAdd((Keys)_newKey, kvp.Value);
                    Debug.WriteLine(kvp.Key.ToString() + " Was replaced");
                }
                else
                {
                    temp.TryAdd(kvp.Key, kvp.Value);
                }
            }

            Vars.MapModeKeyMap = temp;
        }
        CreateKeyBindsListView();
    }

    private void ResetKeyMappingsButton_Click(object sender, EventArgs e)
    {
        DefaultValues.DefaulMapModesKeys();
    }

    private void ConfirmSettingsButton_Click_1(object sender, EventArgs e)
    {
        Dictionary<Keys, object?> nullKeyMap = new();

        foreach (var key in Vars.MapModeKeyMap.Keys.ToList())
        {
            nullKeyMap[key] = null;
        }
        Saving.SaveObjectToJson(nullKeyMap, Path.Combine(Vars.DataPath, "UserKeyMapping.json"));
    }
}
