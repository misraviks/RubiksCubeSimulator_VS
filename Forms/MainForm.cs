using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RubiksCubeSimulator.Algorithms;
using RubiksCubeSimulator.Rubiks;

namespace RubiksCubeSimulator.Forms
{
    public partial class MainForm : Form
    {
        private RubiksCube rubiksCube;
        private CubeDetails cubeDetails;
        /// <summary>
        /// Gets all of the cube displays on the form.
        /// </summary>
        private IEnumerable<CubeFaceDisplay> CubeDisplays => 
            tableLayoutPanel.Controls.OfType<CubeFaceDisplay>();

        public MainForm()
        {
            InitializeComponent();
            ResizeRedraw = true;

            checkBoxLockColors.DataBindings.Add("Checked", 
                Settings.Instance, nameof(Settings.Instance.ColorsLocked));

            colorStrip.Colors = Settings.Instance.Palette;
            SetRubiksCube();
            SetHoverEffect();
            UpdateErrorStatus();
            cubeDetails = new CubeDetails(rubiksCube);
        }

        private void SetRubiksCube()
        {
            // Restore cube from settings
             rubiksCube = new RubiksCube(Settings.Instance.CubeColors);

            // Create a solved cube with the developers color scheme
            //rubiksCube = RubiksCube.Create(CubeColorScheme.DevsScheme);
            
            rubiksCube.MoveMade += RubiksCubeMoveMade;
            UpdateDisplayedCube();
        }

        private void UpdateErrorStatus()
        {
            var defects = rubiksCube.GetColorDefects();
            labellErrorStatus.Text = defects.GetShortReport();
        }

        private void SetHoverEffect()
        {
            foreach (var display in CubeDisplays)
            {
                if (checkBoxLockColors.Checked)
                    display.ClickMode = ClickMode.Rotation;
                else if (colorStrip.HasSelection)
                    display.ClickMode = ClickMode.ColorSet;
                else
                    display.ClickMode = ClickMode.None;
            }
        }

        private void UpdateDisplayedCube()
        {
            foreach (var display in CubeDisplays)
                display.RubiksCube = rubiksCube;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Settings.Instance.Palette = colorStrip.Colors;
            Settings.Instance.CubeColors = rubiksCube.AllColors;
            //Settings.Instance.Save();
            base.OnClosing(e);
        }

        private static DialogResult ShowQuestionPrompt(string message)
        {
            return MessageBox.Show(message, Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void buttonCleanSlate_Click(object sender, EventArgs e)
        {
            if (ShowQuestionPrompt("Are you sure you want to undo changes?") == DialogResult.Yes)
            {
                labellErrorStatus.Text = string.Empty;
                rubiksCube.CleanSlate();
                tableLayoutPanel.Invalidate(true);
            }
        }

        private void buttonUndoChanges_Click(object sender, EventArgs e)
        {
            if (ShowQuestionPrompt("Are you sure you want to undo changes?") == DialogResult.Yes)
            {
                labellErrorStatus.Text = string.Empty;
                rubiksCube.Restore();
                tableLayoutPanel.Invalidate(true);
            }
        }

        private void textBoxCommand_KeyDown(object sender, KeyEventArgs e)
        {
          
            String Scramble = "D2 U2 R B D Fi L2 F2 D U Bi U2 D2 L2 Bi Li F R L F2 U2 L2 U2 Di L2";
            textBoxCommand.BackColor = Color.FromKnownColor(KnownColor.Window);
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right|| e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                KeyMove(e.KeyCode);
            }
            if (e.KeyCode != Keys.Enter) return;
            
            string lower = textBoxCommand.Text;
            if (lower == "clean slate") rubiksCube.CleanSlate();
            string[] splitted = textBoxCommand.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if(textBoxCommand.Text.Contains(',')) splitted = textBoxCommand.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var moveList = new List<CubeMove>();
            if (lower == "scramble")
            {
                splitted = Scramble.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            foreach (string str in splitted)
            {
                try
                {
                    string moveToBeMade = str.Replace("'", "i");
                    moveList.Add(new CubeMove(moveToBeMade));
                }
                catch (ArgumentException)
                {
                    textBoxCommand.BackColor = Color.LightPink;
                    return; // Invalid input
                }
            }

            foreach (var move in moveList)
            {
                lblStatus.Text = "Last Move: " + move;
                rubiksCube.MakeMove(move);
            }
            
            
            e.SuppressKeyPress = true;
            textBoxCommand.Clear();
            tableLayoutPanel.Invalidate();
            
        }

        private void checkBoxLockColors_CheckedChanged(object sender, EventArgs e)
        {
            SetHoverEffect();
            colorStrip.Enabled = !checkBoxLockColors.Checked;
        }

        private void colorStrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetHoverEffect();

            foreach (var display in CubeDisplays)
                display.NewColor = colorStrip.SelectedColor;
        }

        private void cubeDisplay_CellMouseClicked(object sender, CellMouseClickedEventArgs e)
        {
            UpdateErrorStatus();
        }

        private void RubiksCubeMoveMade(object sender, CubeMove move)
        {
            cubeDetails = new CubeDetails(rubiksCube);
            if (rubiksCube.Solved)
            {
                lblStatus.Text = "Cube Solved";
                lblStatus.ForeColor = Color.Green;
            }
            else if (cubeDetails.DaisyFormed())
            {
                lblStatus.Text = "Daisy Solved";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                lblStatus.Text = "Last Move: " + move;
            }
        }

        private void tableLayoutPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            KeyMove(e.KeyCode);
        }

        private void KeyMove(Keys keys)
        {
            string movement = "";
            switch (keys)
            {
                case Keys.Left:
                    movement = "LS";
                    break;
                case Keys.Right:
                    movement = "RS";
                    break;
                case Keys.Up:
                    movement = "US";
                    break;
                case Keys.Down:
                    movement = "DS";
                    break;
                default:
                    return;

            }
            CubeMove move = new CubeMove(movement);
            lblStatus.Text = "Last Move: " + move;
            rubiksCube.MakeMove(move);
            tableLayoutPanel.Invalidate();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            KeyMove(e.KeyCode);
        }
    }

}
